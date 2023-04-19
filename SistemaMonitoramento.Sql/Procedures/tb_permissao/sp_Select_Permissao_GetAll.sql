IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Permissao_GetAll')
    DROP PROCEDURE sp_Select_Permissao_GetAll
GO

CREATE PROCEDURE sp_Select_Permissao_GetAll
(
    @permissao_i_usuario INT
)
AS
SET NOCOUNT OFF;

    SELECT *
         , tela_s_controller AS permissao_s_controller
         , tela_s_controller + ' - ' + tela_s_titulo AS permissao_s_titulo         
      FROM tb_permissao (NOLOCK)
     INNER JOIN tb_tela (NOLOCK) ON permissao_i_tela = tela_i_id
     WHERE permissao_b_deleted = 0
       AND tela_b_deleted = 0
       AND permissao_i_usuario = @permissao_i_usuario
     ORDER
        BY tela_s_controller

SET NOCOUNT ON;
GO
