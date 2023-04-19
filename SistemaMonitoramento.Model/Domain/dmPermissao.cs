using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Validation;

namespace SistemaMonitoramento.Model.Domain
{
    public class dmPermissao
    {

        public List<Permissao> BuscarTodos(int permissao_i_usuario)
        {
            try
            {
                return doPermissao.GetAll(permissao_i_usuario).ToList();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public Permissao BuscarPorId(int permissao_i_id)
        {
            try
            {
                return doPermissao.GetByKey(permissao_i_id);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public bool Validar(Permissao obj)
        {
            try
            {
                return new PermissaoValidator().Validate(obj).IsValid;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Salvar(Permissao obj, out string message, out string status)
        {
            try
            {                
                message = "Registro atualizado com sucesso!";
                status = "success";                    

                doPermissao.Update(obj);
                
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Excluir(Permissao obj, out string message, out string status)
        {
            try
            {
                if (obj.permissao_i_id == 0)
                {
                    message = "Objeto inválido!";
                    status = "error";
                }
                else
                {
                    message = "Registro excluído com sucesso!";
                    status = "success";                                        

                    doPermissao.Delete(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public List<Permissao> BuscarPorUsuario(int usuario_i_id)
        {
            try
            {
                return doPermissao.GetByUsuario(usuario_i_id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
