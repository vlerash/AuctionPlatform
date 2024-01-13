using AuctionPlatform.Domain._DTO.Auction;
using AuctionPlatform.Domain._DTO.User;
using AuctionPlatform.Domain.Entities;
using AutoMapper;

namespace BlogManagementSystem.Business._00_Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region User
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.WalletAmount, opt => opt.MapFrom(src => 1000.00m));
            #endregion

            #region Auction
            CreateMap<Auction, AuctionDto>();
            CreateMap<AuctionDto, Auction>();
            #endregion
        }
    }
}
