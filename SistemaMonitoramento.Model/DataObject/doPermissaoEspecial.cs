using SistemaMonitoramento.DataAccess.DataBase;
using SistemaMonitoramento.DataAccess.Interface;
using SistemaMonitoramento.Model.Class;
using SistemaMonitoramento.Model.Enumerators;
using SistemaMonitoramento.Util.Servicos;
using System;
using System.Data;

namespace SistemaMonitoramento.Model.DataObject
{
    internal static class doPermissaoEspecial
    {
        

        public static void Update(PermissaoEspecial obj)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                try
                {
                    db.BeginTran();

                    db.ExecStoreProcedure(EnumStoreProcedures.sp_Update_PermissaoEspecial.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@permissao_i_usuario", obj.permissao_i_usuario, DbType.Int32),
                            new RepositoryParameter("@permissao_s_permissao", obj.permissao_s_permissao, DbType.String),
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

        


        public static IQueryable<PermissaoEspecial> GetByUsuario(int usuario_i_id)
        {
            using (IDataBase db = Repository.SelectRepository())
            {

                var lst = new List<PermissaoEspecial>();

                var lstPermissoes = Enum.GetValues(typeof(EnumPermissaoEspecial))
                                                        .Cast<EnumPermissaoEspecial>()
                                                        .Select(v => v.ToString())
                                                        .ToList();

                foreach (var item in lstPermissoes)
                {
                    lst.Add(new PermissaoEspecial
                    {
                        permissao_i_id = 0,
                        permissao_i_usuario = usuario_i_id,                        
                        permissao_s_permissao = (EnumPermissaoEspecial)Enum.Parse(typeof(EnumPermissaoEspecial), item)
                    });
                }

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_PermissaoEspecial_GetByUsuario.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_i_id", usuario_i_id, DbType.Int32)
                        }))
                    {
                        while (dr.Read())
                        {
                            foreach (var item in lst)
                            {
                                if (item.permissao_s_permissao == (EnumPermissaoEspecial)Enum.Parse(typeof(EnumPermissaoEspecial), (string)dr["permissao_s_permissao"]))
                                {
                                    item.permissao_i_id = (int)dr["permissao_i_id"];
                                }
                            }                            
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

        public static bool ValidarPermissaoEspecial(int usuario_i_id, EnumPermissaoEspecial permissao_s_permissao)
        {
            using (IDataBase db = Repository.SelectRepository())
            {
                var _validar = false;

                try
                {

                    using (IDataReader dr = db.ExecStoreProcedureDR(EnumStoreProcedures.sp_Select_PermissaoEspecial_Validar_Permissao.ToString(),
                        new RepositoryParameter[]
                        {
                            new RepositoryParameter("@usuario_i_id", usuario_i_id, DbType.Int32),                            
                            new RepositoryParameter("@permissao_s_permissao", permissao_s_permissao, DbType.String)
                        }))
                    {
                        while (dr.Read())
                        {
                            _validar = (bool)dr["VALIDAR"];
                        }
                    }

                    return _validar;
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
