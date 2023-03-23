using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SistemaMonitoramento.Util.Servicos
{
    public class Arquivo
    {
        public string Nome { get; set; }
        public List<string> Linhas { get; set; }

        public string Caminho { get; set; }
        public string CaminhoDownload { get; set; }



        public void GerarArquivo()
        {
            try
            {
                if (!Directory.Exists(Path.Combine(Caminho)))
                    Directory.CreateDirectory(Caminho);

                if (File.Exists(Caminho + Nome))
                {
                    File.Delete(Caminho + Nome);
                }

                File.WriteAllLines(Caminho + Nome, Linhas.ToArray());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
