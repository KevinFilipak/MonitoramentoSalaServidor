IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Insert_Tela')
    DROP PROCEDURE sp_Insert_Tela
GO

CREATE PROCEDURE sp_Insert_Tela
(
    @tela_s_controller VARCHAR(50)
   ,@tela_s_path VARCHAR(50)
   ,@tela_s_titulo VARCHAR(200)
   ,@tela_d_created DATETIME
   ,@tela_s_createdby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    INSERT INTO tb_tela
    SELECT @tela_s_controller AS tela_s_controller
         , @tela_s_path AS tela_s_path
         , @tela_s_titulo AS tela_s_titulo
         , @tela_d_created AS tela_d_created
         , @tela_s_createdby AS tela_s_createdby
         , NULL AS tela_d_updated
         , NULL AS tela_s_updatedby
         , NULL AS tela_d_archived
         , NULL AS tela_s_archivedby
         , 0 AS tela_b_deleted

SET NOCOUNT ON;
GO
