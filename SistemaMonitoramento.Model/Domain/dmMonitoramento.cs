using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Validation;

namespace SistemaMonitoramento.Model.Domain
{
    public class dmMonitoramento
    {

        public List<Monitoramento> BuscarTodos(string filtro)
        {
            try
            {
                return doMonitoramento.GetAll(filtro).ToList();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public Monitoramento BuscarPorId(int monitoramento_i_id)
        {
            try
            {
                return doMonitoramento.GetByKey(monitoramento_i_id);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public bool Validar(Monitoramento obj)
        {
            try
            {
                return new MonitoramentoValidator().Validate(obj).IsValid;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Salvar(Monitoramento obj, out string message, out string status)
        {
            try
            {
                
                if (!Validar(obj))
                {
                    message = "Problema na validação dos dados!";
                    status = "error";
                }
                else if (obj.MONITORAMENTO_I_ID == 0)
                {
                    message = "Registro adicionado com sucesso!";
                    status = "success";                    

                    doMonitoramento.Insert(obj);
                }
                else
                {
                    message = "Registro atualizado com sucesso!";
                    status = "success";                    

                    doMonitoramento.Update(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }


        public void Excluir(Monitoramento obj, out string message, out string status)
        {
            try
            {
                if (obj.MONITORAMENTO_I_ID == 0)
                {
                    message = "Objeto inválido!";
                    status = "error";
                }
                else
                {
                    message = "Registro excluído com sucesso!";
                    status = "success";                                        

                    doMonitoramento.Delete(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

    }
}
