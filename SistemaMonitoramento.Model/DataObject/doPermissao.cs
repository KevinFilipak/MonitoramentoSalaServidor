using SistemaMonitoramento.DataAccess.DataBase;
using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Util.Servicos;
using System;
using System.Data;

namespace SistemaMonitoramento.Model.DataObject
{
    internal static class doPermissao
    {
        public static void Insert(Permissao obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Insert_Permissao.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@permissao_i_usuario", obj.permissao_i_usuario, DbType.Int32),
                            new RepositoryParameter("@permissao_i_tela", obj.permissao_i_tela, DbType.Int32),
                            new RepositoryParameter("@permissao_d_created", obj.permissao_d_created, DbType.DateTime),
                            new RepositoryParameter("@permissao_s_createdby", obj.permissao_s_createdby, DbType.String),
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

        public static void Update(Permissao obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Update_Permissao.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@permissao_i_usuario", obj.permissao_i_usuario, DbType.Int32),
                            new RepositoryParameter("@permissao_i_tela", obj.permissao_i_tela, DbType.Int32),
                            new RepositoryParameter("@permissao_d_updated", obj.permissao_d_updated, DbType.DateTime),
                            new RepositoryParameter("@permissao_s_updatedby", obj.permissao_s_updatedby, DbType.String),
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

        public static void Delete(Permissao obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Delete_Permissao.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@permissao_i_id", obj.permissao_i_id, DbType.Int32),
                            new RepositoryParameter("@permissao_d_archived", obj.permissao_d_archived, DbType.DateTime),
                            new RepositoryParameter("@permissao_s_archivedby", obj.permissao_s_archivedby, DbType.String),
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

        public static IQueryable<Permissao> GetAll(int permissao_i_usuario)
        {
            using (IDataBase db = Repository.SelectRepository())
            {

                var lst = new List<Permissao>();

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Permissao_GetAll.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@permissao_i_usuario", permissao_i_usuario, DbType.Int32)                            
                        }))
                    {
                        while (dr.Read())
                        {
                            var obj = new Permissao();

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

        public static Permissao GetByKey(int permissao_i_id)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    var obj = new Permissao();

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Permissao_GetByKey.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@permissao_i_id", permissao_i_id, DbType.Int32),
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


        public static IQueryable<Permissao> GetByUsuario(int usuario_i_id)
        {
            using (IDataBase db = Repository.SelectRepository())
            {

                var lst = new List<Permissao>();

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Permissao_GetByUsuario.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_i_id", usuario_i_id, DbType.Int32)
                        }))
                    {
                        while (dr.Read())
                        {
                            var obj = new Permissao();

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
