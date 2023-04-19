IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Delete_Parametro')
    DROP PROCEDURE sp_Delete_Parametro
GO

CREATE PROCEDURE sp_Delete_Parametro
(
    @monitoramento_i_id INT
)
AS
SET NOCOUNT OFF;

    DELETE tb_parametro
     WHERE monitoramento_i_id = @monitoramento_i_id

SET NOCOUNT ON;
GO
