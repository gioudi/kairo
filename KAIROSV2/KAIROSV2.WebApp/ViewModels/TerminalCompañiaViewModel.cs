using KAIROSV2.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class TerminalCompañiaViewModel : TTerminalCompañia
    {
        public string Compañia { get; set; }
        public string CompañiaAgrupadora { get; set; }
       
    }
}
