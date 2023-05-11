using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Validation;

namespace SistemaMonitoramento.Model.Domain
{
    public class dmDashboard
    {

        public List<Dashboard> BuscarTodos(string filtro)
        {
            try
            {
                return doDashboard.GetAll(filtro).ToList();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}
