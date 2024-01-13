using AuctionPlatform.Domain._DTO.Auction;
using FluentValidation;

namespace AuctionPlatform.Validators.Auction
{
    public class AuctionCreateDtoValidator : AbstractValidator<AuctionCreateDto>
    {
        public AuctionCreateDtoValidator()
        {
            RuleFor(auction => auction.Title)
               .NotEmpty().WithMessage("Title is required.")
               .Length(3, 255).WithMessage("Title must be between 3 and 255 characters.");

            RuleFor(auction => auction.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(auction => auction.MinimumBid)
                .GreaterThan(0).WithMessage("Minimum bid must be greater than 0.");

            RuleFor(auction => auction.StartTime)
                .NotEmpty().WithMessage("Start time is required.")
                .LessThan(auction => auction.EndTime).WithMessage("Start time must be before end time.");

            RuleFor(auction => auction.EndTime)
                .NotEmpty().WithMessage("End time is required.")
                .GreaterThan(auction => auction.StartTime).WithMessage("End time must be after start time.");
        }
    }
}
