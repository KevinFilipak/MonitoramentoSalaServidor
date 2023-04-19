using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMonitoramento.Model.Class
{
    public class Permissao
    {
        public int permissao_i_id { get; set; }
        public int permissao_i_usuario { get; set; }
        public int permissao_i_tela { get; set; }
        public DateTime permissao_d_created { get; set; }
        public string permissao_s_createdby { get; set; }
        public DateTime? permissao_d_updated { get; set; }
        public string permissao_s_updatedby { get; set; }
        public DateTime? permissao_d_archived { get; set; }
        public string permissao_s_archivedby { get; set; }
        public bool permissao_b_deleted { get; set; }
        public string permissao_s_controller { get; set; }

        public string permissao_s_titulo { get; set; }

        public bool permissao_b_checked
        {
            get
            {
                return permissao_i_id == 0 ? false : true;
            }
        }

        public string permissao_s_checked
        {
            get
            {
                return permissao_i_id == 0 ? "" : "checked";
            }
        }
    }
}
