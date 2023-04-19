IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Insert_Permissao')
    DROP PROCEDURE sp_Insert_Permissao
GO

CREATE PROCEDURE sp_Insert_Permissao
(
    @permissao_i_usuario INT
   ,@permissao_i_tela INT
   ,@permissao_d_created DATETIME
   ,@permissao_s_createdby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    INSERT INTO tb_permissao
    SELECT @permissao_i_usuario AS permissao_i_usuario
         , @permissao_i_tela AS permissao_i_tela
         , @permissao_d_created AS permissao_d_created
         , @permissao_s_createdby AS permissao_s_createdby
         , NULL AS permissao_d_updated
         , NULL AS permissao_s_updatedby
         , NULL AS permissao_d_archived
         , NULL AS permissao_s_archivedby
         , 0 AS permissao_b_deleted

SET NOCOUNT ON;
GO
