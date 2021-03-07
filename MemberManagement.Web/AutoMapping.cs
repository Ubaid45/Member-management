using AutoMapper;
using ManagementSystem.Data.DTOs;
using ManagementSystem.Data.Models;

namespace MemberManagementSystem
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserDto, User>();
            CreateMap<AccountDto, Account>();

            CreateMap<User, UserDto>();
            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.AccountOwnerName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}