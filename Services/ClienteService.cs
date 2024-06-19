using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository;
using static Services.ClienteService.ValidarClienteFluent;



namespace Services
{
    public class ClienteService
    {
        public class ValidarClienteFluent : AbstractValidator<ClienteModel>
        {
            private readonly ClienteRepository _clienteRepository;

            public ValidarClienteFluent(ClienteRepository repositorio)
            {
                _clienteRepository = repositorio;

                RuleFor(cliente => cliente.Nombre)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("El nombre debe tener al menos 3 caracteres.");

                RuleFor(cliente => cliente.Apellido)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("El apellido debe tener al menos 3 caracteres.");

                RuleFor(cliente => cliente.Celular)
                    .Matches(@"^\d{10}$")
                    .WithMessage("El número de celular debe tener 10 dígitos numéricos.");

                RuleFor(cliente => cliente.Mail)
                    .NotEmpty()
                    .EmailAddress()
                    .WithMessage("El correo electrónico no es válido.");

                RuleFor(cliente => cliente.Documento)
                        .NotEmpty()
                        .MinimumLength(7)
                        .Must(IsUniqueDocument)
                        .WithMessage("El documento debe tener al menos 7 caracteres y ser único.");

            }
            private bool IsUniqueDocument(string documento)
            {
                var entidad = _clienteRepository.consultarDocumento(documento);
                return entidad == null;
            }
            
        }

        

        private bool EsNumero(string valor)
        {
            return long.TryParse(valor, out _);
        }
    }
}




