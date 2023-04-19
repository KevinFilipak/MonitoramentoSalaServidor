IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_PermissaoEspecial_GetByUsuario')
    DROP PROCEDURE sp_Select_PermissaoEspecial_GetByUsuario
GO

CREATE PROCEDURE sp_Select_PermissaoEspecial_GetByUsuario
(
    @usuario_i_id INT
)
AS
SET NOCOUNT OFF;

    SELECT permissao_i_id
         , permissao_i_usuario
         , permissao_s_permissao        
      FROM tb_permissao_especial (NOLOCK) 
     WHERE permissao_b_deleted = 0
       AND permissao_i_usuario = @usuario_i_id     
       
SET NOCOUNT ON;
GO
