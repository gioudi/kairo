using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KAIROSV2.Data
{
    public class RecetasRepository : DataRepositoryBase<TProductosReceta>, IRecetasRepository
    {
        protected override TProductosReceta GetEntity(KAIROSV2DBContext entityContext, object id)
        {
            throw new NotImplementedException();
        }

        protected override TProductosReceta UpdateEntity(KAIROSV2DBContext entityContext, TProductosReceta entity)
        {
            return (from e in entityContext.TProductosRecetaSet
                    where e.IdReceta == entity.IdReceta &&
                          e.IdProducto == entity.IdProducto
                    select e).FirstOrDefault();
        }

        public bool Exists(string idReceta, string idProducto)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                return entityContext.TProductosRecetaSet.Any(e => e.IdReceta == idReceta && e.IdProducto == idProducto);
            }
        }

        public void RemoveLeftover(string idProducto, params string[] idRecetas)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                var leftover = entityContext.TProductosRecetaSet.Where(e => idRecetas.All(r => r != e.IdReceta) &&
                                                                        e.IdProducto == idProducto)
                                                                .ToList();
                if (leftover.Count > 0)
                    entityContext.TProductosRecetaSet.RemoveRange(leftover);

                entityContext.SaveChanges();
            }
        }

        public void RemoveRecipesByProduct(string idProducto)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($@"DELETE FROM T_Productos_Recetas WHERE Id_Producto = {idProducto}");
            }
        }

        public void RemoveRecipeComponents(string idProducto, string idReceta)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.Database.ExecuteSqlInterpolated($@"DELETE FROM T_Productos_Recetas_Componentes WHERE Id_Receta = {idReceta} AND Id_Producto = {idProducto}");
            }
        }

        public void AddRecipeComponents(IEnumerable<TProductosRecetasComponente> componentes)
        {
            using (KAIROSV2DBContext entityContext = new KAIROSV2DBContext())
            {
                entityContext.TProductosRecetasComponenteSet.AddRange(componentes);
                entityContext.SaveChanges();
            }
        }
    }
}
