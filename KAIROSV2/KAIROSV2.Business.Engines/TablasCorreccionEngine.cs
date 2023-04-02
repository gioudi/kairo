using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Engines
{
    public class TablasCorreccionEngine : ITablasCorreccionEngine
    {
        public string GetQueryStringTablas(IEnumerable<SearchDataValue> search)
        {
            var query = string.Empty;

            foreach (var data in search)
            {
                if (double.TryParse(data.Value, out double result))
                {
                    if (string.IsNullOrEmpty(query))
                        query += $"{data.Property} == {result}";
                    else
                        query += $" && {data.Property} == {result}";
                }
                else
                    throw new ArgumentException($"El valor para ({data.Property}) no tiene un formato correcto");
            }

            return query;
        }
    }
}
