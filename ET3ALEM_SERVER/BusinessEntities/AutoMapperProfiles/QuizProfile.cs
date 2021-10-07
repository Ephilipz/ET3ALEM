using AutoMapper;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;

namespace BusinessEntities.AutoMapperProfiles
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizVM>();
            CreateMap<QuizQuestion, QuizQuestionVM>().IncludeAllDerived();
        }
    }
}