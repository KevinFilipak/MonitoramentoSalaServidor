IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Insert_Dispositivo')
    DROP PROCEDURE sp_Insert_Dispositivo
GO

CREATE PROCEDURE sp_Insert_Dispositivo
(
    @dispositivo_s_dispositivo		VARCHAR(50)
   ,@dispositivo_s_wifi_nome		VARCHAR(50)
   ,@dispositivo_s_wifi_senha		VARCHAR(50)
   ,@dispositivo_s_status			VARCHAR(50)
   ,@dispositivo_d_created			DATETIME
   ,@dispositivo_s_createdby		VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    INSERT INTO tb_dispositivo
    SELECT @dispositivo_s_dispositivo AS dispositivo_s_dispositivo
		 , @dispositivo_s_wifi_nome	  AS dispositivo_s_wifi_nome
		 , @dispositivo_s_wifi_senha  AS dispositivo_s_wifi_senha
	     , @dispositivo_s_status	  AS dispositivo_s_status
         , @dispositivo_d_created     AS dispositivo_d_created
         , @dispositivo_s_createdby   AS dispositivo_s_createdby
         , NULL						  AS dispositivo_d_updated
         , NULL						  AS dispositivo_s_updatedby
         , NULL						  AS dispositivo_d_archived
         , NULL						  AS dispositivo_s_archivedby
         , 0						  AS dispositivo_b_deleted

SET NOCOUNT ON;
GO
