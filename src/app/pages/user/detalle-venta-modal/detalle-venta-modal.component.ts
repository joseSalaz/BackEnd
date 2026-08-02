import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DetalleVenta } from '../../../Interface/detalle_venta';


@Component({
  selector: 'app-detalle-venta-modal',
  templateUrl: './detalle-venta-modal.component.html',
  styleUrls: ['./detalle-venta-modal.component.scss']
})
export class DetalleVentaModalComponent {
  @Input() detallesVenta: DetalleVenta[] = [];
  @Output() verEstado = new EventEmitter<number>();

  mostrarModal = false;

  abrirModal(): void {
    this.mostrarModal = true;
    document.body.style.overflow = 'hidden';
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    document.body.style.overflow = '';
  }

  cerrarModalAlFondo(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('modal-overlay')) {
      this.cerrarModal();
    }
  }

  irAEstadoPedido(idDetalleVentas: number | undefined): void {
    if (idDetalleVentas) {
      this.verEstado.emit(idDetalleVentas);
    }
  }


}