using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Validation;

namespace SistemaMonitoramento.Model.Domain
{
    public class dmUsuario
    {

        public List<Usuario> BuscarTodos(string filtro)
        {
            try
            {
                return doUsuario.GetAll(filtro).ToList();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public Usuario BuscarPorId(int usuario_i_id)
        {
            try
            {
                return doUsuario.GetByKey(usuario_i_id);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public bool Validar(Usuario obj)
        {
            try
            {
                return new UsuarioValidator().Validate(obj).IsValid;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Salvar(Usuario obj, out string message, out string status)
        {
            try
            {
                
                if (!Validar(obj))
                {
                    message = "Problema na validação dos dados!";
                    status = "error";
                }
                else if (obj.usuario_i_id == 0)
                {
                    message = "Registro adicionado com sucesso!";
                    status = "success";                    

                    doUsuario.Insert(obj);
                }
                else
                {
                    message = "Registro atualizado com sucesso!";
                    status = "success";                    

                    doUsuario.Update(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void AlterarSenha(Usuario obj, out string message, out string status)
        {
            try
            {

                
                message = "Senha alterada com sucesso!";
                status = "success";

                doUsuario.UpdateSenha(obj);                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Excluir(Usuario obj, out string message, out string status)
        {
            try
            {
                if (obj.usuario_i_id == 0)
                {
                    message = "Objeto inválido!";
                    status = "error";
                }
                else
                {
                    message = "Registro excluído com sucesso!";
                    status = "success";                                        

                    doUsuario.Delete(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Autenticar(Usuario obj)
        {
            try
            {
                doUsuario.Autenticar(obj);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
