using FluentValidation;
using HunnyPomodoro.Application.Sessions.Queries;

namespace HunnyPomodoro.Application.Sessions.Command.CreateSession;

public class ListSessionQueryValidator : AbstractValidator<ListSessionQuery>
{
    public ListSessionQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}