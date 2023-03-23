using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace SistemaMonitoramento.Util.Logs
{
    public class LogDataBase
    {

        /// <summary>
        /// Gerar Log de Execução SQL
        /// </summary>
        /// <param name="SQL">Store Procedure ou Query</param>
        /// <param name="Executou">Se o código deu erro true senão false</param>
        public static void GerarLogSQL(string SQL, string Executou)
        {
            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                LogEventInfo info = new LogEventInfo((Executou == "OK" ? LogLevel.Info : LogLevel.Error), "(null)", SQL);
                logger.Log(info);
            }
            finally
            {

            }
        }


        /// <summary>
        /// Gerar Logo de Execução SQL com Parâmetros
        /// </summary>
        /// <param name="SQL">Store Procedure ou Query</param>
        /// <param name="Parametros">Parâmetros utilizados</param>
        /// <param name="Executou">Se o código deu erro true senão false</param>
        public static void GerarLogSQL(string SQL, string[] Parametros, string Executou)
        {
            try
            {
                SQL += " ";
                int i = 0;

                foreach (var item in Parametros.ToList())
                {
                    i += 1;
                    SQL += item + (Parametros.Count() == i ? "" : ", ");
                }

                GerarLogSQL(SQL.Trim(), Executou);
            }
            finally
            {

            }
        }
    }
}
