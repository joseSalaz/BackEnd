import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Direccion } from '../Interface/direccion';

@Injectable({
  providedIn: 'root'
})
export class DireccionService {
  private endPoint: string = `${environment.endPoint}Direccion`;

  constructor(private http: HttpClient) {}

  // Obtener direcciones del usuario
  getDireccionesByUsuario(idUsuario: number): Observable<Direccion[]> {
    return this.http.get<Direccion[]>(`${this.endPoint}/usuario/${idUsuario}`);
  }

  // Obtener una dirección por su ID
  getDireccionById(id: number): Observable<Direccion> {
    return this.http.get<Direccion>(`${this.endPoint}/${id}`);
  }

  // Crear una nueva dirección
  createDireccion(direccion: Direccion): Observable<Direccion> {
    return this.http.post<Direccion>(`${this.endPoint}`, direccion);
  }

  // Actualizar una dirección existente
  updateDireccion(direccion: Direccion): Observable<Direccion> {
    return this.http.put<Direccion>(`${this.endPoint}`, direccion);
  }

  // Establecer una dirección como predeterminada
  setDireccionPredeterminada(idDireccion: number): Observable<any> {
    return this.http.put(`${this.endPoint}/set-predeterminada/${idDireccion}`, {}, { responseType: 'text' });
  }

  // Eliminar una dirección (si no está vinculada a ventas)
  deleteDireccion(id: number): Observable<any> {
    return this.http.delete(`${this.endPoint}/${id}`, { responseType: 'text' });
  }
}
