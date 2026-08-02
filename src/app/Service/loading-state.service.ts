import { Injectable } from '@angular/core';
import { signal } from '@angular/core'; 
import { LoadingState } from '../Interface/loading-state.enum';


@Injectable({
  providedIn: 'root',
})
export class LoadingStateService {
  private loadingSignal = signal<LoadingState>(LoadingState.Waiting); // Estado inicial es 'waiting'

  get loading$() {
    return this.loadingSignal;
  }

  // Método para cambiar el estado de carga
  setLoading(state: LoadingState): void {
    this.loadingSignal.set(state); 
  }
}
