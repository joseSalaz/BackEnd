import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, catchError, throwError } from 'rxjs';
import { Libro } from '../Interface/libro';
import { environment } from '../../environments/environment';
import { Precio } from '../Interface/precio';
import { Kardex } from '../Interface/kardex';

@Injectable({
  providedIn: 'root'
})
export class LibroService {
  private endPoint: string = environment.endPoint;
  private apiUrl: string = `${this.endPoint}Libro`;
  private librosSubject: BehaviorSubject<Libro[]> = new BehaviorSubject<Libro[]>([]);
  public libros$: Observable<Libro[]> = this.librosSubject.asObservable();

  constructor(private http: HttpClient) { 
    this.fetchLibros();
  }

  getLibros(): Observable<Libro[]> {
    return this.libros$;
  }

  getLibroPorId(id: string): Observable<Libro> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Libro>(url).pipe(
      catchError(this.handleError)
    );
  }

  private fetchLibros(): void {
    this.http.get<Libro[]>(`${this.apiUrl}`).pipe(
      catchError(this.handleError)
    ).subscribe(libros => {
      this.librosSubject.next(libros);
    });
  }

  getLibrosPorSubcategoria(idSubcategoria: number): Observable<Libro[]> {
    return this.http.get<Libro[]>(`${this.apiUrl}?idSubcategoria=${idSubcategoria}`).pipe(
      catchError(this.handleError)
    );
  }

  getLibrosPorPrecio(id: string): Observable<Libro[]> {
    return this.http.get<Libro[]>(`${this.apiUrl}/Precio/?idPrecio=${id}`).pipe(
      catchError(this.handleError)
    );
  }

  getPreciosPorIdLibro(libroId: number): Observable<Precio[]> {
    const url = `${this.apiUrl}/precios/${libroId}`;
    return this.http.get<Precio[]>(url).pipe(
      catchError(this.handleError)
    );
  }

  getKardexPorIdLibro(libroId: number): Observable<Kardex> {
    return this.http.get<Kardex>(`${this.apiUrl}/kardex/${libroId}`).pipe(
      catchError(this.handleError)
    );
  }

  getLibrosAutoComplete(titulo: string): Observable<Libro[]> {
    return this.http.get<Libro[]>(`${this.apiUrl}/filtroComplete?titulo=${encodeURIComponent(titulo)}`).pipe(
      catchError(this.handleError)
    );
  }

  /**
   * Manejo de errores mejorado para evitar fallos en SSR
   */
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Error desconocido';

    // ✅ Asegurar que ErrorEvent solo se usa en el navegador
    if (typeof window !== 'undefined' && error.error instanceof ErrorEvent) {
      // Error en la red (cliente)
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Error del servidor
      errorMessage = `Código de error ${error.status}: ${error.message}`;
    }

    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
