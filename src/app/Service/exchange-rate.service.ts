import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ExchangeRateService {
  private apiUrl = ' https://v6.exchangerate-api.com/v6/79a35477976852f2d58f1364/latest/PEN';

  constructor(private http: HttpClient) { }

  getExchangeRate(): Observable<number> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => response.conversion_rates.USD)
    );
  }
}
