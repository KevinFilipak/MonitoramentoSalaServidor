IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Delete_Tela')
    DROP PROCEDURE sp_Delete_Tela
GO

CREATE PROCEDURE sp_Delete_Tela
(
    @tela_i_id INT
   ,@tela_d_archived DATETIME
   ,@tela_s_archivedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    UPDATE tb_tela
       SET tela_d_archived = @tela_d_archived
         , tela_s_archivedby = @tela_s_archivedby
         , tela_b_deleted = 1
     WHERE tela_i_id = @tela_i_id

SET NOCOUNT ON;
GO
