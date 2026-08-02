import { SubCategoria } from "./subcategoria";

export interface Categorium {
    idCategoria: number;
    categoria1: string;
    subcategorias?: SubCategoria[];
  }
  