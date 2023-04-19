using SistemaMonitoramento.DataAccess.DataBase;
using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Util.Servicos;
using System;
using System.Data;

namespace SistemaMonitoramento.Model.DataObject
{
    internal static class doTela
    {
        public static void Insert(Tela obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Insert_Tela.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@tela_s_controller", obj.tela_s_controller, DbType.String),
                            new RepositoryParameter("@tela_s_path", obj.tela_s_path, DbType.String),
                            new RepositoryParameter("@tela_s_titulo", obj.tela_s_titulo, DbType.String),
                            new RepositoryParameter("@tela_d_created", obj.tela_d_created, DbType.DateTime),
                            new RepositoryParameter("@tela_s_createdby", obj.tela_s_createdby, DbType.String),
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

        public static void Update(Tela obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Update_Tela.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@tela_i_id", obj.tela_i_id, DbType.Int32),
                            new RepositoryParameter("@tela_s_controller", obj.tela_s_controller, DbType.String),
                            new RepositoryParameter("@tela_s_path", obj.tela_s_path, DbType.String),
                            new RepositoryParameter("@tela_s_titulo", obj.tela_s_titulo, DbType.String),
                            new RepositoryParameter("@tela_d_updated", obj.tela_d_updated, DbType.DateTime),
                            new RepositoryParameter("@tela_s_updatedby", obj.tela_s_updatedby, DbType.String),
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

        public static void Delete(Tela obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Delete_Tela.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@tela_i_id", obj.tela_i_id, DbType.Int32),
                            new RepositoryParameter("@tela_d_archived", obj.tela_d_archived, DbType.DateTime),
                            new RepositoryParameter("@tela_s_archivedby", obj.tela_s_archivedby, DbType.String),
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

        public static IQueryable<Tela> GetAll(string filtro)
        {
            using (IDataBase db = Repository.SelectRepository())
            {

                var lst = new List<Tela>();

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Tela_GetAll.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@filtro", filtro, DbType.String),                            
                        }))
                    {
                        while (dr.Read())
                        {
                            var obj = new Tela();

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

        public static Tela GetByKey(int tela_i_id)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    var obj = new Tela();

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Tela_GetByKey.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@tela_i_id", tela_i_id, DbType.Int32),
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
