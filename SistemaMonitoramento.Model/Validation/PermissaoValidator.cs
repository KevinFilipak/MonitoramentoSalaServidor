using FluentValidation;
using SistemaMonitoramento.Model.Class;

namespace SistemaMonitoramento.Model.Validation
{
    internal class PermissaoValidator : AbstractValidator<Permissao>
    {
        public PermissaoValidator()
        {
            RuleFor(x => x.permissao_i_usuario).NotEmpty();
            RuleFor(x => x.permissao_i_tela).NotEmpty();            
        }

    }
}
