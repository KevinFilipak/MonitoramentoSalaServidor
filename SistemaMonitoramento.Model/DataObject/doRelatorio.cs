using SistemaMonitoramento.DataAccess.DataBase;
using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Util.Servicos;
using System;
using System.Data;

namespace SistemaMonitoramento.Model.DataObject
{
    internal static class doRelatorio
    {
        public static DataSet GetReport(EnumStoreProcedures procedure, Dictionary<KeyValuePair<string, string>, DbType> parametros)
        {
            var _parameters = new RepositoryParameter[parametros.Count];

            for (int i = 0; i < parametros.Count; i++)
            {
                _parameters[i] = new RepositoryParameter(parametros.ToArray()[i].Key.Key, parametros.ToArray()[i].Key.Value, parametros.ToArray()[i].Value);
            }


            using (var _db = Repository.SelectRepository())
            {
                return _db.ExecStoreProcedureDS(procedure.ToString(), _parameters);
            }
        }
    }
}
