using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Utill.Servicos;

namespace SistemaMonitoramento.DataAccess.DataBase
{
    public class Repository
    {
        

        public static IDataBase SelectRepository()
        {
            IDataBase objDataBase = null;

            string provider = AppSettingsHelper.Setting("ProviderName");

            switch (provider)
            {
                case "SqlClient":
                    objDataBase = new RepositorySQLServer();

                    break;
            }

            
            
            return objDataBase;
        }

    }
}
