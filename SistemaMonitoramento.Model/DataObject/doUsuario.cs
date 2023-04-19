using SistemaMonitoramento.DataAccess.DataBase;
using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Util.Crypto;
using SistemaMonitoramento.Util.Servicos;
using System;
using System.Data;

namespace SistemaMonitoramento.Model.DataObject
{
    internal static class doUsuario
    {
        public static void Insert(Usuario obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Insert_Usuario.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_s_usuario", obj.usuario_s_usuario, DbType.String),
                            new RepositoryParameter("@usuario_s_senha", Encrypt.MD5Hash(obj.usuario_s_senha), DbType.String),
                            new RepositoryParameter("@usuario_s_nome", obj.usuario_s_nome, DbType.String),
                            new RepositoryParameter("@usuario_s_email", obj.usuario_s_email, DbType.String),
                            new RepositoryParameter("@usuario_s_foto", obj.usuario_s_foto, DbType.String),
                            new RepositoryParameter("@usuario_s_funcao", obj.usuario_s_funcao, DbType.String),
                            new RepositoryParameter("@usuario_s_status", obj.usuario_s_status, DbType.String),
                            new RepositoryParameter("@usuario_d_created", obj.usuario_d_created, DbType.DateTime),
                            new RepositoryParameter("@usuario_s_createdby", obj.usuario_s_createdby, DbType.String),
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

        public static void Update(Usuario obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Update_Usuario.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_i_id", obj.usuario_i_id, DbType.String),
                            new RepositoryParameter("@usuario_s_usuario", obj.usuario_s_usuario, DbType.String),
                            new RepositoryParameter("@usuario_s_senha", Encrypt.MD5Hash(obj.usuario_s_senha), DbType.String),
                            new RepositoryParameter("@usuario_s_nome", obj.usuario_s_nome, DbType.String),
                            new RepositoryParameter("@usuario_s_email", obj.usuario_s_email, DbType.String),
                            new RepositoryParameter("@usuario_s_foto", obj.usuario_s_foto, DbType.String),
                            new RepositoryParameter("@usuario_s_funcao", obj.usuario_s_funcao, DbType.String),
                            new RepositoryParameter("@usuario_s_status", obj.usuario_s_status, DbType.String),
                            new RepositoryParameter("@usuario_d_updated", obj.usuario_d_updated, DbType.DateTime),
                            new RepositoryParameter("@usuario_s_updatedby", obj.usuario_s_updatedby, DbType.String),
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

        public static void UpdateSenha(Usuario obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Update_Usuario_Senha.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_i_id", obj.usuario_i_id, DbType.String),                            
                            new RepositoryParameter("@usuario_s_senha", Encrypt.MD5Hash(obj.usuario_s_senha), DbType.String),
                            new RepositoryParameter("@usuario_d_updated", obj.usuario_d_updated, DbType.DateTime),
                            new RepositoryParameter("@usuario_s_updatedby", obj.usuario_s_updatedby, DbType.String),
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

        public static void Delete(Usuario obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Delete_Usuario.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_i_id", obj.usuario_i_id, DbType.Int32),
                            new RepositoryParameter("@usuario_d_archived", obj.usuario_d_archived, DbType.DateTime),
                            new RepositoryParameter("@usuario_s_archivedby", obj.usuario_s_archivedby, DbType.String),
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

        public static IQueryable<Usuario> GetAll(string filtro)
        {
            using (IDataBase db = Repository.SelectRepository())
            {

                var lst = new List<Usuario>();

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Usuario_GetAll.ToString()
                        , new RepositoryParameter[]
                        {
                            new RepositoryParameter("@filtro", filtro, DbType.String),                            
                        }))
                    {
                        while (dr.Read())
                        {
                            var obj = new Usuario();

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

        public static Usuario GetByKey(int usuario_i_id)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    var obj = new Usuario();

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Usuario_GetByKey.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_i_id", usuario_i_id, DbType.Int32),
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

        public static void Autenticar(Usuario obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {                

                try
                {
                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_Usuario_Autenticar.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_s_usuario", obj.usuario_s_usuario, DbType.String),
                            new RepositoryParameter("@usuario_s_senha", Encrypt.MD5Hash(obj.usuario_s_senha), DbType.String),
                        }))
                    {

                        while (dr.Read())
                        {
                            Converter.MapDataToObject(dr, obj);
                        }
                    }
                    
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
