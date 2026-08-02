<<<<<<< HEAD
using DBModel.DB;
=======
﻿using DBModel.DB;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DireccionRepository : GenericRepository<Direccion>, IDireccionRepository
    {
<<<<<<< HEAD
        public DireccionRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        public List<Direccion> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Direccion>> GetDireccionesPorUsuario(int idPersona)
        {
            return await dbSet
                .Where(d => d.IdPersona == idPersona)
                .ToListAsync();
        }

        public void SetDireccionPredeterminada(int idDireccion)
        {
            var direccion = dbSet.Find(idDireccion);
            if (direccion == null) return;

            var direccionesUsuario = dbSet.Where(d => d.IdPersona == direccion.IdPersona).ToList();
            foreach (var dir in direccionesUsuario)
            {
                dir.EsPredeterminada = dir.IdDireccion == idDireccion;
            }

            db.SaveChanges();
        }

        public bool ExisteDireccionEnVenta(int idDireccion)
        {
            return db.Ventas.Any(v => v.IdDireccion == idDireccion);
        }
    }
}
