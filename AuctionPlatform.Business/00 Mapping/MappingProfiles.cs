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

            #endregion   
            #region Auction
            CreateMap<Auction, AuctionDto>();
            CreateMap<AuctionDto, Auction>();
            #endregion
        }
    }
}
