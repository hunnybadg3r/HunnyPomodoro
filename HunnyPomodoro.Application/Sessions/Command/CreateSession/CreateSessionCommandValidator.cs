using FluentValidation;

namespace HunnyPomodoro.Application.Sessions.Command.CreateSession;

public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
{
    public CreateSessionCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.StartTime).NotNull();
    }
}