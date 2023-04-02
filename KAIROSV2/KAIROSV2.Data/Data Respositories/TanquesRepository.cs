using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KAIROSV2.Data
{
    public class TanquesRepository : DataRepositoryBase<TTanque>, ITanquesRepository
    {
        protected override TTanque GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            var query = (from e in entityContext.TTanqueSet
                         where e.IdTanque == id.ToString()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override TTanque UpdateEntity(KAIROSV2DBContext entityContext, TTanque entity)
        {
            return (from e in entityContext.TTanqueSet
                    where e.IdTanque == entity.IdTanque
                    select e).FirstOrDefault();
        }

        public IEnumerable<TTanque> ObtenerTodas(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTanqueSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }

        public IEnumerable<TTanque> ObtenerTodas()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTanqueSet.ToList();
            }
        }

        public async Task<IEnumerable<TTanque>> ObtenerTodasAsync()
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTanqueSet.ToList();
            }
        }

        public IEnumerable<TTanquesEstado> ObtenerEstadosTanque()
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTanquesEstadoSet.ToList();
            }
        }

        public TTanque Obtener(string IdTanque, string IdTerminal, params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTanqueSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.FirstOrDefault(e => e.IdTanque == IdTanque && e.IdTerminal == IdTerminal);
            }
        }

        public TTanque Obtener(string IdTanque, string IdTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTanqueSet.Where(e => e.IdTanque == IdTanque && e.IdTerminal == IdTerminal).FirstOrDefault();
            }
        }

        public async Task<TTanque> ObtenerAsync(string IdTanque, string IdTerminal)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTanqueSet.Where(e => e.IdTanque == IdTanque && e.IdTerminal == IdTerminal).FirstOrDefault();
            }
        }        

        public bool Existe(string IdTanque , string IdTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTanqueSet.Any(e => e.IdTanque == IdTanque && e.IdTerminal == IdTerminal);
            }
        }

        public async Task<bool> ExisteAsync(string IdTanque, string IdTerminal)
        {
            await using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TTanqueSet.Any(e => e.IdTanque == IdTanque && e.IdTerminal == IdTerminal);
            }
        }

        public void BorrarPantallaFlotante(string IdTanque , string IdTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($"DELETE FROM [T_Tanques_Pantalla_Flotante] WHERE Id_Tanque = {IdTanque} AND Id_Terminal = {IdTerminal} ");
            }
        }

    }
}
