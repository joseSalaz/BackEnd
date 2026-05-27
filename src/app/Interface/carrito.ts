import { Libro } from "./libro";

export interface ItemCarrito {
    libro: Libro;
    precioVenta: number;
    cantidad: number;
  }