using FluentValidation;
using NodaTime;

using Pidp.Data;
using Pidp.Models;

namespace Pidp.Features.Parties
{
    public class Create
    {
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.DateOfBirth).NotEmpty();
            }
        }

        public class Command : ICommand<int>
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public LocalDate DateOfBirth { get; set; }
        }

        public class CreateCommandHandler : ICommandHandler<Command, int>
        {
            private readonly PidpDbContext _context;

            public CreateCommandHandler(PidpDbContext context)
            {
                _context = context;
            }

            public async Task<int> HandleAsync(Command command)
            {
                var party = new Party
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    DateOfBirth = command.DateOfBirth
                };

                _context.Parties.Add(party);

                await _context.SaveChangesAsync();

                return party.Id;
            }
        }
    }
}
