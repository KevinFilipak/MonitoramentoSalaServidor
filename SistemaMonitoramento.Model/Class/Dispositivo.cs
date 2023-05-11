using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMonitoramento.Model.Class
{
    public class Dispositivo
    {
        public int dispositivo_i_id { get; set; }
        public string dispositivo_s_dispositivo { get; set; }
        public string dispositivo_s_wifi_nome { get; set; }
        public string dispositivo_s_wifi_senha { get; set; }
        public DateTime dispositivo_d_created { get; set; }
        public string dispositivo_s_createdby { get; set; }
        public DateTime? dispositivo_d_updated { get; set; }
        public string dispositivo_s_updatedby { get; set; }
        public DateTime? dispositivo_d_archived { get; set; }
        public string dispositivo_s_archivedby { get; set; }
        public bool dispositivo_b_deleted { get; set; }
        public string dispositivo_s_status { get; set; }
        public string dispositivo_s_status_cor
        {
            get
            {
                switch (dispositivo_s_status)
                {
                    case "Ativo":
                        return "primary";
                    case "Inativo":
                        return "danger";
                    default:
                        return "default";
                }
            }
        }

    }
}
