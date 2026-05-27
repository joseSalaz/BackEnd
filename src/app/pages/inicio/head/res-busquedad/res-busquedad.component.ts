import { Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { LibroService } from '../../../../Service/libro.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-res-busquedad',
  templateUrl: './res-busquedad.component.html',
  styleUrl: './res-busquedad.component.scss'
})
export class ResBusquedadComponent {

  terminoBusqueda: string = '';
  resultados: any[] = [];
  private listener?: () => void;
  private subscription: Subscription = new Subscription();
  defaultImageUrl: string = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
  @ViewChild('searchBox') searchBox!: ElementRef;

  constructor(
    private libroService: LibroService,
    private renderer: Renderer2,
    private el: ElementRef
    ) {}

    ngOnInit() {
      // Escuchar clics en el documento
      this.listener = this.renderer.listen('document', 'click', (event) => {
        // Si el clic fue fuera del searchBox, limpia los resultados
        if (!this.el.nativeElement.contains(event.target)) {
          this.resultados = [];
          this.terminoBusqueda = '';
        }
      });
    }

  ngOnDestroy(): void {
    this.listener?.(); // Llama a la función si existe
    this.subscription.unsubscribe(); // Siempre bueno desuscribirse para evitar fugas de memoria
  }
  onImageError(event: Event): void {
    const imgElement = event.target as HTMLImageElement;
    imgElement.src = this.defaultImageUrl; // Asignamos la URL de imagen por defecto
  }
  buscarLibros() {
    if (this.terminoBusqueda.trim()) {
      this.libroService.getLibrosAutoComplete(this.terminoBusqueda).subscribe({
        next: (libros) => {
          this.resultados = libros;
        },
        error: (error) => {
          this.resultados = [];
        }
      });
    } 
  }
  
}
