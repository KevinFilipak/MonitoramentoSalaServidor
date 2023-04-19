using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMonitoramento.Model.Class
{
    public class Usuario
    {
        public int usuario_i_id { get; set; }
        public string usuario_s_usuario { get; set; }
        public string usuario_s_senha { get; set; }
        public string usuario_s_confirmar_senha { get; set; }
        public string usuario_s_nome { get; set; }
        public string usuario_s_email { get; set; }
        public string usuario_s_status { get; set; }
        public DateTime usuario_d_created { get; set; }
        public string usuario_s_createdby { get; set; }
        public DateTime? usuario_d_updated { get; set; }
        public string usuario_s_updatedby { get; set; }
        public DateTime? usuario_d_archived { get; set; }
        public string usuario_s_archivedby { get; set; }
        public bool usuario_b_deleted { get; set; }
        public string usuario_s_pagina_anterior { get; set; }
        public bool usuario_s_lembrar_me { get; set; }

        public string usuario_s_foto { get; set; }
        public string usuario_s_funcao { get; set; }

        public string usuario_s_status_cor
        {
            get
            {
                switch (usuario_s_status)
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
