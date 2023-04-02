using KAIROSV2.Business.Contracts.Engines;
using KAIROSV2.Business.Entities;
using KAIROSV2.Business.Entities.DTOs;
using KAIROSV2.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Engines
{
    public class DespachosEngine : IDespachosEngine
    {
        private readonly IDespachosRepository _despachosRepository;
        private readonly ITASCortesRepository _ITASCortesRepository;
        private readonly IProductosRepository _IProductosRepository;
        private readonly ICompañiasRepository _ICompañiasRepository;
        private readonly ITablasCorreccionRepository _TablasCorreccionRepository;


        public DespachosEngine(IDespachosRepository despachosRepository , ITASCortesRepository ITASCortesRepository , IProductosRepository productosRepository , ICompañiasRepository CompañiasRepository , ITablasCorreccionRepository TablasCorreccionRepository)
        {
            _despachosRepository = despachosRepository;
            _ITASCortesRepository = ITASCortesRepository;
            _IProductosRepository = productosRepository;
            _ICompañiasRepository = CompañiasRepository;
            _TablasCorreccionRepository = TablasCorreccionRepository;
        }

        public IEnumerable<DespachosConsolidadosDetalleDTO> CalcularDespachosConsolidadosDetalle( string Id_Terminal , string Id_Compañia , string Id_Producto ,  DateTime FechaCorte)
        {                        

            var DespachosPonderados = new List<DespachosConsolidadosDetalleDTO>();
            var DespachosPonderadosAgrupados = new List<DespachosConsolidadosDetalleDTO>();

            // Obtener Despachos Componentes con el filtro
            var despachosComponentes = ObtenerDespachosComponentes(Id_Terminal , Id_Compañia , Id_Producto,  FechaCorte);

            if (despachosComponentes.Count() > 0)
            {
                //Obtener Productos y Compañias de Kairos
                var productos = _IProductosRepository.Get(new string[] { "TProductosAtributos" });
                var Compañias = _ICompañiasRepository.ObtenerTodas();

                // Recorrer cada Componente de los despachos
                foreach (var Producto in despachosComponentes)
                {

                    // Obtener Encabezado del componente actual
                    var DespachoEncabezado = _despachosRepository.Obtener(Producto.Id_Despacho);

                    //Valida que el despacho este Activo
                    if (DespachoEncabezado.Estado_Kairos)
                    {
                        // Sumar el total despachado del producto actual
                        var VolumenTotalProducto = despachosComponentes.Where(e => e.Id_Producto_Componente == Producto.Id_Producto_Componente).Sum(v => Convert.ToDouble(v.Volumen_Bruto));

                        // Calculos de pesos ponderados para cada componente del despacho 
                        var Ponderado = new DespachosConsolidadosDetalleDTO()
                        {
                            IdCompañia = DespachoEncabezado.Id_Compañia,
                            Compañia = Compañias.Any(e => e.IdCompañia == DespachoEncabezado.Id_Compañia) ? Compañias.First(e => e.IdCompañia == DespachoEncabezado.Id_Compañia).Nombre : DespachoEncabezado.Id_Compañia,
                            Fecha = DespachoEncabezado.Fecha_Final_Despacho,
                            IdProducto = Producto.Id_Producto_Componente,
                            Producto = productos.Any(e => e.IdProducto == Producto.Id_Producto_Componente) ? productos.First(e => e.IdProducto == Producto.Id_Producto_Componente).NombreCorto : Producto.Id_Producto_Componente,
                            VolumenUnitarioBruto = Math.Round(Producto.Volumen_Bruto, 1),
                            PorcentajePonderado = Producto.Volumen_Bruto / VolumenTotalProducto,
                            TemperaturaPonderada = Math.Round((Math.Round(Producto.Temperatura, 2) * (Producto.Volumen_Bruto / VolumenTotalProducto)), 2),
                            DensidadPonderada = Math.Round((Math.Round(Producto.Densidad, 2) * (Producto.Volumen_Bruto / VolumenTotalProducto)), 2),
                            //Factor = Math.Round(factor , 5),
                            //VolumenUnitarioNeto = Math.Round( Producto.Volumen_Bruto * factor, 1)
                        };

                        DespachosPonderados.Add(Ponderado);
                    }
                }


                // Sumatoria de las Temperatura , Densidades y Volumen Bruto 
                DespachosPonderadosAgrupados = DespachosPonderados.GroupBy(e => e.IdProducto).
                                                        Select(l => new DespachosConsolidadosDetalleDTO()
                                                        {
                                                            VolumenUnitarioBruto = Math.Round(l.Sum(e => e.VolumenUnitarioBruto), 2),
                                                            Fecha = l.First().Fecha,
                                                            IdCompañia = l.First().IdCompañia,
                                                            Compañia = l.First().Compañia,
                                                            IdProducto = l.First().IdProducto,
                                                            Producto = l.First().Producto,
                                                            TemperaturaPonderada = Math.Round( l.Sum(e => e.TemperaturaPonderada),2),
                                                            DensidadPonderada = Math.Round( l.Sum(e => e.DensidadPonderada),2),
                                                            Factor = 1,
                                                            VolumenUnitarioNeto = 0

                                                        }).ToList();

                var TablaCorrecion = "T_API_Correccion_6B";
                double factor = 1;

                // Buscar en las tablas de correccion del producto , con la conbinacion Temperatura Densidad
                for (var i = 0; i < DespachosPonderadosAgrupados.Count(); i++)
                {
                    var Producto = DespachosPonderadosAgrupados[i];

                    //Consultar el atributo Tabla_Correccion del producto 
                    if (productos.First(e => e.IdProducto == Producto.IdProducto).TProductosAtributos.Any(i => i.IdAtributo == 10003))
                        TablaCorrecion = productos.First(e => e.IdProducto == Producto.IdProducto).TProductosAtributos.First(i => i.IdAtributo == 10003).ValorTexto;

                    switch (TablaCorrecion)
                    {
                        case "T_API_Correccion_5B":
                            factor = _TablasCorreccionRepository.ExisteCorrecion5b(Math.Round(Producto.DensidadPonderada, 1), Math.Round(Producto.TemperaturaPonderada, 1)) ? _TablasCorreccionRepository.GetCorrecion5b(Math.Round(Producto.DensidadPonderada, 1), Math.Round(Producto.TemperaturaPonderada, 1)) : 1;
                            break;
                        case "T_API_Correccion_6B":
                            factor = _TablasCorreccionRepository.ExisteCorrecion6b(Math.Round(Producto.DensidadPonderada, 1), Math.Round(Producto.TemperaturaPonderada, 1)) ? _TablasCorreccionRepository.GetCorrecion6b(Math.Round(Producto.DensidadPonderada, 1), Math.Round(Producto.TemperaturaPonderada, 1)) : 1;
                            break;
                        case "T_API_Correccion_6C_Alcohol":
                            var DensidadActual = Math.Round(Producto.DensidadPonderada, 1);
                            var DensidadCorregida = Math.Round(DensidadActual * 2, MidpointRounding.AwayFromZero) / 2;
                            factor = _TablasCorreccionRepository.ExisteCorrecion6CAlcohol(DensidadCorregida, Math.Round(Producto.TemperaturaPonderada, 1)) ? _TablasCorreccionRepository.GetCorrecion6CAlcohol(DensidadCorregida, Math.Round(Producto.TemperaturaPonderada, 1)) : 1;
                            break;
                    }

                    // Calcular el volumen neto
                    DespachosPonderadosAgrupados[i].Factor = Math.Round(factor, 5);
                    DespachosPonderadosAgrupados[i].VolumenUnitarioNeto = Math.Round( Math.Round(factor, 5) * DespachosPonderadosAgrupados[i].VolumenUnitarioBruto,1);
                }
            }

            return DespachosPonderadosAgrupados;
        }

        public IEnumerable<TDespachosComponente> ObtenerDespachosComponentes(string Id_Terminal, string Id_Compañia, string Id_Producto , DateTime FechaCorte)
        {
            if (_ITASCortesRepository.ExisteFechaCortePorTerminal(FechaCorte , Id_Terminal))
            {
                var idCorte = _ITASCortesRepository.ObtenerIdsPorFechaCorte(FechaCorte).ToList();

                if (Id_Compañia != "TODAS")

                    return _despachosRepository.ObtenerDespachosComponentesPorIds(_despachosRepository.ObtenerIdsDespachosTerminalCompañiaProductoCorte(Id_Terminal, Id_Compañia, Id_Producto ,idCorte));
                else
                    return _despachosRepository.ObtenerDespachosComponentesPorIds(_despachosRepository.ObtenerIdsDespachosTerminalCorte(Id_Terminal, idCorte));
            }
            else
            {
                if (Id_Compañia != "TODAS")

                    return _despachosRepository.ObtenerDespachosComponentesPorIds(_despachosRepository.ObtenerIdsDespachosTerminalCompaniaProducto(Id_Terminal, Id_Compañia, Id_Producto));
                else
                    return _despachosRepository.ObtenerDespachosComponentesPorIds(_despachosRepository.ObtenerIdsDespachosTerminalProducto(Id_Terminal, Id_Producto));
            }
                
        }

        public IEnumerable<DespachosDetalladosDTO> ObtenerDespachosDetallados(string Id_Terminal, string Id_Compañia, DateTime FechaCorte)
        {
            IEnumerable<TDespacho> DespachosDetallados;

            // Si tiene un corte en la fecha seleccionada
            if (_ITASCortesRepository.ExisteFechaCortePorTerminal(FechaCorte , Id_Terminal))
            {
                // Consulta todos los Cortes con esta fecha de corte selecionada. incluye las fechas de cierre. 
                var CortesPorFechaCorte = _ITASCortesRepository.ObtenerTASCortesPorFechaCorte(FechaCorte);

                //Extrae las fechas de Cierre si las tiene 
                var FechasCieres = CortesPorFechaCorte.Where(e => e.Fecha_Cierre_Kairos != null).Select(e => e.Fecha_Cierre_Kairos).Distinct().ToList();

                List<long> IdsCortes = new List<long>();
                

                //Si hay Cortes con fechas de cierre
                if (FechasCieres.Count() > 0)
                {
                    var IdCortesPorFechasCierre = _ITASCortesRepository.ObtenerIdsPorFechasCierre(FechasCieres);
                    DespachosDetallados = _despachosRepository.ObtenerDespachosTerminalCompañiaCorte(Id_Terminal, Id_Compañia, IdCortesPorFechasCierre, "TDespachosComponentes");
                }
                // Si hay Cortes sin fecha de cierre 
                else
                {
                    var IdCortesPorFechaCorte = CortesPorFechaCorte.Select(e => e.Id_Corte).ToList();
                    DespachosDetallados = _despachosRepository.ObtenerDespachosTerminalCompañiaCorte(Id_Terminal, Id_Compañia, IdCortesPorFechaCorte, "TDespachosComponentes");
                }

                return ExtraerDespachosDetallados(DespachosDetallados);

            }
            // Si la fecha no tiene corte 
            else if (FechaCorte.Date <= DateTime.Now.Date)
            {
                var UltimaFechaCorte = _ITASCortesRepository.ObtenerUltimaFechaCorte(Id_Terminal);
                FechaCorte = FechaCorte.AddHours(23).AddMinutes(59).AddSeconds(59);
                DespachosDetallados = _despachosRepository.ObtenerDespachosTerminalCompañiaSinCorte(Id_Terminal, Id_Compañia, UltimaFechaCorte ,FechaCorte, "TDespachosComponentes");
                return ExtraerDespachosDetallados(DespachosDetallados);
            }
            else
                return new List<DespachosDetalladosDTO>();
        }

        public IEnumerable<DespachosDetalladosDTO> ObtenerDespachosDetallados(string Id_Terminal, List<string> Id_Compañia, DateTime FechaCorte)
        {
            IEnumerable<TDespacho> DespachosDetallados;

            if (_ITASCortesRepository.ExisteFechaCortePorTerminal(FechaCorte, Id_Terminal))
            {
                // Consulta todos los Cortes con esta fecha de corte selecionada. incluye las fechas de cierre. 
                var CortesPorFechaCorte = _ITASCortesRepository.ObtenerTASCortesPorFechaCorte(FechaCorte);

                //Extrae las fechas de Cierre si las tiene 
                var FechasCieres = CortesPorFechaCorte.Where(e => e.Fecha_Cierre_Kairos != null).Select(e => e.Fecha_Cierre_Kairos).Distinct().ToList();

                List<long> IdsCortes = new List<long>();
                

                //Si hay fechas de cierre
                if (FechasCieres.Count() > 0)
                {
                    var IdCortesPorFechasCierre = _ITASCortesRepository.ObtenerIdsPorFechasCierre(FechasCieres);
                    DespachosDetallados =_despachosRepository.ObtenerDespachosTerminalCompañiaCorte(Id_Terminal, Id_Compañia, IdCortesPorFechasCierre, "TDespachosComponentes");

                }
                else
                {
                    var IdCortesPorFechaCorte = CortesPorFechaCorte.Select(e => e.Id_Corte).ToList();
                    DespachosDetallados = (_despachosRepository.ObtenerDespachosTerminalCompañiaCorte(Id_Terminal, Id_Compañia, IdCortesPorFechaCorte, "TDespachosComponentes"));
                }

                return ExtraerDespachosDetallados(DespachosDetallados);

            }
            // Si la fecha no tiene corte 
            else if (FechaCorte.Date <= DateTime.Now.Date)
            {
                var UltimaFechaCorte = _ITASCortesRepository.ObtenerUltimaFechaCorte(Id_Terminal);
                FechaCorte = FechaCorte.AddHours(23).AddMinutes(59).AddSeconds(59);
                DespachosDetallados = _despachosRepository.ObtenerDespachosTerminalCompañiaSinCorte(Id_Terminal, Id_Compañia, UltimaFechaCorte , FechaCorte, "TDespachosComponentes");
                return ExtraerDespachosDetallados(DespachosDetallados);
            }
            else
                return new List<DespachosDetalladosDTO>();
        }

        public IEnumerable<DespachosConsolidadosDTO> ObtenerDespachosConsolidados(string Id_Terminal, string Id_Compañia, DateTime FechaCorte)
        {
            IEnumerable<TDespacho> DespachosConsolidados;

            if (_ITASCortesRepository.ExisteFechaCortePorTerminal(FechaCorte, Id_Terminal))
            {
                // Consulta todos los Cortes con esta fecha de corte selecionada. incluye las fechas de cierre. 
                var CortesPorFechaCorte = _ITASCortesRepository.ObtenerTASCortesPorFechaCorte(FechaCorte);

                //Extrae las fechas de Cierre si las tiene 
                var FechasCieres = CortesPorFechaCorte.Where(e => e.Fecha_Cierre_Kairos != null).Select(e => e.Fecha_Cierre_Kairos).Distinct().ToList();

                List<long> IdsCortes = new List<long>();
                
                //Si hay fechas de cierre
                if (FechasCieres.Count() > 0)
                {
                    var IdCortesPorFechasCierre = _ITASCortesRepository.ObtenerIdsPorFechasCierre(FechasCieres);
                    DespachosConsolidados = _despachosRepository.ObtenerDespachosTerminalCompañiaCorte(Id_Terminal, Id_Compañia, IdCortesPorFechasCierre, "TDespachosComponentes").Where(e => e.Estado_Kairos == true);
                }
                else
                {
                    var IdCortesPorFechaCorte = CortesPorFechaCorte.Select(e => e.Id_Corte).ToList();
                    DespachosConsolidados = _despachosRepository.ObtenerDespachosTerminalCompañiaCorte(Id_Terminal, Id_Compañia, IdCortesPorFechaCorte, "TDespachosComponentes").Where(e => e.Estado_Kairos == true);
                }

                    return ExtraerDespachos(DespachosConsolidados);
               
            }
            // Si la fecha no tiene corte 
            else if (FechaCorte.Date <= DateTime.Now.Date)
            {
                var UltimaFechaCorte = _ITASCortesRepository.ObtenerUltimaFechaCorte(Id_Terminal);
                FechaCorte = FechaCorte.AddHours(23).AddMinutes(59).AddSeconds(59);
                DespachosConsolidados = _despachosRepository.ObtenerDespachosTerminalCompañiaSinCorte(Id_Terminal, Id_Compañia, UltimaFechaCorte , FechaCorte, "TDespachosComponentes").Where(e => e.Estado_Kairos == true);
                return ExtraerDespachos(DespachosConsolidados);
            }
            else
                return new List<DespachosConsolidadosDTO>();
        }

        public IEnumerable<DespachosConsolidadosDTO> ObtenerDespachosConsolidados(string Id_Terminal, List<string> Id_Compañia, DateTime FechaCorte)
        {
            IEnumerable<TDespacho> DespachosConsolidados;

            if (_ITASCortesRepository.ExisteFechaCortePorTerminal(FechaCorte , Id_Terminal))
            {
                // Consulta todos los Cortes con esta fecha de corte selecionada. incluye las fechas de cierre. 
                var CortesPorFechaCorte = _ITASCortesRepository.ObtenerTASCortesPorFechaCorte(FechaCorte);

                //Extrae las fechas de Cierre si las tiene 
                var FechasCieres = CortesPorFechaCorte.Where(e => e.Fecha_Cierre_Kairos != null).Select(e => e.Fecha_Cierre_Kairos).Distinct().ToList();

                List<long> IdsCortes = new List<long>();
                

                //Si hay fechas de cierre
                if (FechasCieres.Count() > 0)
                {
                    var IdCortesPorFechasCierre = _ITASCortesRepository.ObtenerIdsPorFechasCierre(FechasCieres);
                    DespachosConsolidados =(_despachosRepository.ObtenerDespachosTerminalCompañiaCorte(Id_Terminal, Id_Compañia, IdCortesPorFechasCierre, "TDespachosComponentes").Where(e => e.Estado_Kairos == true));
                }
                else
                {
                    var IdCortesPorFechaCorte = CortesPorFechaCorte.Select(e => e.Id_Corte).ToList();
                    DespachosConsolidados = _despachosRepository.ObtenerDespachosTerminalCompañiaCorte(Id_Terminal, Id_Compañia, IdCortesPorFechaCorte, "TDespachosComponentes").Where(e => e.Estado_Kairos == true);
                }

                return ExtraerDespachos(DespachosConsolidados);
            }
            // Si la fecha no tiene corte 
            else if (FechaCorte.Date <= DateTime.Now.Date)
            {
                var UltimaFechaCorte = _ITASCortesRepository.ObtenerUltimaFechaCorte(Id_Terminal);
                FechaCorte = FechaCorte.AddHours(23).AddMinutes(59).AddSeconds(59);
                DespachosConsolidados = _despachosRepository.ObtenerDespachosTerminalCompañiaSinCorte(Id_Terminal, Id_Compañia, UltimaFechaCorte , FechaCorte, "TDespachosComponentes").Where(e => e.Estado_Kairos == true);
                return ExtraerDespachos(DespachosConsolidados);
            }
            else
                return new List<DespachosConsolidadosDTO>();
        }

        public IEnumerable<DespachosDetalladosDTO> ExtraerDespachosDetallados(IEnumerable<TDespacho> Despachos)
        {
            var productos = _IProductosRepository.Get(new string[] { "TProductosAtributos" });
            var Compañias = _ICompañiasRepository.ObtenerTodas();

            var DespachosDetallados = new List<DespachosDetalladosDTO>();
            foreach (var despacho in Despachos)
            {
                var nuevoDespacho = new DespachosDetalladosDTO()
                {
                    Id_Despacho = despacho.Id_Despacho,
                    Estado_Kairos = despacho.Estado_Kairos,
                    Fecha_Final_Despacho = despacho.Fecha_Final_Despacho,
                    Id_Compañia = Compañias.Any(e => e.IdCompañia == despacho.Id_Compañia) ? Compañias.First(e => e.IdCompañia == despacho.Id_Compañia).Nombre : despacho.Id_Compañia,
                    No_Orden = despacho.No_Orden,
                    Placa_Cabezote = despacho.Placa_Cabezote,
                    Placa_Trailer = despacho.Placa_Trailer,
                    Cedula_Conductor = despacho.Cedula_Conductor,
                    Id_Producto_Despacho = productos.Any(e => e.IdProducto == despacho.Id_Producto_Despacho) ? productos.First(e => e.IdProducto == despacho.Id_Producto_Despacho).NombreCorto : despacho.Id_Producto_Despacho,
                    Volumen_Cargado = despacho.Volumen_Cargado,
                    Volumen_Ordenado = despacho.Volumen_Ordenado,
                    Compartimento = despacho.Compartimento.ToString()


                };

                DespachosDetallados.Add(nuevoDespacho);
            }

            return DespachosDetallados;
        }

        public IEnumerable<DespachosConsolidadosDTO> ExtraerDespachos(IEnumerable<TDespacho> Despachos)
        {
            var productos = _IProductosRepository.Get(new string[] { "TProductosAtributos" });
            var Compañias = _ICompañiasRepository.ObtenerTodas();
            var DespachosConsolidadosAgrupados = new List<DespachosConsolidadosDTO>();

            var DespachosDetallados = new List<DespachosConsolidadosDTO>();
            foreach (var despacho in Despachos)
            {
                var nuevoDespacho = new DespachosConsolidadosDTO()
                {
                    Id_Producto = despacho.Id_Producto_Despacho,
                    Producto = productos.Any(e => e.IdProducto == despacho.Id_Producto_Despacho) ? productos.First(e => e.IdProducto == despacho.Id_Producto_Despacho).NombreCorto : despacho.Id_Producto_Despacho,
                    Volumen_Cargado = despacho.Volumen_Cargado,
                    Volumen_Ordenado = despacho.Volumen_Ordenado,
                    Diferencia = despacho.Volumen_Ordenado - despacho.Volumen_Cargado
                };

                DespachosDetallados.Add(nuevoDespacho);
            }

            // Sumatoria del Volumen Neto y Volumen Bruto 
            DespachosConsolidadosAgrupados = DespachosDetallados.GroupBy(e => e.Id_Producto).
                                                    Select(l => new DespachosConsolidadosDTO()
                                                    {
                                                        Volumen_Cargado = Math.Round(l.Sum(e => e.Volumen_Cargado),2),
                                                        Id_Producto = l.First().Id_Producto,
                                                        Producto = l.First().Producto,
                                                        Volumen_Ordenado = Math.Round(l.Sum(e => e.Volumen_Ordenado), 2),
                                                        Diferencia = Math.Round((l.Sum(e => e.Volumen_Ordenado)) - (l.Sum(e => e.Volumen_Cargado))),
                                                        

                                                    }).ToList();

            return DespachosConsolidadosAgrupados;
        }

        public bool TieneFechaCierre(DateTime FechaCorte , string Id_Terminal)
        {
            bool FechaEncontrada = false;
            // Si tiene un corte en la fecha seleccionada
            if (_ITASCortesRepository.ExisteFechaCortePorTerminal(FechaCorte , Id_Terminal))
            {
                // Consulta todos los Cortes con esta fecha de corte selecionada. incluye las fechas de cierre. 
                var CortesPorFechaCorte = _ITASCortesRepository.ObtenerTASCortesPorFechaCorte(FechaCorte);

                //Extrae las fechas de Cierre si las tiene 
                var FechasCieres = CortesPorFechaCorte.Where(e => e.Fecha_Cierre_Kairos != null).Select(e => e.Fecha_Cierre_Kairos).Distinct().ToList();

                if (FechasCieres.Any())
                    FechaEncontrada = true;
            }
            
            return FechaEncontrada;
        }

        public bool TieneFechaCierre(long? Id_Corte)
        {
            bool FechaEncontrada = false;

            //Extrae las fechas de Cierre si las tiene 
            var FechasCieres = _ITASCortesRepository.ObtenerTodas().Where(e => e.Id_Corte == Id_Corte && e.Fecha_Cierre_Kairos != null ).ToList();

            if (FechasCieres.Any())
                FechaEncontrada = true;


            return FechaEncontrada;
        }
    }
}
