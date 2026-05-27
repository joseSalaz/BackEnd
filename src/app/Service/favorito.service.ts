import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, catchError, throwError } from 'rxjs';

import { environment } from '../../environments/environment';
import { Favorito } from '../Interface/favorito';


@Injectable({
  providedIn: 'root'
})
export class FavoritoService {
  private endPoint: string = environment.endPoint;
  private apiUrl: string = `${this.endPoint}Favorito`;
 

  constructor(private http: HttpClient) { 
    
  }
getFavoritosByPersona(idPersona: number): Observable<Favorito[]> {
    return this.http.get<Favorito[]>(`${this.apiUrl}/persona/${idPersona}`).pipe(
      catchError(this.handleError)
    );
  }
 
  checkIsFavorito(idPersona: number, idLibro: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/verificar?idPersona=${idPersona}&idLibro=${idLibro}`).pipe(
      catchError(this.handleError)
    );
  }
  addFavorito(favorito: Favorito): Observable<Favorito> {
    return this.http.post<Favorito>(this.apiUrl, favorito).pipe(
      catchError(this.handleError)
    );
  }

deleteFavoritoByPersonaAndLibro(idPersona: number, idLibro: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/${idPersona}/${idLibro}`).pipe(
    catchError(this.handleError)
  );
}
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Error desconocido';
    if (typeof window !== 'undefined' && error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Código de error ${error.status}: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
