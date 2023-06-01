IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Dispositivo_GetByKey')
    DROP PROCEDURE sp_Select_Dispositivo_GetByKey
GO

CREATE PROCEDURE sp_Select_Dispositivo_GetByKey
(
    @dispositivo_i_id INT
)
AS
SET NOCOUNT OFF;

    SELECT dispositivo_i_id
         , dispositivo_s_dispositivo
         , dispositivo_s_wifi_nome
         , dispositivo_s_wifi_senha
         , dispositivo_s_status
         , (dispositivo_f_delay / 60000) AS dispositivo_f_delay
         , dispositivo_d_created
         , dispositivo_s_createdby
         , dispositivo_d_updated
         , dispositivo_s_updatedby
         , dispositivo_d_archived
         , dispositivo_s_archivedby
         , dispositivo_b_deleted
      FROM tb_dispositivo (NOLOCK)
     WHERE dispositivo_i_id = @dispositivo_i_id

SET NOCOUNT ON;
GO
