IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Delete_Usuario')
    DROP PROCEDURE sp_Delete_Usuario
GO

CREATE PROCEDURE sp_Delete_Usuario
(
    @usuario_i_id INT
   ,@usuario_d_archived DATETIME
   ,@usuario_s_archivedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    UPDATE tb_usuario
       SET usuario_d_archived = @usuario_d_archived
         , usuario_s_archivedby = @usuario_s_archivedby
         , usuario_b_deleted = 1
     WHERE usuario_i_id = @usuario_i_id

SET NOCOUNT ON;
GO
