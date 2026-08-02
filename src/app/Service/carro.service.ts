import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { ItemCarrito } from '../Interface/carrito';
import { HttpClient } from '@angular/common/http';
import { Datallecarrito } from '../Interface/detallecarrito';
import { ExchangeRateService } from './exchange-rate.service';
import { catchError, switchMap } from 'rxjs/operators';
import { tap } from 'rxjs/operators';
import { Direccion } from '../Interface/direccion';
import { environment } from '../../environments/environment';
import { LocalStorageService } from './local-storage.service';
@Injectable({
  providedIn: 'root'
})
export class CarroService {
  private direccionSeleccionada: Direccion | null = null;
  private storageKey = 'carroItems';
  private endPoint: string = environment.endPoint;
  private _itemsCarrito: BehaviorSubject<ItemCarrito[]>;
  private executePaymentUrl = `${this.endPoint}api/Paypal/execute-payment`;
  constructor(
    private http: HttpClient,
    private exchangeRateService: ExchangeRateService,
    private localStorageService: LocalStorageService
  ) {
    const storedItems = this.localStorageService.getItem<ItemCarrito[]>(this.storageKey) || [];
    this._itemsCarrito = new BehaviorSubject<ItemCarrito[]>(storedItems);
  }
  setDireccionSeleccionada(direccion: Direccion): void {
    this.direccionSeleccionada = direccion;
    localStorage.setItem('direccionSeleccionada', JSON.stringify(direccion)); // ✅ Guarda en localStorage
  }


  getDireccionSeleccionada(): Direccion | null {
    if (!this.direccionSeleccionada) {
      this.direccionSeleccionada = JSON.parse(localStorage.getItem('direccionSeleccionada') || 'null');
    }
    return this.direccionSeleccionada;
  }
  obtenerCantidadProductos(): number {
    return this._itemsCarrito.value.reduce((acc, curr) => acc + curr.cantidad, 0);
  }

  get itemsCarrito() {
    return this._itemsCarrito.asObservable();
  }

  addNewProduct(item: ItemCarrito) {
    const itemsActualizados = [...this._itemsCarrito.value, item];
    this.updateStorage(itemsActualizados);
  }

  deleteLibro(index: number) {
    const itemsActualizados = this._itemsCarrito.value.filter((_, i) => i !== index);
    this.updateStorage(itemsActualizados);
  }

  private updateStorage(items: ItemCarrito[]) {
    this._itemsCarrito.next(items);
    this.localStorageService.setItem(this.storageKey, items);
  }
  enviarCarritoAlBackend(idDireccion: number): Observable<any> {
    const usuarioData = JSON.parse(localStorage.getItem('usuarioData') || '{}');
    return this.exchangeRateService.getExchangeRate().pipe(
      switchMap(tasaDeCambio => {
        const carritoActual = this._itemsCarrito.value.map(item => ({
          ...item,
          precioVenta: parseFloat((item.precioVenta * tasaDeCambio).toFixed(2))
        }));
        const totalAmount = parseFloat(carritoActual.reduce((acc, item) => acc + (item.precioVenta * item.cantidad), 0).toFixed(2));

        const detalleCarrito: Datallecarrito = {
          Items: carritoActual,
          TotalAmount: totalAmount,
          Persona: usuarioData,
          IdDireccion: idDireccion // Asegúrate de que idDireccion esté presente aquí
        };

        return this.http.post(`${this.endPoint}api/Cart`, detalleCarrito);
      })
    );
  }

  getCantidadPorProducto(idLibro: number): number {
    const item = this._itemsCarrito.value.find(item => item.libro.idLibro === idLibro);
    return item ? item.cantidad : 0;
  }

  confirmarPago(paymentId: string, payerId: string): Observable<any> {
    if (typeof window === 'undefined') {
      console.error("⚠️ localStorage no está disponible en SSR.");
      return throwError(() => new Error("LocalStorage no está disponible."));
    }
    const carritoActual = JSON.parse(localStorage.getItem(this.storageKey) || '[]');
    const totalAmount = carritoActual.reduce((acc: any, item: any) => acc + (item.precioVenta * item.cantidad), 0);
    const usuarioData = JSON.parse(localStorage.getItem('usuarioData') || '{}');
    const direccionSeleccionada = this.getDireccionSeleccionada();

    if (!direccionSeleccionada || !direccionSeleccionada.idDireccion) {
      console.error("⚠️ Error: No se seleccionó una dirección antes de confirmar el pago.");
      return throwError(() => new Error("Debe seleccionar una dirección antes de confirmar el pago."));
    }


    const body = {
      PaymentId: paymentId,
      PayerID: payerId,

      Carrito: {
        Items: carritoActual,
        TotalAmount: totalAmount,
        Persona: usuarioData,
        IdDireccion: direccionSeleccionada.idDireccion,
      }
    };

    return this.http.post(this.executePaymentUrl, body).pipe(
      tap(response => {
        // Si el pago se confirma exitosamente, limpia el carrito
        this.limpiarCarrito();
      })
    );
  }
  private limpiarCarrito(): void {
    localStorage.removeItem(this.storageKey);
    this._itemsCarrito.next([]);
  }


  enviarCarritoAlBackendParaMercadoPago(idDireccion: number): Observable<any> {
    const usuarioData = JSON.parse(localStorage.getItem('usuarioData') || '{}');
    const carritoActual = this._itemsCarrito.value.map(item => ({
      ...item,
      precioVenta: item.precioVenta // Asumiendo que ya está en soles
    }));

    const totalAmount = carritoActual.reduce((acc, item) => acc + (item.precioVenta * item.cantidad), 0);

    const detalleCarrito: Datallecarrito = {
      Items: carritoActual,
      TotalAmount: totalAmount,
      Persona: usuarioData,
      IdDireccion: idDireccion,
    };

    return this.http.post(`${this.endPoint}api/Cart/mercadopago`, detalleCarrito).pipe(
      tap(response => {

      }),
      catchError(error => {
        console.error('Error al enviar el carrito a Mercado Pago:', error);
        return throwError(error); // Lanza el error
      })
    );
  }


  confirmarPagoConMercadoPago(paymentId: string, preferenceId: string): Observable<any> {
    // Recuperar el carrito del almacenamiento local
    const carritoActual = JSON.parse(localStorage.getItem(this.storageKey) || '[]');
    const totalAmount = carritoActual.reduce((acc: any, item: any) => acc + (item.precioVenta * item.cantidad), 0);
    const usuarioData = JSON.parse(localStorage.getItem('usuarioData') || '{}');
    const direccionSeleccionada = this.getDireccionSeleccionada();

    if (!direccionSeleccionada || !direccionSeleccionada.idDireccion) {
      console.error("⚠️ Error: No se seleccionó una dirección antes de confirmar el pago.");
      return throwError(() => new Error("Debe seleccionar una dirección antes de confirmar el pago."));
    }
    // Preparar el cuerpo de la solicitud incluyendo el ID de la preferencia y el carrito
    const body = {
      PaymentID: paymentId,
      PreferenceId: preferenceId,
      Carrito: {
        Items: carritoActual,
        TotalAmount: totalAmount,
        Persona: usuarioData,
        IdDireccion: direccionSeleccionada.idDireccion,
      }
    };

    return this.http.post(`${this.endPoint}api/MercadoPago/execute-payment`, body).pipe(
      tap(response => {
        // Si el pago se confirma exitosamente, limpia el carrito
        this.limpiarCarrito();
      }),
      catchError(error => {
        console.error('Error al ejecutar el pago en Mercado Pago:', error);
        return throwError(error); // Lanza el error
      })
    );
  }

  actualizarCantidad(idLibro: number, nuevaCantidad: number) {
    const itemsActualizados = this._itemsCarrito.value.map(item => {
      if (item.libro.idLibro === idLibro) {
        return { ...item, cantidad: Number(nuevaCantidad) }; // Asegura que cantidad sea número
      }
      return item;
    });

    this.updateStorage(itemsActualizados); // Guarda en LocalStorage y actualiza el BehaviorSubject
  }

  agregarOActualizarProducto(item: ItemCarrito): void {
    const itemsActuales = this._itemsCarrito.value;

    const indiceExistente = itemsActuales.findIndex(i =>
      i.libro?.idLibro === item.libro?.idLibro ||
      i.libro?.idLibro?.toString() === item.libro?.idLibro?.toString()
    );

    if (indiceExistente !== -1) {
      // Ya existe → sumar cantidad
      const itemsActualizados = itemsActuales.map((i, index) =>
        index === indiceExistente
          ? { ...i, cantidad: i.cantidad + item.cantidad }
          : i
      );
      this.updateStorage(itemsActualizados);
    } else {
      // Nuevo item
      this.addNewProduct(item);
    }
  }

}

