using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace SistemaMonitoramento.DataAccess.Interface
{
    public interface IDataBase : IDisposable
    {        
        IDbConnection GetConnection { get; }
        bool Connect();
        bool Disconnect();
        void BeginTran();
        void RoolbackTran();
        void CommitTran();
        int ExecStoreProcedure(string procedure);
        int ExecStoreProcedure(string procedure, params object[] parameters);
        int ExecStoreProcedureScalar(string procedure, params object[] parameters);
        DataSet ExecStoreProcedureDS(string procedure, params object[] parameters);
        DataSet ExecStoreProcedureDS(string procedure);
        IDataReader ExecStoreProcedureDR(string procedure, params object[] parameters);
        IDataReader ExecStoreProcedureDR(string procedure);
        
    }
}
