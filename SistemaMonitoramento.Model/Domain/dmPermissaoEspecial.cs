using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Model.Validation;

namespace SistemaMonitoramento.Model.Domain
{
    public class dmPermissaoEspecial
    {

        public void Salvar(PermissaoEspecial obj, out string message, out string status)
        {
            try
            {                
                message = "Registro atualizado com sucesso!";
                status = "success";                    

                doPermissaoEspecial.Update(obj);
                
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }        

        public List<PermissaoEspecial> BuscarPorUsuario(int usuario_i_id)
        {
            try
            {
                return doPermissaoEspecial.GetByUsuario(usuario_i_id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ValidarPermissaoEspecial(int usuario_i_id, EnumPermissaoEspecial permissao_s_permissao)
        {
            try
            {
                return doPermissaoEspecial.ValidarPermissaoEspecial(usuario_i_id, permissao_s_permissao);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}
