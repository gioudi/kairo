using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Contracts.Managers
{
    public interface ITablasCorreccionManager
    {
        public DatatableResult ObtenerCorrecion5b(int skip, int pageSize, string sortExpression, IEnumerable<SearchDataValue> searchDataValues);
        public DatatableResult ObtenerCorrecion6b(int skip, int pageSize, string sortExpression, IEnumerable<SearchDataValue> searchDataValues);
        public DatatableResult ObtenerCorrecion6cAlcohol(int skip, int pageSize, string sortExpression, IEnumerable<SearchDataValue> searchDataValues);
    }
}
