IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Update_Permissao')
    DROP PROCEDURE sp_Update_Permissao
GO

CREATE PROCEDURE sp_Update_Permissao
(
    @permissao_i_usuario INT
   ,@permissao_i_tela INT
   ,@permissao_d_updated DATETIME
   ,@permissao_s_updatedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    IF EXISTS (SELECT 1 
                 FROM tb_permissao (NOLOCK)
                WHERE permissao_i_tela = @permissao_i_tela 
                  AND permissao_i_usuario = @permissao_i_usuario
                  AND permissao_b_deleted = 0)
    BEGIN

        UPDATE tb_permissao           
           SET permissao_d_archived = @permissao_d_updated
             , permissao_s_archivedby = @permissao_s_updatedby
             , permissao_b_deleted = 1
         WHERE permissao_i_tela = @permissao_i_tela 
           AND permissao_i_usuario = @permissao_i_usuario   
           
    END
    ELSE
    BEGIN

        INSERT INTO tb_permissao
        SELECT @permissao_i_usuario AS permissao_i_usuario
             , @permissao_i_tela AS permissao_i_tela
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
