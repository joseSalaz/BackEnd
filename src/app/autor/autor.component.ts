import { Component } from '@angular/core';
import { AutorService } from '../Service/autor.service';
import { Autor } from '../Interface/autor';
import { LibroService } from '../Service/libro.service';
import { Libro } from '../Interface/libro';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-autor',
  templateUrl: './autor.component.html',
  styleUrls: ['./autor.component.scss']
})
export class AutorComponent {

  dataSource: Autor[] = []; // Declaración de la propiedad dataSource como un array de tipo Autor
  datas: Libro[] = [];

  constructor(
    private _autorServicio: AutorService,
    private _libroServicio: LibroService
  ) {
    // this.mostrarAutor();
    this.mostrarLibro();
  }

  mostrarLibro() {

    this._libroServicio.getLibros().subscribe({
      next: (data: Libro[]) => {
        this.datas = data;
      },
      error: (err) => {
        console.log("error", err);
      },
      complete: () => {
        // Hacer algo
      },
    });
  }

  // mostrarLibro(): void {
  //   this._libroServicio.getLibros().subscribe(element => {
  //     this.datas = element;
  //   })
  // }
}


