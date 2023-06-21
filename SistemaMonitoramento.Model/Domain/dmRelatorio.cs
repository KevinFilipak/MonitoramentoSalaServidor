using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Validation;
using SistemaMonitoramento.Model.DataObject;
using SistemaMonitoramento.Model.Enumerators;
using System.Data;

namespace SistemaMonitoramento.Model.Domain
{
    public class dmRelatorio
    {
        public DataSet BuscarDados(EnumStoreProcedures procedure, Dictionary<KeyValuePair<string, string>, DbType> parametros)
        {
            return doRelatorio.GetReport(procedure, parametros);
        }
    }
}
