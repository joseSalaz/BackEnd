import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LibroAutor } from '../Interface/libro_autor';
import { environment } from '../../environments/environment';
import { Autor } from '../Interface/autor';

@Injectable({
  providedIn: 'root'
})
export class LibroAutorService {
  private apiUrl: string = `${environment.endPoint}LibroAutor`;

  constructor(private http: HttpClient) {}

  // Obtener los autores asociados a un libro por su ID
  getAutoresDeLibro(idLibro: number): Observable<Autor[]> {
    const url = `${this.apiUrl}/GetAutoresByLibroId/${idLibro}`;
    return this.http.get<Autor[]>(url);
  }
  // Obtener los detalles de un autor por su ID
  getAutorPorId(idAutor: number): Observable<any> {
    const url = `${environment.endPoint}Autor/${idAutor}`;
    return this.http.get<any>(url).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Error desconocido';
        if (error.error instanceof ErrorEvent) {
          // Error del cliente
          errorMessage = `Error: ${error.error.message}`;
        } else {
          // Error del servidor
          errorMessage = `Código de error ${error.status}: ${error.error}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
      })
    );
  }
}
