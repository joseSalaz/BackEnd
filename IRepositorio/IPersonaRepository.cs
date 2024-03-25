﻿using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface IPersonaRepository : ICRUDRepositorio<Persona>
    {
        //Persona buscarporDNI(string dni);
        Persona GetByTipoNroDocumento(string TipoDocumento, string NumeroDocumento);

        
    }
    
}
