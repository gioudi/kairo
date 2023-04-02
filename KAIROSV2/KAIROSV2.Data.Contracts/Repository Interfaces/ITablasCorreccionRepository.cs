using KAIROSV2.Business.Entities;
using LightCore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Data.Contracts
{
    public interface ITablasCorreccionRepository : IDataRepository<TApiCorreccion5b>
    {
        IEnumerable<TApiCorreccion5b> GetAPICorrecion5b(int skip, int pageSize, string sortExpression, string searchQuery, out int totalRecords);
        IEnumerable<TApiCorreccion6b> GetAPICorrecion6b(int skip, int pageSize, string sortExpression, string searchQuery, out int totalRecords);
        IEnumerable<TApiCorreccion6cAlcohol> GetAPICorrecion6cAlcohol(int skip, int pageSize, string sortExpression, string searchQuery, out int totalRecords);
        double GetCorrecion5b(double ApiObservado, double Temperatura);
        bool ExisteCorrecion5b(double ApiObservado, double Temperatura);
        public double GetCorrecion6b(double ApiCorregido, double Temperatura);
        bool ExisteCorrecion6b(double ApiCorregido, double Temperatura);
        public double GetCorrecion6CAlcohol(double ApiCorregido, double Temperatura);
        bool ExisteCorrecion6CAlcohol(double ApiCorregido, double Temperatura);
    }
}
