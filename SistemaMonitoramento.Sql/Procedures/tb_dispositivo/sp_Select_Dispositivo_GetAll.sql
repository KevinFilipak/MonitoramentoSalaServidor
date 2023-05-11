IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Dispositivo_GetAll')
    DROP PROCEDURE sp_Select_Dispositivo_GetAll
GO

CREATE PROCEDURE sp_Select_Dispositivo_GetAll
(
    @filtro VARCHAR(50) = ''
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_dispositivo (NOLOCK)
     WHERE dispositivo_b_deleted = 0 
       AND (@filtro = '' 
        OR dispositivo_s_dispositivo LIKE '%'+ @filtro + '%')
     ORDER
        BY dispositivo_s_dispositivo

SET NOCOUNT ON;
GO
