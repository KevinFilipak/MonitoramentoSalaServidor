using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Validation;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Util.Logs;

namespace SistemaMonitoramento.Model.Domain
{
    public class dmDispositivo
    {

        public List<Dispositivo> BuscarTodos(string filtro)
        {
            try
            {
                return doDispositivo.GetAll(filtro).ToList();
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public Dispositivo BuscarPorId(int dispositivo_i_id)
        {
            try
            {
                return doDispositivo.GetByKey(dispositivo_i_id);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public bool Validar(Dispositivo obj)
        {
            try
            {
                return new DispositivoValidator().Validate(obj).IsValid;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Salvar(Dispositivo obj, out string message, out string status)
        {
            try
            {
                
                if (!Validar(obj))
                {
                    message = "Problema na validação dos dados!";
                    status = "error";
                }
                else if (obj.dispositivo_i_id == 0)
                {
                    message = "Registro adicionado com sucesso!";
                    status = "success";                    

                    doDispositivo.Insert(obj);
                }
                else
                {
                    message = "Registro atualizado com sucesso!";
                    status = "success";

                    doDispositivo.Update(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void Excluir(Dispositivo obj, out string message, out string status)
        {
            try
            {
                if (obj.dispositivo_i_id == 0)
                {
                    message = "Objeto inválido!";
                    status = "error";
                }
                else
                {
                    message = "Registro excluído com sucesso!";
                    status = "success";

                    doDispositivo.Delete(obj);
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public void GerarCodigo(Dispositivo obj, string Caminho, string CaminhoDownload, string NomeArquivo)
        {
            try
            {
                string Codigo = System.IO.File.ReadAllText(Caminho);

                Codigo = Codigo.Replace("#REDEWIFI", obj.dispositivo_s_wifi_nome);
                Codigo = Codigo.Replace("#CODIGODISPOSITIVO", obj.dispositivo_i_id.ToString());
                Codigo = Codigo.Replace("#IPWIFI", obj.dispositivo_s_wifi_senha.ToString());
                Codigo = Codigo.Replace("#DELAYSINAL", obj.dispositivo_f_delay.ToString());


                if (!Directory.Exists(Path.Combine(CaminhoDownload)))
                    Directory.CreateDirectory(CaminhoDownload);

                if (File.Exists(CaminhoDownload + NomeArquivo))
                {
                    File.Delete(CaminhoDownload + NomeArquivo);
                }

                File.WriteAllText(CaminhoDownload + NomeArquivo, Codigo);



            }
            catch (Exception ex)
            {
                Log.GerarLog(ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
