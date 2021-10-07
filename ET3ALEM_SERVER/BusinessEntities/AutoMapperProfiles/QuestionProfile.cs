using AutoMapper;
using BusinessEntities.Models;
using BusinessEntities.ViewModels;

namespace BusinessEntities.AutoMapperProfiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionVM>().IncludeAllDerived();
            CreateMap<MultipleChoiceQuestion, MultipleChoiceQuestionVM>();
            CreateMap<Choice, ChoiceVM>();
            CreateMap<TrueFalseQuestion, TrueFalseQuestionVM>();
            CreateMap<ShortAnswerQuestion, ShortAnswerQuestionVM>();
            CreateMap<LongAnswerQuestion, LongAnswerQuestionVM>();
        }
    }
}