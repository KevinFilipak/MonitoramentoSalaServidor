using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMonitoramento.Model.Class
{
    public class Tela
    {
        public int tela_i_id { get; set; }
        public string tela_s_controller { get; set; }
        public string tela_s_path { get; set; }
        public string tela_s_titulo { get; set; }
        public DateTime tela_d_created { get; set; }
        public string tela_s_createdby { get; set; }
        public DateTime? tela_d_updated { get; set; }
        public string tela_s_updatedby { get; set; }
        public DateTime? tela_d_archived { get; set; }
        public string tela_s_archivedby { get; set; }
        public bool tela_b_deleted { get; set; }

        public string tela_s_descricao_completa 
        {
            get
            {
                return tela_s_controller + " - " + tela_s_titulo;
            }
        }
    }
}
