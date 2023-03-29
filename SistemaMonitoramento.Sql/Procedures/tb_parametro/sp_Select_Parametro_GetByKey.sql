IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Parametro_GetByKey')
    DROP PROCEDURE sp_Select_Parametro_GetByKey
GO

CREATE PROCEDURE sp_Select_Parametro_GetByKey
(
    @monitoramento_i_id INT
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_monitoramento (NOLOCK)
     WHERE monitoramento_i_id = @monitoramento_i_id

SET NOCOUNT ON;
GO
