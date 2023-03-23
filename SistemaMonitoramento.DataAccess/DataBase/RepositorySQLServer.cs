using SistemaMonitoramento.DataAccess.Interface;
using System.Data;
using System.Data.SqlClient;
using SistemaMonitoramento.Util.Logs;
using SistemaMonitoramento.Utill.Servicos;

namespace SistemaMonitoramento.DataAccess.DataBase
{
    public class RepositorySQLServer : IDataBase
    {
        [ThreadStatic]
        private static SqlConnection _dbConnection;
        private IDbTransaction _tran;
        private bool _return;
        private string _connString;
        private bool _stayConnect;
        

        public RepositorySQLServer() 
        {
            
        }

        public IDbConnection GetConnection
        {
            get { return _dbConnection; }
        }

        public bool Connect()
        {            
            try 
            {
                _return = false;

                if (_dbConnection == null)
                {
                    _stayConnect = false;
                    _connString = AppSettingsHelper.Setting("ConnectionStrings:Connection");
                    _dbConnection = new SqlConnection(_connString);
                    _dbConnection.Open();

                    if (_dbConnection.State == ConnectionState.Open)
                        _return = true;
                }
                else if (_dbConnection.State == ConnectionState.Closed)
                {
                    _dbConnection.Open();
                    _return = (_dbConnection.State == ConnectionState.Open);
                }
                else
                {
                    _stayConnect = true;
                    _return = true;
                }

                return _return;
            }
            catch (SqlException sqlException)
            {
                _return = false;
                throw new Exception(sqlException.Message);
            }
            catch (Exception ex)
            {
                _return = false;
                throw new Exception(ex.Message);
            }
        }

        public bool Disconnect()
        {
            _return = false;

            if (!_stayConnect & (_dbConnection != null))
            {
                if (_dbConnection.State == ConnectionState.Open)
                {
                    _dbConnection.Close();
                    _return = true;
                    _dbConnection = null;
                }
            }

            return _return;
        }

        public void BeginTran()
        {
            if (_dbConnection == null)
                Connect();

            if (_dbConnection != null) 
                _tran = _dbConnection.BeginTransaction();
        }

        public void RoolbackTran()
        {
            _tran.Rollback();
            _tran = null;
        }

        public void CommitTran()
        {
            _tran.Commit();
            _tran = null;
        }
        
        public int ExecStoreProcedure(string Procedure)
        {
            
            Connect();
            
            var cmdSqlServer = _dbConnection.CreateCommand();

            cmdSqlServer.CommandText = Procedure;
            cmdSqlServer.CommandType = CommandType.StoredProcedure;
            cmdSqlServer.CommandTimeout = 0;

            if (_tran != null)
                cmdSqlServer.Transaction = (SqlTransaction)_tran;

            try
            {                
                int result = cmdSqlServer.ExecuteNonQuery();                
                LogDataBase.GerarLogSQL(Procedure, "OK");
                
                return result;

            }
            catch (SqlException sqlException)
            {
                LogDataBase.GerarLogSQL(Procedure, new string[0], "NOK - " + sqlException.Message);
                throw new Exception(sqlException.Message);
            }
            catch (Exception ex)
            {
                LogDataBase.GerarLogSQL(Procedure, new string[0], "NOK - " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {                
                cmdSqlServer.Dispose();                
            }
        }
        
        public int ExecStoreProcedure(string Procedure, params object[] Parametros)
        {
            
            Connect();
            
            var cmdSqlServer = _dbConnection.CreateCommand();

            cmdSqlServer.CommandType = CommandType.StoredProcedure;
            cmdSqlServer.CommandText = Procedure;
            cmdSqlServer.CommandTimeout = 0;

            if (_tran != null)
                cmdSqlServer.Transaction = (SqlTransaction)_tran;            
            
            try
            {                
                int count;

                for (count = 0; count <= Parametros.Length - 1; count++)
                {
                    RepositoryParameter param = (RepositoryParameter)Parametros[count];
                    
                    cmdSqlServer.Parameters.Add(param.Param);                    
                }

                int result = cmdSqlServer.ExecuteNonQuery();

                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "OK");
                return result;
            }
            catch (SqlException sqlException)
            {
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "NOK - " + sqlException.Message);
                throw new Exception(sqlException.Message);
            }
            catch (Exception ex)
            {
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "NOK - " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {                
                cmdSqlServer.Dispose();                
            }
        }
        
        public int ExecStoreProcedureScalar(string Procedure, params object[] Parametros)
        {
            Connect();

            var cmdSqlServer = _dbConnection.CreateCommand();

            cmdSqlServer.CommandType = CommandType.StoredProcedure;
            cmdSqlServer.CommandText = Procedure;
            cmdSqlServer.CommandTimeout = 0;

            if (_tran != null)
                cmdSqlServer.Transaction = (SqlTransaction)_tran;            

            try
            {
                int count;

                for (count = 0; count <= Parametros.Length - 1; count++)
                {
                    RepositoryParameter param = (RepositoryParameter)Parametros[count];
                    cmdSqlServer.Parameters.Add(param.Param);                    
                }

                int result = Convert.ToInt32(cmdSqlServer.ExecuteScalar());

                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "OK");
                return result;
            }
            catch (SqlException sqlException)
            {
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "NOK - " + sqlException.Message);
                throw new Exception(sqlException.Message);
            }
            catch (Exception ex)
            {
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "NOK - " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                cmdSqlServer.Dispose();
            }
        }
        
        public DataSet ExecStoreProcedureDS(string Procedure, params object[] Parametros)
        {
            Connect();

            var dsResult = new DataSet();
            var daSQLServer = new SqlDataAdapter();
            var cmdSqlServer = _dbConnection.CreateCommand();

            cmdSqlServer.CommandText = Procedure;
            cmdSqlServer.CommandType = CommandType.StoredProcedure;
            cmdSqlServer.CommandTimeout = 0;

            if (_tran != null)
                cmdSqlServer.Transaction = (SqlTransaction)_tran;            

            try
            {
                int count;

                for (count = 0; count <= Parametros.Length - 1; count++)
                {
                    RepositoryParameter param = (RepositoryParameter)Parametros[count];
                    cmdSqlServer.Parameters.Add(param.Param);                    
                }

                daSQLServer.SelectCommand = cmdSqlServer;
                
                daSQLServer.Fill(dsResult);
                
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "OK");

                return dsResult;

            }
            catch (SqlException sqlException)
            {
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "NOK - " + sqlException.Message);
                throw new Exception(sqlException.Message);
            }
            catch (Exception ex)
            {
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "NOK - " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                cmdSqlServer.Dispose();
            }
        }
        
        public DataSet ExecStoreProcedureDS(string Procedure)
        {
            Connect();

            var dsResult = new DataSet();
            var daSQLServer = new SqlDataAdapter();
            var cmdSqlServer = _dbConnection.CreateCommand();

            cmdSqlServer.CommandText = Procedure;
            cmdSqlServer.CommandType = CommandType.StoredProcedure;
            cmdSqlServer.CommandTimeout = 0;

            if (_tran != null)
                cmdSqlServer.Transaction = (SqlTransaction)_tran;

            try
            {
                daSQLServer.SelectCommand = cmdSqlServer;
                daSQLServer.Fill(dsResult);
                LogDataBase.GerarLogSQL(Procedure, "OK");

                return dsResult;

            }
            catch (SqlException sqlException)
            {
                LogDataBase.GerarLogSQL(Procedure, "NOK - " + sqlException.Message);
                throw new Exception(sqlException.Message);
            }
            catch (Exception ex)
            {
                LogDataBase.GerarLogSQL(Procedure, "NOK - " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                cmdSqlServer.Dispose();
            }
        }
        
        public IDataReader ExecStoreProcedureDR(string Procedure, params object[] Parametros)
        {
            Connect();

            var cmdSqlServer = _dbConnection.CreateCommand();

            cmdSqlServer.CommandText = Procedure;
            cmdSqlServer.CommandType = CommandType.StoredProcedure;
            cmdSqlServer.CommandTimeout = 0;

            if (_tran != null)
                cmdSqlServer.Transaction = (SqlTransaction)_tran;            

            try
            {
                int count;

                for (count = 0; count <= Parametros.Length - 1; count++)
                {
                    RepositoryParameter param = (RepositoryParameter)Parametros[count];
                    cmdSqlServer.Parameters.Add(param.Param);                    
                }

                var result = cmdSqlServer.ExecuteReader();

                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "OK");
                
                return result;
            }
            catch (SqlException sqlException)
            {
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "NOK - " + sqlException.Message);
                throw new Exception(sqlException.Message);
            }
            catch (Exception ex)
            {
                LogDataBase.GerarLogSQL(Procedure, FixeParamsExceptLog(Parametros), "NOK - " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                cmdSqlServer.Dispose();
            }
        }
        
        public IDataReader ExecStoreProcedureDR(string Procedure)
        {
            Connect();

            var cmdSqlServer = _dbConnection.CreateCommand();

            cmdSqlServer.CommandText = Procedure;
            cmdSqlServer.CommandType = CommandType.StoredProcedure;
            cmdSqlServer.CommandTimeout = 0;

            if (_tran != null)
                cmdSqlServer.Transaction = (SqlTransaction)_tran;            

            try
            {
                var result = cmdSqlServer.ExecuteReader();

                LogDataBase.GerarLogSQL(Procedure, new string[0], "OK");

                return result;
            }
            catch (SqlException sqlException)
            {
                LogDataBase.GerarLogSQL(Procedure, new string[0], "NOK - " + sqlException.Message);
                throw new Exception(sqlException.Message);
            }
            catch (Exception ex)
            {
                LogDataBase.GerarLogSQL(Procedure, new string[0], "NOK - " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                cmdSqlServer.Dispose();
            }
        }

        public void Dispose()
        {                        
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbConnection.State != ConnectionState.Executing ||
                _dbConnection.State != ConnectionState.Connecting ||
                _dbConnection.State != ConnectionState.Fetching ||
                _dbConnection.State != ConnectionState.Broken)
                {
                    Disconnect();

                }
            }            
        }

        private string[] FixeParamsExceptLog(object[] Parametros)
        {
            string[] fixedParameters = new string[Parametros.Length];

            for (int i = 0; i < Parametros.Length; i++)
            {
                var parameter = (RepositoryParameter)Parametros[i];

                if (parameter.Value == DBNull.Value)
                {
                    fixedParameters[i] = "null";
                }
                else
                {
                    switch (parameter.DbType)
                    {
                        case DbType.Boolean:
                            fixedParameters[i] = bool.Parse(parameter.Value.ToString()) == true ? "1" : "0";
                            break;
                        case DbType.Int32:
                            fixedParameters[i] = int.Parse(parameter.Value.ToString()).ToString();
                            break;
                        case DbType.DateTime:
                            fixedParameters[i] = "'" + DateTime.Parse(parameter.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                            break;
                        case DbType.Double:
                            fixedParameters[i] = double.Parse(parameter.Value.ToString()).ToString().Replace(",", ".");
                            break;
                        case DbType.String:
                            fixedParameters[i] = "'" + parameter.Value + "'";
                            break;
                    }
                }
                
            }

            return fixedParameters;
        }

    }
}
