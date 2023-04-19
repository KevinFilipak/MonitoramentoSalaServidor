IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Permissao_GetByKey')
    DROP PROCEDURE sp_Select_Permissao_GetByKey
GO

CREATE PROCEDURE sp_Select_Permissao_GetByKey
(
    @permissao_i_id INT
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_permissao (NOLOCK)
     WHERE permissao_i_id = @permissao_i_id

SET NOCOUNT ON;
GO
