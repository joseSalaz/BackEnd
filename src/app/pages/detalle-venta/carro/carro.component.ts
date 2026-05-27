import { Component, Injectable, Input } from '@angular/core';
import { Libro } from '../../../Interface/libro';

import { CarroService } from '../../../Service/carro.service';
import { BehaviorSubject } from 'rxjs';
import { LibroService } from '../../../Service/libro.service';
import { ItemCarrito } from '../../../Interface/carrito';
import { AuthService } from '../../../Service/auth.service';
import { Router } from '@angular/router';
import { LocalStorageService } from '../../../Service/local-storage.service';
@Injectable({
  providedIn: 'root'
})
@Component({
  selector: 'app-carro',
  templateUrl: './carro.component.html',
  styleUrls: ['./carro.component.scss']
})
export class CarroComponent {

  private storageKey = 'carroLibros';

  libros: Libro[] = [];
  itemsCarrito: ItemCarrito[] = [];
  defaultImageUrl: string = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";

  constructor(
    private localStorageService: LocalStorageService,
    private carroService: CarroService,
    private libroService: LibroService,
    private authService: AuthService,
    private router: Router
  ) {
    this.itemsCarrito = this.localStorageService.getItem<ItemCarrito[]>(this.storageKey) || [];
  }


  mostrarCarro = true;
  onImageError(event: Event): void {
    const imgElement = event.target as HTMLImageElement;
    imgElement.src = this.defaultImageUrl; // Asignamos la URL de imagen por defecto
  }
  finalizarCompra() {
    const profile = this.authService.getProfile();
    if (!profile) {
      // Indicar el intento de compra
      localStorage.setItem('intentoCompra', 'true');
      // Iniciar el flujo de login
      this.router.navigate(['auth/log-in'])
    } else {

      // Proceder con la compra
      this.router.navigate(['/detalle-venta']);
    }
  }



  // Método para abrir el componente Carro
  abrirCarro() {
    this.mostrarCarro = true;
  }

  // Método para cerrar el componente Carro
  cerrarCarro() {
    this.mostrarCarro = false;
  }

  ngOnInit() {
    this.carroService.itemsCarrito.subscribe(items => {
      this.itemsCarrito = items;
    });
  }

  delete(indice: number) {
    this.carroService.deleteLibro(indice);
  }

  calcularTotal(): number {
    if (!this.itemsCarrito || this.itemsCarrito.length === 0) {
      return 0;
    }

    return this.itemsCarrito.reduce((total, item) => {
      return total + (item.precioVenta * (item.cantidad || 1));
    }, 0);
  }
}