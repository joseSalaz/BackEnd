
﻿using DBModel.DB;
using IRepository;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DetalleDocSalidaRepository : GenericRepository<DetalleDocSalida>, IDetalleDocSalidaRepository
    {
        public DetalleDocSalidaRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }


        public List<DetalleDocSalida> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
