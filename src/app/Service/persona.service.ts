import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Persona } from '../Interface/persona';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PersonaService {
    private endPoint: string = environment.endPoint;
  private apiUrl: string = `${this.endPoint}Persona`;
  constructor(private http: HttpClient) {}

 actualizarPersona(persona: Persona): Observable<Persona> {
  return this.http.put<Persona>(`${this.apiUrl}`, persona);
}

  obtenerPersonaPorId(id: number): Observable<Persona> {
    return this.http.get<Persona>(`${this.apiUrl}/${id}`);
  }
}
