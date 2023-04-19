IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Usuario_GetAll')
    DROP PROCEDURE sp_Select_Usuario_GetAll
GO

CREATE PROCEDURE sp_Select_Usuario_GetAll
(
    @filtro VARCHAR(50) = ''
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_usuario (NOLOCK)
     WHERE usuario_b_deleted = 0
       AND (@filtro = ''
        OR usuario_s_usuario LIKE '%' + @filtro + '%'       
        OR usuario_s_nome    LIKE '%' + @filtro + '%'
        OR usuario_s_email   LIKE '%' + @filtro + '%'     
        OR usuario_s_funcao  LIKE '%' + @filtro + '%')
     ORDER
        BY usuario_s_usuario

SET NOCOUNT ON;
GO
