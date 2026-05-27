import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CarroService } from '../../../Service/carro.service';
import { Libro } from '../../../Interface/libro';
import { ItemCarrito } from '../../../Interface/carrito';

@Component({
  selector: 'app-respuestas',
  templateUrl: './respuestas.component.html',
  styleUrl: './respuestas.component.scss'
})
export class RespuestasComponent {
  @Input() showModal: boolean = false;  // Variable para controlar la visibilidad del modal
  @Input() isSuccess: boolean = false;
  librosCarro: Libro[] = [];
  itemsCarrito: ItemCarrito[] = [];
  paymentId: string | null = null;
  payerId: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private carroService: CarroService,
    private router: Router,
  ) { }


  confirmarPago(): void {
    if (this.paymentId && this.payerId) {
      this.carroService.confirmarPago(this.paymentId, this.payerId).subscribe({
        next: (response: any) => {
          console.log('Pago confirmado con exito:', response);
          this.isSuccess = true;
          this.showModal = true;
        },
        error: (error: any) => {
          console.error('Error al confirmar el pago:', error);
          this.isSuccess = false;
          this.showModal = true;
        }
      });
    } else {
      console.error('Payment ID o Payer ID no estan disponibles.');
      this.isSuccess = false;
      this.showModal = true;
    }
  }

  redirigirAPedidos(): void {
    this.router.navigate(['/user'], { queryParams: { section: 'pedidos' } });
  }


  close() {
    this.showModal = false;
  }

}
