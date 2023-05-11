using SistemaMonitoramento.DataAccess.DataBase;
using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Util.Servicos;
using System;
using System.Data;

namespace SistemaMonitoramento.Model.DataObject
{
    internal static class doDashboard
    {
        public static IQueryable<Dashboard> GetAll(string filtro)
        {
            using (IDataBase db = Repository.SelectRepository())
            {

                var lst = new List<Dashboard>();

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Dashboard.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@filtro", filtro, DbType.String),                            
                        }))
                    {
                        while (dr.Read())
                        {
                            var obj = new Dashboard();

                            Converter.MapDataToObject(dr, obj);

                            lst.Add(obj);
                        }
                    }

                    return lst.AsQueryable();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {

                }
            }
        }
    }
}
