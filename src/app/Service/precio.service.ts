import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Precio } from '../Interface/precio';

@Injectable({
  providedIn: 'root'
})
export class PrecioService {

  private endPoint: string = environment.endPoint;
  private apiUrl: string = `${this.endPoint}Libro`;
  // private precioSubjet: BehaviorSubject<Precio[]> = new BehaviorSubject<Precio[]>([]);
  constructor(
    private http : HttpClient
    ) 
    {}

    getPrecios() : Observable<Precio[]>{
      return this.http.get<Precio[]>(`${this.apiUrl}`)
    }

    obtenerPreciosLibro(libroId: string): Observable<Precio[]> {
      const url = `${this.apiUrl}/${libroId}/precios-objetivos`; // URL correcta para obtener los precios relacionados con un libro
      return this.http.get<Precio[]>(url);
    }

}
