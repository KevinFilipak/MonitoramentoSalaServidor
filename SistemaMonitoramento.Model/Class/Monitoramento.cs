using static System.Net.Mime.MediaTypeNames;

namespace SistemaMonitoramento.Model.Class
{
    public class Monitoramento
    {
        public int MONITORAMENTO_I_ID { get; set; }
        public double MONITORAMENTO_F_TEMPERATURA { get; set; }
        public double MONITORAMENTO_F_UMIDADE { get; set; }
        public DateTime MONITORAMENTO_D_DATA { get; set; }
    }
}