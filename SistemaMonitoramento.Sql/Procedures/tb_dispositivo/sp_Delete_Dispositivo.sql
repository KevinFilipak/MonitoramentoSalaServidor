IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Delete_Dispositivo')
    DROP PROCEDURE sp_Delete_Dispositivo
GO

CREATE PROCEDURE sp_Delete_Dispositivo
(
    @dispositivo_i_id INT
   ,@dispositivo_d_archived DATETIME
   ,@dispositivo_s_archivedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    UPDATE tb_dispositivo
       SET dispositivo_d_archived = @dispositivo_d_archived
         , dispositivo_s_archivedby = @dispositivo_s_archivedby
         , dispositivo_b_deleted = 1
     WHERE dispositivo_i_id = @dispositivo_i_id

SET NOCOUNT ON;
GO
