IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Delete_Monitoramento')
    DROP PROCEDURE sp_Delete_Monitoramento
GO

CREATE PROCEDURE sp_Delete_Monitoramento
(
    @monitoramento_i_id INT
)
AS
SET NOCOUNT OFF;

    DELETE tb_monitoramento
     WHERE monitoramento_i_id = @monitoramento_i_id

SET NOCOUNT ON;
GO
