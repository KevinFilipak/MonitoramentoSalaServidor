using NLog;
using System;

namespace SistemaMonitoramento.Util.Logs
{
    public class Log
    {
        /// <summary>
        /// Gerar Log
        /// </summary>
        /// <param name="Classe">Nome da Classe</param>
        /// <param name="Metodo">Nome do Método</param>
        /// <param name="Descricao">Decrição do Evento</param>
        /// <param name="Executou">Se o código deu erro true senão false</param>
        /// 

        public static void GerarLog(string Classe, string Metodo, string Descricao, bool Executou)
        {
            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Log((Executou ? LogLevel.Info : LogLevel.Error), $"Classe: {Classe} Método: {Metodo} Descrição: {Descricao}");
            }
            finally
            {


            }
        }

        public static void GerarLog(Exception ex)
        {
            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                LogEventInfo info = new LogEventInfo(LogLevel.Error, "(null)", ex.Message);
                info.Exception = ex;
                logger.Log(info);
            }
            finally
            {


            }
        }

        public static void GerarLog(Exception ex, EnumLog Level)
        {
            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                LogLevel logLevel = LogLevel.Error;
                switch (Level)
                {
                    case EnumLog.Trace:
                        logLevel = LogLevel.Trace;
                        break;
                    case EnumLog.Debug:
                        logLevel = LogLevel.Debug;
                        break;
                    case EnumLog.Info:
                        logLevel = LogLevel.Info;
                        break;
                    case EnumLog.Warn:
                        logLevel = LogLevel.Warn;
                        break;
                    case EnumLog.Error:
                        logLevel = LogLevel.Error;
                        break;
                    case EnumLog.Fatal:
                        logLevel = LogLevel.Fatal;
                        break;
                    case EnumLog.Off:
                        logLevel = LogLevel.Off;
                        break;
                }
                LogEventInfo info = new LogEventInfo(logLevel, "(null)", ex.Message);
                info.Exception = ex;
                logger.Log(info);
            }
            finally
            {


            }
        }        

        public static void GerarLog(Exception ex, string mensagemtratada)
        {
            try
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                LogEventInfo info = new LogEventInfo(LogLevel.Error, "(null)", $"{ex.Message} {mensagemtratada}");
                info.Exception = ex;
                logger.Log(info);
            }
            finally
            {


            }
        }

    }
}
