IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Usuario_Autenticar')
    DROP PROCEDURE sp_Select_Usuario_Autenticar
GO

CREATE PROCEDURE sp_Select_Usuario_Autenticar
(
   @usuario_s_usuario VARCHAR(200) 
  ,@usuario_s_senha VARCHAR(200)
)
AS
SET NOCOUNT OFF;

	SELECT *
	  FROM tb_usuario (NOLOCK)
	 WHERE usuario_b_deleted = 0
       AND usuario_s_usuario = @usuario_s_usuario
	   AND usuario_s_senha = @usuario_s_senha

SET NOCOUNT ON;
GO
