IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Tela_GetAll')
    DROP PROCEDURE sp_Select_Tela_GetAll
GO

CREATE PROCEDURE sp_Select_Tela_GetAll
(
    @filtro VARCHAR(50) = ''
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_tela (NOLOCK)
     WHERE tela_b_deleted = 0 
       AND (@filtro = '' 
        OR tela_s_controller LIKE '%'+ @filtro + '%'
        OR tela_s_path       LIKE '%'+ @filtro + '%'
        OR tela_s_titulo     LIKE '%'+ @filtro + '%')
     ORDER
        BY tela_s_controller

SET NOCOUNT ON;
GO
