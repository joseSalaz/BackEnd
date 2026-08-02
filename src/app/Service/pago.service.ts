import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { catchError, Observable, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PagoService {
  private endPoint: string = environment.endPoint;
  private apiUrl: string = `${this.endPoint}api/Paypal`;
  private apiUrlMercadoPago: string = `${this.endPoint}api/MercadoPago`;
  
  constructor(private http: HttpClient ) {}


  iniciarPago(cartItems: any[]){
    return this.http.post('api/pago/iniciar',{cartItems})
  }

  executePayment(paymentId: string, payerId: string): Observable<any> {
    const url = `${this.apiUrl}/execute-payment`;
    return this.http.post(url, { paymentId, payerId });
  }


  // Iniciar pago con Mercado Pago
  iniciarPagoMercadoPago(detalleCarrito: any): Observable<any> {
    const url = `${this.apiUrlMercadoPago}/mercadopago`; // Ruta para iniciar el pago
    return this.http.post(url, detalleCarrito);
  }

  // Ejecutar pago con Mercado Pago
  executePaymentMercadoPago(preferenceId: string, paymentId: string): Observable<any> {
    const url = `${this.apiUrlMercadoPago}/execute-payment`; // Ruta para ejecutar el pago
    return this.http.post(url, { preferenceId, paymentId }).pipe(
      tap(response => {
        console.log('Respuesta de ejecutar pago de Mercado Pago:', response); // Agrega este log
      }),
      catchError(error => {
        console.error('Error al ejecutar el pago de Mercado Pago:', error);
        return throwError(error);
      })
    );
  }
  
}
