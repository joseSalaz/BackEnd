import { Component,Input,OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { FavoritoService } from '../../../Service/favorito.service';
import { Favorito } from '../../../Interface/favorito';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-user-fav',
  templateUrl: './user-fav.component.html',
  styleUrl: './user-fav.component.scss',
  
})
export class UserFavComponent  {
@Input() idPersonaLogueada!: number;
  listaFavoritos: Favorito[] = [];
  cargando: boolean = true;

constructor(
    private favoritoService: FavoritoService,
    private router: Router
  ) { }
ngOnChanges(changes: SimpleChanges): void {
    if (changes['idPersonaLogueada'] && this.idPersonaLogueada > 0) {
      this.cargarFavoritos();
    }
  }
 cargarFavoritos(): void {
    this.cargando = true;
    this.favoritoService.getFavoritosByPersona(this.idPersonaLogueada).subscribe({
      next: (data) => {
        this.listaFavoritos = data;
        this.cargando = false;
      },
      error: (err) => {
        console.error('Error al cargar favoritos:', err);
        this.cargando = false;
      }
    });
    }
  

  redireccionarAlDetalle(idLibro: number): void {
    this.router.navigate(['/detalle-producto', idLibro]);
  }
}
