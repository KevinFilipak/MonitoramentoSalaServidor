IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_Permissao_GetByUsuario')
    DROP PROCEDURE sp_Select_Permissao_GetByUsuario
GO

CREATE PROCEDURE sp_Select_Permissao_GetByUsuario
(
    @usuario_i_id INT
)
AS
SET NOCOUNT OFF;

    SELECT ISNULL(permissao_i_id, 0) AS permissao_i_id
         , @usuario_i_id AS permissao_i_usuario
         , tela_i_id AS permissao_i_tela
         , tela_s_controller AS permissao_s_controller
         , tela_s_controller + ' - ' + tela_s_titulo AS permissao_s_titulo         
      FROM tb_tela (NOLOCK)
      LEFT JOIN tb_permissao (NOLOCK) ON permissao_i_tela = tela_i_id
                                     AND permissao_b_deleted = 0
                                     AND permissao_i_usuario = @usuario_i_id
     WHERE tela_b_deleted = 0
       

SET NOCOUNT ON;
GO
