import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { EstadoPedido, EstadoPedidoImagen } from '../../../Interface/estado_pedido';
import { VentaService } from '../../../Service/venta.service';

@Component({
  selector: 'app-detalle-pedido',
  templateUrl: './detalle-pedido.component.html',
  styleUrls: ['./detalle-pedido.component.scss']
})
export class EstadoPedidoModalComponent implements OnInit {
  @Input() idDetalleVenta: number | null = null;
  @Output() cerrar = new EventEmitter<void>();

  estadoPedido: EstadoPedido | null = null;
  imagenesEstado: EstadoPedidoImagen[] = [];
  mostrarModal = false;

  constructor(private ventaService: VentaService) { }

  ngOnInit(): void {
    if (this.idDetalleVenta) {
      this.mostrarModal = true;
      document.body.style.overflow = 'hidden';
      this.obtenerEstadoPedido(this.idDetalleVenta);  // ← abre primero, carga después
    }
  }

  obtenerEstadoPedido(idDetalleVenta: number): void {
    this.cargando = true;                             // ← activa spinner
    this.ventaService.obtenerEstadoPedido(idDetalleVenta).subscribe({
      next: (estado) => {
        this.estadoPedido = estado;
        this.imagenesEstado = estado.estadoPedidoImagenes || [];
        this.cargando = false;                        // ← desactiva spinner
      },
      error: (error) => {
        console.error('Error al obtener estado del pedido:', error);
        this.cargando = false;
      }
    });
  }

  cargando = false;

  cerrarModal(): void {
    this.mostrarModal = false;
    document.body.style.overflow = '';
    this.cerrar.emit();
  }

  cerrarModalAlFondo(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.cerrarModal();
    }
  }
}