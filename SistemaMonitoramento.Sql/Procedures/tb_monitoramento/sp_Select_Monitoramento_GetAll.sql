IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Monitoramento_GetAll')
    DROP PROCEDURE sp_Select_Monitoramento_GetAll
GO

CREATE PROCEDURE sp_Select_Monitoramento_GetAll
(
    @filtro VARCHAR(50) = ''
)
AS
SET NOCOUNT OFF;

    SELECT *
      FROM tb_monitoramento (NOLOCK)
     WHERE (@filtro = ''
        OR monitoramento_f_temperatura LIKE '%' + @filtro + '%'       
        OR monitoramento_f_umidade    LIKE '%' + @filtro + '%'
        OR monitoramento_d_data   LIKE '%' + @filtro + '%')
     ORDER
        BY monitoramento_i_id

SET NOCOUNT ON;
GO
