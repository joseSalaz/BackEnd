export interface Libro {
    idLibro: number;
    titulo?: string;
    isbn?: number;
    tamanno?: string;
    descripcion?: string;
    condicion?: string;
    impresion?: string;
    tipoTapa?: string;
    estado?: boolean;
    idSubcategoria: number;
    idTipoPapel: number;
    idProveedor: number;
    imagen: string ;
    precioVenta?: number;
}

  
