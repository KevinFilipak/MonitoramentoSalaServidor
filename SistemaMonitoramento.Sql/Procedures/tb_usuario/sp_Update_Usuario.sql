IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Update_Usuario')
    DROP PROCEDURE sp_Update_Usuario
GO

CREATE PROCEDURE sp_Update_Usuario
(
    @usuario_i_id INT
   ,@usuario_s_usuario VARCHAR(50)
   ,@usuario_s_senha VARCHAR(500) = ''
   ,@usuario_s_nome VARCHAR(100)
   ,@usuario_s_email VARCHAR(100)
   ,@usuario_s_foto VARCHAR(50) = ''
   ,@usuario_s_funcao VARCHAR(50)
   ,@usuario_s_status VARCHAR(50)
   ,@usuario_d_updated DATETIME
   ,@usuario_s_updatedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    UPDATE tb_usuario
       SET usuario_s_usuario = @usuario_s_usuario
         , usuario_s_senha = CASE WHEN @usuario_s_senha = ''
                                  THEN usuario_s_senha
                                  ELSE @usuario_s_senha
                              END
         , usuario_s_nome = @usuario_s_nome
         , usuario_s_email = @usuario_s_email
         , usuario_s_foto = CASE WHEN @usuario_s_foto = ''
                                  THEN usuario_s_foto
                                  ELSE @usuario_s_foto
                              END
         , usuario_s_funcao = @usuario_s_funcao
         , usuario_s_status = @usuario_s_status
         , usuario_d_updated = @usuario_d_updated
         , usuario_s_updatedby = @usuario_s_updatedby
    WHERE usuario_i_id = @usuario_i_id

SET NOCOUNT ON;
GO
