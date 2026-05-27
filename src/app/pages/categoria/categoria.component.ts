import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriaService } from '../../Service/categoria.service';
import { Libro } from '../../Interface/libro';
import { switchMap } from 'rxjs/operators'; // Importa switchMap
import { LibroService } from '../../Service/libro.service';

@Component({
  selector: 'app-categoria',
  templateUrl: './categoria.component.html',
  styleUrls: ['./categoria.component.scss']
})
export class CategoriaComponent implements OnInit {
  datas: Libro[] = [];

  mostrarContenido = false;
  filtros=['prueba01','prueba02'];

  constructor(
     private categoriaService: CategoriaService,
     private router: Router,
     private route: ActivatedRoute,
     private libroService: LibroService,
   ) {}

   ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        const idCategoria = Number(params.get('idCategoria'));
        return this.categoriaService.getLibrosPorCategoriaId(idCategoria);
      })
    ).subscribe(libros => {
      this.datas = libros;
      this.obtenerPrecios();
    });
  }

  obtenerPrecios(): void {
    this.datas.forEach((libro, index) => {
      this.libroService.getPreciosPorIdLibro(libro.idLibro).subscribe(precios => {
        if (precios.length > 0 && precios[0].precioVenta != null) {
          this.datas[index].precioVenta = precios[0].precioVenta;
        }
      });
    });
  }

  redireccionarAlDetalleProducto(libroId: number): void {
    this.router.navigate(['/detalle-producto', libroId]);
  }
}









