using FluentValidation;
using SistemaMonitoramento.Model.Class;

namespace SistemaMonitoramento.Model.Validation
{
    internal class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.usuario_s_nome).NotEmpty();
            RuleFor(x => x.usuario_s_email).NotEmpty();
            RuleFor(x => x.usuario_s_usuario).NotEmpty();            
        }

    }
}
