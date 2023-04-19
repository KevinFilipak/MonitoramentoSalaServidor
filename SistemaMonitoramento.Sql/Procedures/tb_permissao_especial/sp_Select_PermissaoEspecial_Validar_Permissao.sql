IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Select_PermissaoEspecial_Validar_Permissao')
    DROP PROCEDURE sp_Select_PermissaoEspecial_Validar_Permissao
GO

CREATE PROCEDURE sp_Select_PermissaoEspecial_Validar_Permissao
(
    @usuario_i_id INT
    ,@permissao_s_permissao VARCHAR(50)
)
AS
SET NOCOUNT OFF;

    DECLARE @VALIDAR BIT = 0

    IF EXISTS (SELECT 1
                 FROM tb_permissao_especial (NOLOCK)                
                WHERE permissao_s_permissao = @permissao_s_permissao 
                  AND permissao_i_usuario = @usuario_i_id
                  AND permissao_b_deleted = 0)
    BEGIN

        SET @VALIDAR = 1

    END
      
    SELECT @VALIDAR AS VALIDAR
       

SET NOCOUNT ON;
GO
