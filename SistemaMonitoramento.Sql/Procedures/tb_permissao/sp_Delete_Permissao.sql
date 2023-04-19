IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Delete_Permissao')
    DROP PROCEDURE sp_Delete_Permissao
GO

CREATE PROCEDURE sp_Delete_Permissao
(
    @permissao_i_id INT
   ,@permissao_d_archived DATETIME
   ,@permissao_s_archivedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    UPDATE tb_permissao
       SET permissao_d_archived = @permissao_d_archived
         , permissao_s_archivedby = @permissao_s_archivedby
         , permissao_b_deleted = 1
     WHERE permissao_i_id = @permissao_i_id

SET NOCOUNT ON;
GO
