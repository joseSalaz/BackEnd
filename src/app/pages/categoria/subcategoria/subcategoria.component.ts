import { Component, OnInit } from '@angular/core';
import { Libro } from '../../../Interface/libro';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { SubCategoriaService } from '../../../Service/subcategoria.service';
import { LibroService } from '../../../Service/libro.service';
@Component({
  selector: 'app-subcategoria',
  templateUrl: './subcategoria.component.html',
  styleUrl: './subcategoria.component.scss'
})
export class SubcategoriaComponent implements OnInit {
    datas: Libro[] = [];
    mostrarContenido = false;
    filtros=['prueba01','prueba01'];
    constructor(
       private subcategoriaService: SubCategoriaService,
       private router: Router,
       private route: ActivatedRoute,
       private libroService:LibroService
     ) {}
  
     ngOnInit(): void {
       this.route.paramMap.pipe(
         switchMap(params => {
           const idSubCategoria = Number(params.get('idSubCategoria'));
           return this.subcategoriaService.getLibrosPorSubCategoriaId(idSubCategoria);
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
     redireccionarAlDetalleProducto(libroId: number) {
       this.router.navigate(['/detalle-producto', libroId]);
     }
}
