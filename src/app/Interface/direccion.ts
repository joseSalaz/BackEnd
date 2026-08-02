export interface Direccion {
    idDireccion?: number; 
    idPersona: number;
    direccion1: string;
    referencia?: string;
    departamento: string;
    provincia: string;
    distrito: string;
    codigoPostal?: string;
    esPredeterminada: boolean;
    fechaCreacion?: Date;
  }
  