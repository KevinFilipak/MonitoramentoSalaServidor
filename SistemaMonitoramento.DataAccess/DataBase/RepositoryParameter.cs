using Microsoft.Extensions.Configuration;
using SistemaMonitoramento.Utill.Servicos;
using System.Data;
using System.Data.SqlClient;

namespace SistemaMonitoramento.DataAccess.DataBase
{
    public class RepositoryParameter
    {
        private IDataParameter _param { get; set; }
        private IConfiguration _configuration;
        public IDataParameter Param
        {
            get { return _param; }
            set { _param = value; }
        }

        public RepositoryParameter()
        {


            string provider = AppSettingsHelper.Setting("ProviderName");

            switch (provider)
            {
                case "SqlClient":
                    _param = new SqlParameter();
                    break;
            }

        }

        public RepositoryParameter(string name, object value, DbType dbType)
        {
            string provider = AppSettingsHelper.Setting("ProviderName");

            switch (provider)
            {
                case "SqlClient":
                    _param = new SqlParameter
                    {
                        Value = value,
                        DbType = dbType,
                        ParameterName = name,
                        IsNullable = true
                    };

                    break;
            }            
        }

        public RepositoryParameter(string name, object value, DbType dbType, bool isNullable)
        {
            string provider = AppSettingsHelper.Setting("ProviderName");


            switch (provider)
            {
                case "SqlClient":
                    _param = new SqlParameter
                    {
                        Value = value?? DBNull.Value,
                        DbType = dbType,
                        ParameterName = name,
                        IsNullable = isNullable
                    };

                    break;
            }
        }


        public DbType DbType
        {
            get
            {
                return _param.DbType;
            }
            set
            {
                _param.DbType = value;
            }
        }

        public ParameterDirection Direction
        {
            get
            {
                return _param.Direction;
            }
            set
            {
                _param.Direction = value;
            }
        }

        public bool IsNullable
        {
            get { return _param.IsNullable; }
        }

        public string ParameterName
        {
            get
            {
                return _param.ParameterName;
            }
            set
            {
                _param.ParameterName = value;
            }
        }

        public object Value
        {
            get
            {
                return _param.Value;
            }
            set
            {
                _param.Value = value;
            }
        }

    }
}
