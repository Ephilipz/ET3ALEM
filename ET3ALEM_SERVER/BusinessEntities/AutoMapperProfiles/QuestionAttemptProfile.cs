using AutoMapper;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;

namespace BusinessEntities.AutoMapperProfiles
{
    public class QuestionAttemptProfile : Profile
    {
        public QuestionAttemptProfile()
        {
            CreateMap<QuestionAttempt, QuestionAttemptVM>().IncludeAllDerived();
            CreateMap<LongAnswerAttempt, LongQuestionAttemptVM>();
        }
    }
}