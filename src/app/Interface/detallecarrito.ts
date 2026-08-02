import { ItemCarrito } from "./carrito";
import { Persona } from "./persona";

export interface Datallecarrito{
    Items:ItemCarrito[],
    TotalAmount:number,
    Persona:Persona,
    IdDireccion: number;
}