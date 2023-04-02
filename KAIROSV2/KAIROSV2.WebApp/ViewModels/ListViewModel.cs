using KAIROSV2.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.ViewModels
{
    public class ListViewModel<T> where T : class, new()
    {
        public ActionsPermission ActionsPermission { get; set; } 
        public IEnumerable<T> Entidades { get; set; }
        public IEnumerable<string> Encabezados { get; set; }
    }
}
