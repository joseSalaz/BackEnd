<<<<<<< HEAD
using DBModel.DB;
=======
﻿using DBModel.DB;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using IRepository;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TipoDocSalidaRepository : GenericRepository<TipoDocSalida>, ITipoDocSalidaRepository
    {
<<<<<<< HEAD
        public TipoDocSalidaRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        public List<TipoDocSalida> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
