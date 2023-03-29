IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Update_Parametro')
    DROP PROCEDURE sp_Update_Parametro
GO

CREATE PROCEDURE sp_Update_Parametro
(
    @monitoramento_i_id				INT
   ,@monitoramento_f_temperatura	FLOAT
   ,@monitoramento_f_umidade 		FLOAT
   ,@monitoramento_d_data 			DATETIME
)
AS
SET NOCOUNT OFF;

    UPDATE tb_monitoramento
       SET @monitoramento_f_temperatura = @monitoramento_f_temperatura
	     , @monitoramento_f_umidade = @monitoramento_f_umidade
		 , @monitoramento_d_data = @monitoramento_d_data
     WHERE monitoramento_i_id = @monitoramento_i_id

SET NOCOUNT ON;
GO
