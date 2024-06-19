using FluentValidation;
using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class FacturaService
    {
        public class ValidarFacturaFluent : AbstractValidator<FacturaModel>
        {
            private readonly FacturaRepository _facturaRepository;

            public ValidarFacturaFluent(FacturaRepository facturaRepository)
            {
                _facturaRepository = facturaRepository;

                RuleFor(factura => factura.Nro_factura)
                    .Matches(@"^\d{3}-\d{3}-\d{6}$")
                    .WithMessage("El número de factura debe tener el formato 'XXX-XXX-XXXXXX'.");

                RuleFor(factura => factura.Total)
                    .GreaterThan(0)
                    .WithMessage("El total de la factura debe ser mayor a 0.");

                RuleFor(factura => factura.Total_iva5)
                    .GreaterThan(0)
                    .WithMessage("El total del IVA 5% debe ser mayor a 0.");

                RuleFor(factura => factura.Total_iva10)
                    .GreaterThan(0)
                    .WithMessage("El total del IVA 10% debe ser mayor a 0.");

                RuleFor(factura => factura.Total_iva)
                    .GreaterThan(0)
                    .WithMessage("El total del IVA debe ser mayor a 0.");

                RuleFor(factura => factura.Total_letras)
                    .NotEmpty()
                    .MinimumLength(6)
                    .WithMessage("El total en letras debe tener al menos 6 caracteres.");

                RuleFor(factura => factura.Id_cliente)
                    .NotEqual(0)
                    .WithMessage("El ID del cliente no puede ser 0.");
            }
        }
    }
}
