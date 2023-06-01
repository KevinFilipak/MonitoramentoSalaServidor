using SistemaMonitoramento.DataAccess.DataBase;
using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Util.Servicos;
using System;
using System.Data;

namespace SistemaMonitoramento.Model.DataObject
{
    internal static class doDispositivo
    {
        public static void Insert(Dispositivo obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Insert_Dispositivo.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@dispositivo_s_dispositivo", obj.dispositivo_s_dispositivo, DbType.String),
                            new RepositoryParameter("@dispositivo_s_wifi_nome", obj.dispositivo_s_wifi_nome, DbType.String),
                            new RepositoryParameter("@dispositivo_s_wifi_senha", obj.dispositivo_s_wifi_senha, DbType.String),
                            new RepositoryParameter("@dispositivo_s_status", obj.dispositivo_s_status, DbType.String),
                            new RepositoryParameter("@dispositivo_f_delay", obj.dispositivo_f_delay, DbType.Double),
                            new RepositoryParameter("@dispositivo_d_created", obj.dispositivo_d_created, DbType.DateTime),
                            new RepositoryParameter("@dispositivo_s_createdby", obj.dispositivo_s_createdby, DbType.String),
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

        public static void Update(Dispositivo obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Update_Dispositivo.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@dispositivo_i_id", obj.dispositivo_i_id, DbType.Int32),
                            new RepositoryParameter("@dispositivo_s_dispositivo", obj.dispositivo_s_dispositivo, DbType.String),
                            new RepositoryParameter("@dispositivo_s_wifi_nome", obj.dispositivo_s_wifi_nome, DbType.String),
                            new RepositoryParameter("@dispositivo_s_wifi_senha", obj.dispositivo_s_wifi_senha, DbType.String),
                            new RepositoryParameter("@dispositivo_s_status", obj.dispositivo_s_status, DbType.String),
                            new RepositoryParameter("@dispositivo_f_delay", obj.dispositivo_f_delay, DbType.Double),
                            new RepositoryParameter("@dispositivo_d_updated", obj.dispositivo_d_updated, DbType.DateTime),
                            new RepositoryParameter("@dispositivo_s_updatedby", obj.dispositivo_s_updatedby, DbType.String),
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

        public static void Delete(Dispositivo obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Delete_Dispositivo.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@dispositivo_i_id", obj.dispositivo_i_id, DbType.Int32),
                            new RepositoryParameter("@dispositivo_d_archived", obj.dispositivo_d_archived, DbType.DateTime),
                            new RepositoryParameter("@dispositivo_s_archivedby", obj.dispositivo_s_archivedby, DbType.String),
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

        public static IQueryable<Dispositivo> GetAll(string filtro)
        {
            using (IDataBase db = Repository.SelectRepository())
            {

                var lst = new List<Dispositivo>();

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Dispositivo_GetAll.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@filtro", filtro, DbType.String),                            
                        }))
                    {
                        while (dr.Read())
                        {
                            var obj = new Dispositivo();

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

        public static Dispositivo GetByKey(int dispositivo_i_id)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    var obj = new Dispositivo();

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Dispositivo_GetByKey.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@dispositivo_i_id", dispositivo_i_id, DbType.Int32),
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
