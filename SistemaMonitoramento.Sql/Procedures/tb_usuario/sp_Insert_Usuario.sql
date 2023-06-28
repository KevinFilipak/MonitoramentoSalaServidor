IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Insert_Usuario')
    DROP PROCEDURE sp_Insert_Usuario
GO

CREATE PROCEDURE sp_Insert_Usuario
(
    @usuario_s_usuario VARCHAR(50)
   ,@usuario_s_senha VARCHAR(500)
   ,@usuario_s_nome VARCHAR(100)
   ,@usuario_s_email VARCHAR(100)
   ,@usuario_s_foto VARCHAR(50)
   ,@usuario_s_funcao VARCHAR(50)   
   ,@usuario_s_status VARCHAR(50)
   ,@usuario_d_created DATETIME
   ,@usuario_s_createdby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    INSERT INTO tb_usuario
    SELECT @usuario_s_usuario AS usuario_s_usuario
         , @usuario_s_senha AS usuario_s_senha
         , @usuario_s_nome AS usuario_s_nome
         , @usuario_s_email AS usuario_s_email
		 , @usuario_s_foto AS usuario_s_foto 
		 , @usuario_s_funcao AS usuario_s_funcao         
         , @usuario_s_status AS usuario_s_status
         , @usuario_d_created AS usuario_d_created
         , @usuario_s_createdby AS usuario_s_createdby
         , NULL AS usuario_d_updated
         , NULL AS usuario_s_updatedby
         , NULL AS usuario_d_archived
         , NULL AS usuario_s_archivedby
         , 0 AS usuario_b_deleted

SET NOCOUNT ON;
GO
