IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Dispositivo_GetByKey')
    DROP PROCEDURE sp_Select_Dispositivo_GetByKey
GO

CREATE PROCEDURE sp_Select_Dispositivo_GetByKey
(
    @dispositivo_i_id INT
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_dispositivo (NOLOCK)
     WHERE dispositivo_i_id = @dispositivo_i_id

SET NOCOUNT ON;
GO
