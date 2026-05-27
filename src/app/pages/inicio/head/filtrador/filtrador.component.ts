import { ChangeDetectorRef, Component } from '@angular/core';
import { AuthService } from '../../../../Service/auth.service';
import { Router } from '@angular/router';
import { CategoriaService } from '../../../../Service/categoria.service';
import { Categorium } from '../../../../Interface/categorium';

@Component({
  selector: 'app-filtrador',
  templateUrl: './filtrador.component.html',
  styleUrl: './filtrador.component.scss'
})
export class FiltradorComponent {
  categorias: Categorium[] = [];
  isMenuVisible: boolean = false;
  isProcessing: boolean=false;
  hoveredCategoriaId: number | null = null;
  constructor(
    private router: Router,
    private categoriaService:CategoriaService,
    private cd: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
      this.obtenerCategorias(); 
    };
  
  obtenerCategorias(): void {
    this.categoriaService.getList().subscribe(
      categorias => {
        this.categorias = categorias;
      },
      error => {
        console.error('Error al obtener las categorías:', error);
      }
    );
  }
  redireccionarALibros(idCategoria: number) {
    this.router.navigate(['/categoria', idCategoria, 'libros']);
}
cargarSubcategorias(idCategoria: number): void {
  this.categoriaService.getSubCategoriasPorId(idCategoria).subscribe(subcategorias => {
    const categoriaIndex = this.categorias.findIndex(c => c.idCategoria === idCategoria);
    if (categoriaIndex !== -1) {
      this.categorias[categoriaIndex].subcategorias = subcategorias;
      this.categorias = [...this.categorias];
      this.hoveredCategoriaId = idCategoria;
      this.cd.detectChanges(); 
    }
  }, (error: any) => {
    console.error('Error al cargar subcategorías', error);
  });
}
  redireccionarALibrosSubcategoria(idSubcategoria: number) {
    this.router.navigate(['/subcategoria', idSubcategoria, 'libros']);
  }
  mostrarSubcategorias(categoriaId: number): boolean {
    return categoriaId === this.hoveredCategoriaId;
  }
  esconderSubcategorias(): void {
    this.hoveredCategoriaId = null;
}
}
