import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  isLocalStorageAvailable(): boolean {
    return typeof localStorage !== 'undefined';
  }

  getItem<T>(key: string): T | null {
    if (!this.isLocalStorageAvailable()) return null;
    const data = localStorage.getItem(key);
    return data ? JSON.parse(data) : null;
  }

  setItem<T>(key: string, value: T): void {
    if (this.isLocalStorageAvailable()) {
      localStorage.setItem(key, JSON.stringify(value));
    }
  }

  removeItem(key: string): void {
    if (this.isLocalStorageAvailable()) {
      localStorage.removeItem(key);
    }
  }

  clear(): void {
    if (this.isLocalStorageAvailable()) {
      localStorage.clear();
    }
  }
}
