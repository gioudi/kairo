using KAIROSV2.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Contracts.Engines
{
    public interface ITablasCorreccionEngine
    {
        string GetQueryStringTablas(IEnumerable<SearchDataValue> search);
    }
}
