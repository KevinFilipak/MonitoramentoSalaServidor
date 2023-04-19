IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Update_Usuario_Senha')
    DROP PROCEDURE sp_Update_Usuario_Senha
GO

CREATE PROCEDURE sp_Update_Usuario_Senha
(
    @usuario_i_id INT   
   ,@usuario_s_senha VARCHAR(500) = ''
   ,@usuario_d_updated DATETIME
   ,@usuario_s_updatedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    UPDATE tb_usuario
       SET usuario_s_senha = @usuario_s_senha      
         , usuario_d_updated = @usuario_d_updated
         , usuario_s_updatedby = @usuario_s_updatedby
    WHERE usuario_i_id = @usuario_i_id

SET NOCOUNT ON;
GO
