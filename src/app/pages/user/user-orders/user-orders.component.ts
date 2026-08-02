import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { VentaService } from '../../../Service/venta.service';
import { Venta } from '../../../Interface/venta';

@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.scss']
})
export class UserOrdersComponent implements OnInit {
  @Input() usuarioId!: number;
  @Output() onVerDetalles = new EventEmitter<number>();

  ventas: Venta[] = [];

  constructor(private ventaService: VentaService) { }

  ngOnInit(): void {
    if (this.usuarioId) {
      this.obtenerVentas();
    }
  }

  obtenerVentas(): void {
    this.ventaService.obtenerVentasPorPersona(this.usuarioId).subscribe({
      next: (ventas) => {
        this.ventas = ventas;
      },
      error: (error) => console.error('Error al obtener ventas:', error)
    });
  }

  abrirModalVenta(idVenta: number): void {
    // Emite el ID al componente padre para que orqueste la apertura del modal global
    this.onVerDetalles.emit(idVenta);
  }
}