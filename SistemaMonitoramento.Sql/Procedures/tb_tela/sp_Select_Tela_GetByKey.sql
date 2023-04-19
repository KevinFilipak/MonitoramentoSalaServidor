IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Tela_GetByKey')
    DROP PROCEDURE sp_Select_Tela_GetByKey
GO

CREATE PROCEDURE sp_Select_Tela_GetByKey
(
    @tela_i_id INT
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_tela (NOLOCK)
     WHERE tela_i_id = @tela_i_id

SET NOCOUNT ON;
GO
