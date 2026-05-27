import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LibroService } from '../../Service/libro.service';
import { Libro } from '../../Interface/libro';
import { LibroAutorService } from '../../Service/libro_autor.service';
import { CarroService } from '../../Service/carro.service';
import { Precio } from '../../Interface/precio';
import { SubCategoria } from '../../Interface/subcategoria';
import { SubCategoriaService } from '../../Service/subcategoria.service';
import { ItemCarrito } from '../../Interface/carrito';
import { Autor } from '../../Interface/autor';
import { Kardex } from '../../Interface/kardex';
import Swal from 'sweetalert2';
import { FavoritoService } from '../../Service/favorito.service';
import { Favorito } from '../../Interface/favorito';
import { AuthService } from '../../Service/auth.service';

@Component({
  selector: 'app-detalle-producto',
  templateUrl: './detalle-producto.component.html',
  styleUrls: ['./detalle-producto.component.scss']
})
export class DetalleProductoComponent implements OnInit {
  libro: Libro | null = null;
  subcategoria: SubCategoria | null = null;
  autores: any[] = [];
  idLibro: number = 0; 
  cantidad: number = 1;
  altura: number = 0;
  ancho: number = 0;
  precioVenta: number = 0; 
  idSubCategoria: number = 0;
  stockDisponible: number = 0;
  kardex: Kardex | null = null;
  bloquearInput: boolean = false;
  idPersonaLogueada: number = 0;
  esFavorito: boolean = false;
  defaultImageUrl: string = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";

private Toast = Swal.mixin({
  toast: true,
  position: 'top-end',
  showConfirmButton: false,
  timer: 2500,
  timerProgressBar: true,
  // quita target: 'body'
  showClass: {
    popup: 'animate__animated animate__fadeInRight animate__faster'
  },
  hideClass: {
    popup: 'animate__animated animate__fadeOutRight animate__faster'
  }
});

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private libroService: LibroService,
    private libroAutorService: LibroAutorService,
    private carroService: CarroService,
    private subCategoriaService: SubCategoriaService,
    private favoritoService: FavoritoService
  ) {}

  ngOnInit(): void {
    
const usuarioDataStr = localStorage.getItem('usuarioData');
    if (usuarioDataStr) {
      try {
        const usuarioLogueado = JSON.parse(usuarioDataStr);
        // Validamos que exista la propiedad correcta (ej: idPersona)
        if (usuarioLogueado && usuarioLogueado.idPersona) {
          this.idPersonaLogueada = usuarioLogueado.idPersona;
        }
      } catch (error) {
        console.error('Error al parsear usuarioData desde el localStorage', error);
      }
    }
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.obtenerLibro(id);
        this.obtenerAutoresDeLibro(+id);
        this.obtenerKardex(+id);
        
        if (this.idPersonaLogueada > 0) {
          this.verificarEstadoFavorito(+id);
        }
      }
    });
  }




  verificarEstadoFavorito(libroId: number): void {
    this.favoritoService.checkIsFavorito(this.idPersonaLogueada, libroId).subscribe({
      next: (esFav) => this.esFavorito = esFav,
      error: (err) => console.error('Error al verificar favorito:', err)
    });
  }

  toggleFavorito(): void {
  
   

    if (this.idPersonaLogueada <= 0) {
      Swal.fire({
        title: '¡Inicia Sesión!',
        text: 'Debes tener una cuenta activa para poder guardar libros en tus favoritos.',
        icon: 'info',
        confirmButtonText: 'Entendido',
        confirmButtonColor: '#0d6efd',
        customClass: {
          popup: 'rounded-4'
        }
      });
      return;
    }

    if (this.esFavorito) {
      this.favoritoService.deleteFavoritoByPersonaAndLibro(this.idPersonaLogueada, this.idLibro).subscribe({
        next: () => {
          this.esFavorito = false;
          // Alerta sin ícono amarillo, limpia, minimalista y más grande
          this.Toast.fire({
            title: '<h5 class="m-0 font-weight-bold" style="color: #6c757d;">Quitado de tus favoritos 💔</h5>',
            background: '#ffffff',
            html: false
          });
        },
        error: () => this.mostrarError('No se pudo quitar de favoritos.')
      });
    } else {
      if (!this.libro) return;

      const nuevoFavorito: Favorito = {
        idPersona: this.idPersonaLogueada,
        idLibro: this.idLibro,
        fechaAgregado: new Date().toISOString(),
        tituloLibro: this.libro.titulo || '',  
        imagenLibro: this.libro.imagen || ''
      };

      this.favoritoService.addFavorito(nuevoFavorito).subscribe({
        next: () => {
          this.esFavorito = true;
          this.Toast.fire({
            title: '<h5 class="m-0 text-white font-weight-bold">¡Agregado a favoritos! ❤️</h5>',
            background: '#22c55e'
          });
        },
        error: () => this.mostrarError('No se pudo agregar a favoritos.')
      });
    }
  }

  agregarAlCarrito(): void {
    const cantidadEnCarrito = this.carroService.getCantidadPorProducto(this.idLibro);

    if (cantidadEnCarrito + this.cantidad > this.stockDisponible) {
      this.mostrarError('Ya has alcanzado el máximo permitido de este producto en el carrito.');
      return;
    }

    if (!this.libro || this.precioVenta <= 0 || this.cantidad <= 0 || this.stockDisponible <= 0) {
      this.mostrarError('Datos inválidos. No se puede agregar al carrito.');
      return;
    }

    const itemCarrito: ItemCarrito = {
      libro: this.libro,
      precioVenta: this.precioVenta,
      cantidad: this.cantidad
    };

    // Toast animado para éxito al comprar
    this.Toast.fire({
      title: '<h5 class="m-0 text-white font-weight-bold">¡Agregado al carrito con éxito! 🛒</h5>',
      background: '#0d6efd'
    });

    this.carroService.agregarOActualizarProducto(itemCarrito); 
  }

  onImageError(event: Event): void {
    const imgElement = event.target as HTMLImageElement;
    imgElement.src = this.defaultImageUrl; 
  }

  mostrarError(mensaje: string): void {
    Swal.fire({
      title: 'Atención',
      text: mensaje,
      icon: 'warning',
      confirmButtonText: 'Entendido',
      confirmButtonColor: '#0d6efd'
    });
  }

  obtenerLibro(id: string): void {
    this.libroService.getLibroPorId(id).subscribe(
      (libro: Libro) => {
        this.libro = libro;
        this.idSubCategoria = libro.idSubcategoria;
        if (libro.tamanno) {
          this.extraerDimensiones(libro.tamanno);
        }
        this.idLibro = libro.idLibro; 
        this.obtenerSubCategoria(libro.idSubcategoria);
        this.obtenerPrecioVenta(); 
      },
      (error: any) => console.error('Error al obtener los detalles del libro:', error)
    );
  }

  obtenerSubCategoria(idSubCategoria: number): void {
    if (idSubCategoria) {
      this.subCategoriaService.getSubCategoriaPorId(idSubCategoria).subscribe(
        (subCategoria: SubCategoria) => this.subcategoria = subCategoria,
        (error: any) => console.error('Error al obtener la subcategoría:', error)
      );
    }
  }

  obtenerKardex(libroId: number): void {
    this.libroService.getKardexPorIdLibro(libroId).subscribe(
      (kardex: Kardex) => this.stockDisponible = kardex.stock,
      (error: any) => console.error('Error al obtener el kardex:', error)
    );
  }

  obtenerAutoresDeLibro(idLibro: number): void {
    this.libroAutorService.getAutoresDeLibro(idLibro).subscribe(
      (autores: Autor[]) => this.autores = autores,
      (error: any) => console.error('Error al obtener los autores:', error)
    );
  }

  private extraerDimensiones(tamanno: string): void {
    const tamannoConPuntos = tamanno.replace(/,/g, '.');
    const dimensiones = tamannoConPuntos.match(/(\d+(\.\d+)?)/g);
    if (dimensiones) {
      this.ancho = parseFloat(dimensiones[0]);
      this.altura = dimensiones.length > 1 ? parseFloat(dimensiones[1]) : this.altura;
    }
  }

  obtenerPrecioVenta(): void {
    if (!this.idLibro) return;

    this.libroService.getPreciosPorIdLibro(this.idLibro).subscribe(
      (precios: Precio[]) => {
        const precioConVenta = precios.find(precio => precio.precioVenta != null);
        this.precioVenta = precioConVenta!.precioVenta ?? 0;
      },
      error => console.error('Error al obtener el precio de venta:', error)
    );
  }

  incrementarCantidad(): void {
    if (this.cantidad < this.stockDisponible) {
      this.cantidad++;
    }
    this.verificarStock();
  }

  decrementarCantidad(): void {
    if (this.cantidad > 1) {
      this.cantidad--;
    }
    this.verificarStock();
  }

  verificarStock(): void {
    if (this.cantidad >= this.stockDisponible) {
      this.mostrarError('La cantidad no puede superar el stock disponible.');
      this.cantidad = this.stockDisponible;
      this.bloquearInput = true; 
    } else if (this.cantidad < this.stockDisponible) {
      this.bloquearInput = false; 
    }
  }
}