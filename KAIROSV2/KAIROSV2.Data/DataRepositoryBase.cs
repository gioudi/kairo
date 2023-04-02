using LightCore.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, KAIROSV2DBContext>
        where T : class, new()
    {

    }
}
