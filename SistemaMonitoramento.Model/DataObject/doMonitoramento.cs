using SistemaMonitoramento.DataAccess.DataBase;
using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Util.Servicos;
using System;
using System.Data;

namespace SistemaMonitoramento.Model.DataObject
{
    internal static class doMonitoramento
    {
        public static void Insert(Monitoramento obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Insert_Monitoramento.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@monitoramento_i_dispositivo", obj.monitoramento_i_dispositivo, DbType.Int32),
                            new RepositoryParameter("@monitoramento_f_temperatura", obj.monitoramento_f_temperatura, DbType.Double),
                            new RepositoryParameter("@monitoramento_f_umidade", obj.monitoramento_f_umidade, DbType.Double),
                            new RepositoryParameter("@monitoramento_d_data", obj.monitoramento_d_data, DbType.DateTime),
                        });

                    db.CommitTran();

                }
                catch (Exception ex)
                {
                    db.RoolbackTran();
                    throw new Exception(ex.Message);
                }
                finally
                {

                }
            }
        }

        public static void Update(Monitoramento obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Update_Monitoramento.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@monitoramento_i_id", obj.monitoramento_i_id, DbType.Int32),
                            new RepositoryParameter("@monitoramento_f_temperatura", obj.monitoramento_f_temperatura, DbType.Double),
                            new RepositoryParameter("@monitoramento_f_umidade", obj.monitoramento_f_umidade, DbType.Double),
                            new RepositoryParameter("@monitoramento_d_data", obj.monitoramento_d_data, DbType.DateTime),
                        });

                    db.CommitTran();

                }
                catch (Exception ex)
                {
                    db.RoolbackTran();
                    throw new Exception(ex.Message);
                }
                finally
                {

                }
            }
        }

        public static void Delete(Monitoramento obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Delete_Monitoramento.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@monitoramento_i_id", obj.monitoramento_i_id, DbType.Int32),
                        });

                    db.CommitTran();

                }
                catch (Exception ex)
                {
                    db.RoolbackTran();
                    throw new Exception(ex.Message);
                }
                finally
                {

                }
            }
        }

        public static IQueryable<Monitoramento> GetAll(string filtro)
        {
            using (IDataBase db = Repository.SelectRepository())
            {

                var lst = new List<Monitoramento>();

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Monitoramento_GetAll.ToString()
                        , new RepositoryParameter[]
                        {
                            new RepositoryParameter("@filtro", filtro, DbType.String),                            
                        }))
                    {
                        while (dr.Read())
                        {
                            var obj = new Monitoramento();

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

        public static Monitoramento GetByKey(int monitoramento_i_id)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    var obj = new Monitoramento();

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Monitoramento_GetByKey.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@monitoramento_i_id", obj.monitoramento_i_id, DbType.Int32),
                        }))
                    {
                        while (dr.Read())
                        {
                            Converter.MapDataToObject(dr, obj);                            
                        }
                    }

                    return obj;

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
