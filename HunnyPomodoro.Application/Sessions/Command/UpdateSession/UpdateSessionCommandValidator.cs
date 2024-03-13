using FluentValidation;

namespace HunnyPomodoro.Application.Sessions.Command.UpdateSession;

public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
{
    public UpdateSessionCommandValidator()
    {
        RuleFor(x => x.SessionId).NotEmpty();
        RuleFor(x => x.EndTime).NotNull();
        RuleFor(x => x.Status).NotEmpty();
    }
}