import { DetalleVenta } from "./detalle_venta";

export interface EstadoPedido {
    idEstadoPedido: number;
    idDetalleVentas: number;
    estado: string;
    fechaEstado: Date;
    comentario?: string;
    estadoPedidoImagenes: EstadoPedidoImagen[];
    idDetalleVentasNavigation?: DetalleVenta;
  }
  
  export interface EstadoPedidoImagen {
    idEstadoPedidoImagen: number;
    idEstadoPedido: number;
    urlImagen?: string;
    estado?: string;
    fecha?: Date;
    idEstadoPedidoNavigation?: EstadoPedido;
  }