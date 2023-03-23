using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMonitoramento.Utill.Servicos
{
    public static class ListaGeral
    {
        public static List<string> Status()
        {
            List<string> lst = new List<string>();

            lst.Add("Ativo");
            lst.Add("Inativo");

            return lst;
        }
        public static List<string> PedidoStatus()
        {
            List<string> lst = new List<string>();

            lst.Add("Orçamento");
            lst.Add("Inativo");

            return lst;
        }

        public static List<string> EstadosUF()
        {
            List<string> lst = new List<string>();

            lst.Add("AC");
            lst.Add("AL");
            lst.Add("AM");
            lst.Add("AP");
            lst.Add("BA");
            lst.Add("CE");
            lst.Add("DF");
            lst.Add("ES");
            lst.Add("GO");
            lst.Add("MA");
            lst.Add("MG");
            lst.Add("MS");
            lst.Add("MT");
            lst.Add("PA");
            lst.Add("PB");
            lst.Add("PE");
            lst.Add("PI");
            lst.Add("PR");
            lst.Add("RJ");
            lst.Add("RN");
            lst.Add("RR");
            lst.Add("RS");
            lst.Add("SC");
            lst.Add("SE");
            lst.Add("SP");
            lst.Add("TO");

            return lst;
        }
        public static List<string> TipoDePessoa()
        {
            List<string> lst = new List<string>();

            lst.Add("Pessoa Física");
            lst.Add("Pessoa Jurídica");

            return lst;
        }

        public static List<string> UnidadesMedida()
        {
            List<string> lst = new List<string>();

            lst.Add("PÇ");
            lst.Add("JG");

            return lst;
        }
    }
}
