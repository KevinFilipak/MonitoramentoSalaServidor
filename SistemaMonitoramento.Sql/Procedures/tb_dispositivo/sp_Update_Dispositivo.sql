IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Update_Dispositivo')
    DROP PROCEDURE sp_Update_Dispositivo
GO

CREATE PROCEDURE sp_Update_Dispositivo
(
    @dispositivo_i_id INT
   ,@dispositivo_s_dispositivo VARCHAR(50)
   ,@dispositivo_s_wifi_nome		VARCHAR(50)
   ,@dispositivo_s_wifi_senha	VARCHAR(50)
   ,@dispositivo_s_status VARCHAR(50)
   ,@dispositivo_f_delay FLOAT
   ,@dispositivo_d_updated DATETIME
   ,@dispositivo_s_updatedby VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    UPDATE tb_dispositivo
       SET dispositivo_s_dispositivo = @dispositivo_s_dispositivo
	     , dispositivo_s_wifi_nome = @dispositivo_s_wifi_nome
		 , dispositivo_s_wifi_senha = @dispositivo_s_wifi_senha
	     , dispositivo_s_status = @dispositivo_s_status
         , dispositivo_d_updated = @dispositivo_d_updated
         , dispositivo_s_updatedby = @dispositivo_s_updatedby
		 , dispositivo_f_delay    = (@dispositivo_f_delay * 60000)
    WHERE dispositivo_i_id = @dispositivo_i_id

SET NOCOUNT ON;
GO
