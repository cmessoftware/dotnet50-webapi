using FluentValidation;

namespace cmes_webapi.Api.Dto.Validators
{
    public class TipoOperatoriaRequestDatosDtoValidator : AbstractValidator<TipoOperatoriaRequestDto>
    {
        public TipoOperatoriaRequestDatosDtoValidator()
        {
            RuleFor(x => x.BGBAHeader).NotNull();
            RuleFor(x => x.Datos).NotNull();
            RuleFor(x => x.Datos.AgenteColocador).NotNull().NotEmpty();
            RuleFor(x => x.Datos.Canal).NotNull().NotEmpty();
            RuleFor(x => x.Datos.Comitente).NotNull().NotEmpty();
            RuleFor(x => x.Datos.HostId).NotNull().NotEmpty();

        }
    }
}
