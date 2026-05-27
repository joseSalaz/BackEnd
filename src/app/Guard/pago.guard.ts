import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { CarroService } from '../Service/carro.service';


@Injectable({
  providedIn: 'root'
})
export class PagoGuard implements CanActivate {

  constructor(private carroService: CarroService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    // Verifica si hay libros en el carro
    const librosEnCarro = this.carroService.obtenerCantidadProductos();
    
    // Si hay libros en el carro, permite la navegación al componente "Pago"
    if (librosEnCarro > 0) {
      return true;
    } else {
      // Si no hay libros en el carro, redirige al usuario a otra página (por ejemplo, la página de inicio)
      this.router.navigate(['/inicio']); // Cambia '/inicio' por la ruta que desees
      return false;
    }
  }
}
