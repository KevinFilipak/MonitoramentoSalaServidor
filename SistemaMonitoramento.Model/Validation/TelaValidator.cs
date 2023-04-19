using FluentValidation;
using SistemaMonitoramento.Model.Class;

namespace SistemaMonitoramento.Model.Validation
{
    internal class TelaValidator : AbstractValidator<Tela>
    {
        public TelaValidator()
        {
            RuleFor(x => x.tela_s_controller).NotEmpty();
            RuleFor(x => x.tela_s_path).NotEmpty();
            RuleFor(x => x.tela_s_titulo).NotEmpty();            
        }

    }
}
