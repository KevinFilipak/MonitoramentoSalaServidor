IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Usuario_GetByKey')
    DROP PROCEDURE sp_Select_Usuario_GetByKey
GO

CREATE PROCEDURE sp_Select_Usuario_GetByKey
(
    @usuario_i_id INT
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_usuario (NOLOCK)
     WHERE usuario_i_id = @usuario_i_id

SET NOCOUNT ON;
GO
