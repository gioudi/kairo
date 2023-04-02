using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data
{
    public class TerminalesProductosRecetasRepository : DataRepositoryBase<TTerminalesProductosReceta>, ITerminalesProductosRecetasRepository
    {
        public void RemoveAllByProductTerminal(string idProducto, string idTerminal)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($"DELETE FROM [T_Terminales_Productos_Recetas] WHERE Id_Producto = {idProducto} AND Id_Terminal = {idTerminal}");
            }
        }

        protected override TTerminalesProductosReceta GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TTerminalesProductosReceta UpdateEntity(KAIROSV2DBContext entityContext, TTerminalesProductosReceta entity)
        {
            throw new NotImplementedException();
        }
        public void AddProductsRecipesTerminal(IEnumerable<TTerminalesProductosReceta> entities)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TTerminalesProductosRecetaSet.AddRange(entities);
                entityContext.SaveChanges();
            }
        }

        public IEnumerable<TTerminalesProductosReceta> Get(params string[] includes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var query = entityContext.TTerminalesProductosRecetaSet.AsQueryable();
                foreach (string include in includes)
                {
                    query = query.Include(include);
                };

                return query.ToList();
            }
        }
    }
}
