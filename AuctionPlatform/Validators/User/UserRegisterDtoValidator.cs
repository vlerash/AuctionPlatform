using AuctionPlatform.Domain._DTO.User;
using FluentValidation;

namespace AuctionPlatform.Validators.User
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(dto => dto.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(dto => dto.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(50).WithMessage("Surname cannot exceed 50 characters.");

            RuleFor(dto => dto.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(4, 19).WithMessage("Username must be between 4 and 19 characters.");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit."); ;
        }
    }
}
