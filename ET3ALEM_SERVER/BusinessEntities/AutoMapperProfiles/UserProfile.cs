using AutoMapper;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;

namespace BusinessEntities.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, SimpleUserVM>();
        }
    }
}