using AutoMapper;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;

namespace BusinessEntities.AutoMapperProfiles
{
    public class QuizAttemptProfile : Profile
    {
        public QuizAttemptProfile()
        {
            CreateMap<QuizAttempt, QuizAttemptVM>();
        }
    }
}