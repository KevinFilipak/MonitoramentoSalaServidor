using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Validation;

namespace SistemaMonitoramento.Model.Domain
{
    public class dmTela
    {

        public List<Tela> BuscarTodos(string filtro)
        {
            try
            {
                return doTela.GetAll(filtro).ToList();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public Tela BuscarPorId(int tela_i_id)
        {
            try
            {
                return doTela.GetByKey(tela_i_id);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public bool Validar(Tela obj)
        {
            try
            {
                return new TelaValidator().Validate(obj).IsValid;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Salvar(Tela obj, out string message, out string status)
        {
            try
            {
                
                if (!Validar(obj))
                {
                    message = "Problema na validação dos dados!";
                    status = "error";
                }
                else if (obj.tela_i_id == 0)
                {
                    message = "Registro adicionado com sucesso!";
                    status = "success";                    

                    doTela.Insert(obj);
                }
                else
                {
                    message = "Registro atualizado com sucesso!";
                    status = "success";                    

                    doTela.Update(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Excluir(Tela obj, out string message, out string status)
        {
            try
            {
                if (obj.tela_i_id == 0)
                {
                    message = "Objeto inválido!";
                    status = "error";
                }
                else
                {
                    message = "Registro excluído com sucesso!";
                    status = "success";                                        

                    doTela.Delete(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }        
    }
}
