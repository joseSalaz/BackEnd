import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DetalleVenta } from '../Interface/detalle_venta';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DetalleVentaService {
  private endPoint:string = environment.endPoint;
  private apiUrl:string = this.endPoint + "DetalleVenta";
  constructor(private http: HttpClient) { 

  }
  getDetalleVentaporPersonaId(idPersona: number): Observable<DetalleVenta[]> {
    return this.http.get<DetalleVenta[]>(`${this.apiUrl}/traer/${idPersona}`);
  } 
}
