IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Update_Tela')
    DROP PROCEDURE sp_Update_Tela
GO

CREATE PROCEDURE sp_Update_Tela
(
    @tela_i_id INT
   ,@tela_s_controller VARCHAR(50)
   ,@tela_s_path VARCHAR(50)
   ,@tela_s_titulo VARCHAR(200)
   ,@tela_d_updated DATETIME
   ,@tela_s_updatedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    UPDATE tb_tela
       SET tela_s_controller = @tela_s_controller
         , tela_s_path = @tela_s_path
         , tela_s_titulo = @tela_s_titulo
         , tela_d_updated = @tela_d_updated
         , tela_s_updatedby = @tela_s_updatedby
    WHERE tela_i_id = @tela_i_id

SET NOCOUNT ON;
GO
