IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Update_PermissaoEspecial')
    DROP PROCEDURE sp_Update_PermissaoEspecial
GO

CREATE PROCEDURE sp_Update_PermissaoEspecial
(
    @permissao_i_usuario INT
   ,@permissao_s_permissao VARCHAR(50)
   ,@permissao_d_updated DATETIME
   ,@permissao_s_updatedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    IF EXISTS (SELECT 1 
                 FROM tb_permissao_especial (NOLOCK)
                WHERE permissao_s_permissao = @permissao_s_permissao 
                  AND permissao_i_usuario = @permissao_i_usuario
                  AND permissao_b_deleted = 0)
    BEGIN

        UPDATE tb_permissao_especial           
           SET permissao_d_archived = @permissao_d_updated
             , permissao_s_archivedby = @permissao_s_updatedby
             , permissao_b_deleted = 1
         WHERE permissao_i_usuario = @permissao_i_usuario
           AND permissao_s_permissao = @permissao_s_permissao   
           
    END
    ELSE
    BEGIN

        INSERT INTO tb_permissao_especial
        SELECT @permissao_i_usuario AS permissao_i_usuario
             , @permissao_s_permissao AS permissao_s_permissao
             , @permissao_d_updated AS permissao_d_created
             , @permissao_s_updatedby AS permissao_s_createdby
             , NULL AS permissao_d_updated
             , NULL AS permissao_s_updatedby
             , NULL AS permissao_d_archived
             , NULL AS permissao_s_archivedby
             , 0 AS permissao_b_deleted

    END  


SET NOCOUNT ON;
GO
