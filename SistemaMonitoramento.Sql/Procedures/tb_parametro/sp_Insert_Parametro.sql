IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Insert_Parametro')
    DROP PROCEDURE sp_Insert_Parametro
GO

CREATE PROCEDURE sp_Insert_Parametro
(
    @monitoramento_f_temperatura	FLOAT
   ,@monitoramento_f_umidade 		FLOAT
   ,@monitoramento_d_data 			DATETIME

)
AS
SET NOCOUNT OFF;

    INSERT INTO tb_monitoramento
    SELECT @monitoramento_f_temperatura AS monitoramento_f_temperatura
         , @monitoramento_f_umidade		AS monitoramento_f_umidade
         , @monitoramento_d_data		AS monitoramento_d_data

SET NOCOUNT ON;
GO
