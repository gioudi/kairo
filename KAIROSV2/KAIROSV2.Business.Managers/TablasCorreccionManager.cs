using System;
using System.Collections.Generic;
using System.Text;
using KAIROSV2.Business.Contracts.Managers;
using KAIROSV2.Business.Entities;
using KAIROSV2.Data.Contracts;
using System.Linq;
using KAIROSV2.Business.Contracts.Engines;
using System.Threading.Tasks;
using KAIROSV2.Business.Entities.DTOs;

namespace KAIROSV2.Business.Managers
{
    public class TablasCorreccionManager : ITablasCorreccionManager
    {
        private readonly ITablasCorreccionRepository _tablasCorreccionRepository;
        private readonly ITablasCorreccionEngine _tablasCorreccionEngine;

        public TablasCorreccionManager(ITablasCorreccionRepository tablasCorreccionRepository, ITablasCorreccionEngine tablasCorreccionEngine)
        {
            _tablasCorreccionRepository = tablasCorreccionRepository;
            _tablasCorreccionEngine = tablasCorreccionEngine;
        }

        public DatatableResult ObtenerCorrecion5b(int skip, int pageSize, string sortExpression, IEnumerable<SearchDataValue> searchDataValues)
        {
            IEnumerable<TApiCorreccion5b> result = null;
            string error = string.Empty;
            int totalRecords = 0;

            try
            {
                var query = _tablasCorreccionEngine.GetQueryStringTablas(searchDataValues);
                result = _tablasCorreccionRepository.GetAPICorrecion5b(skip, pageSize, sortExpression, query, out totalRecords);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            
            return new DatatableResult()
            {
                data = result,
                draw = 1,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                error = error
            };
        }

        public DatatableResult ObtenerCorrecion6b(int skip, int pageSize, string sortExpression, IEnumerable<SearchDataValue> searchDataValues)
        {
            IEnumerable<TApiCorreccion6b> result = null;
            string error = string.Empty;
            int totalRecords = 0;

            try
            {
                var query = _tablasCorreccionEngine.GetQueryStringTablas(searchDataValues);
                result = _tablasCorreccionRepository.GetAPICorrecion6b(skip, pageSize, sortExpression, query, out totalRecords);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return new DatatableResult()
            {
                data = result,
                draw = 1,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                error = error
            };
        }

        public DatatableResult ObtenerCorrecion6cAlcohol(int skip, int pageSize, string sortExpression, IEnumerable<SearchDataValue> searchDataValues)
        {
            IEnumerable<TApiCorreccion6cAlcohol> result = null;
            string error = string.Empty;
            int totalRecords = 0;

            try
            {
                var query = _tablasCorreccionEngine.GetQueryStringTablas(searchDataValues);
                result = _tablasCorreccionRepository.GetAPICorrecion6cAlcohol(skip, pageSize, sortExpression, query, out totalRecords);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return new DatatableResult()
            {
                data = result,
                draw = 1,
                recordsFiltered = totalRecords,
                recordsTotal = totalRecords,
                error = error
            };
        }
    }
}
