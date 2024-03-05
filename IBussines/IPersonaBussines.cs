﻿using DBModel.DB;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IBussines
{
    public interface IPersonaBussines : ICRUDBussnies<PersonaRequest, PersonaResponse>
    {
        Persona GetByTipoNroDocumento(string tipoDocumento, string NroDocumento);
    }
}
