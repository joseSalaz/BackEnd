import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, map } from 'rxjs';

import { Libro } from '../Interface/libro';
import { SubCategoria } from '../Interface/subcategoria';
@Injectable({
  providedIn: 'root'
})
export class SubCategoriaService {
    private endPoint: string = environment.endPoint;
    private apiUrl:string = this.endPoint + "Subcategoria";
  constructor(
    private http: HttpClient
    ){}

  getList(): Observable<SubCategoria[]> {
    return this.http.get<SubCategoria[]>(this.apiUrl);
  } 
  getLibrosPorSubCategoriaId(IdSubCategoria: number): Observable<Libro[]> {
    return this.http.get<Libro[]>(`${this.apiUrl}/librosbysubcategoria/${IdSubCategoria}`);
  }
  getSubCategoriaPorId(IdCategoria: number): Observable<SubCategoria> {
    const url = `${this.apiUrl}/${IdCategoria}`;
    return this.http.get<SubCategoria>(url);
  }
}

