using LightCore.Common.Contracts;
using LightCore.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public class EntityBaseKairos : EntityBase
    {
        
        public EntityBaseKairos(ICurrentClaimsPrincipal currentClaimsPrincipal) : base(currentClaimsPrincipal)
        {
        }
    }
}
