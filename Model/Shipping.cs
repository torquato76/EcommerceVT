using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceVT.Model
{
    public class Shipping
    {
        public string Codigo { get; set; }
        public string freteValor { get; set; }
        public string prazoEntrega { get; set; }
        public string Erro { get; set; }
        public string MsgErro { get; set; }
    }  
}
