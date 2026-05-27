import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Venta } from '../Interface/venta';
import { environment } from '../../environments/environment';
import { DetalleVenta } from '../Interface/detalle_venta';
import { EstadoPedido } from '../Interface/estado_pedido';

@Injectable({
  providedIn: 'root'
})
export class VentaService {
  private endPoint: string = environment.endPoint;
  private apiUrl: string = `${this.endPoint}Venta`;

  constructor(private http: HttpClient) { }

  obtenerVentasPorPersona(idPersona: number): Observable<Venta[]> {
    return this.http.get<Venta[]>(`${this.apiUrl}/persona/${idPersona}`);
  }

  obtenerDetallesVenta(idVenta: number): Observable<DetalleVenta[]> {
    return this.http.get<DetalleVenta[]>(`${this.apiUrl}/detalle/${idVenta}`);
  }

  obtenerEstadoPedido(idDetalleVenta: number): Observable<EstadoPedido> {
    return this.http.get<EstadoPedido>(`${this.apiUrl}/estado/${idDetalleVenta}`);
  }

  obtenerVentaConPersonaYDireccion(idVenta: number): Observable<Venta> {
    return this.http.get<Venta>(`${this.apiUrl}/detallePersona/${idVenta}`);
  }
}
