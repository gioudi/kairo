using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAIROSV2.Business.Engines.Tests.Modelos
{
    public class Permisos
    {
        public static TUPermiso ObtenerPermisoInicial()
        {
            return new TUPermiso() {IdPermiso = 1 };
        }

        public static IEnumerable<TUPermiso> ObtenerListaPermisos()
        {
            return new List<TUPermiso>()
            {
                new TUPermiso()
                {
                    IdPermiso = 2,
                    IdPermisoPadre = 1
                },
                new TUPermiso()
                {
                    IdPermiso = 3,
                    IdPermisoPadre = 1
                },
                new TUPermiso()
                {
                    IdPermiso = 4,
                    IdPermisoPadre = 2
                },
                new TUPermiso()
                {
                    IdPermiso = 5,
                    IdPermisoPadre = 2
                },
                new TUPermiso()
                {
                    IdPermiso = 6,
                    IdPermisoPadre = 3
                },
                new TUPermiso()
                {
                    IdPermiso = 7,
                    IdPermisoPadre = 4
                }
            };
        }
    }
}
