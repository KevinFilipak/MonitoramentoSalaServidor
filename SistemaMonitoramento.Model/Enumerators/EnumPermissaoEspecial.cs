using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaMonitoramento.Model.Enumerators
{
    public enum EnumPermissaoEspecial
    {
        [Display(Name = "Teste 1")]
        Teste1,
        [Display(Name = "Teste 2")]
        Teste2
    }
}
