using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using System.Collections.Generic;
using AutoMapper;

namespace BusinessEntities.ViewModels
{
    public abstract class QuestionVM
    {
        public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Body { get; set; }
    }
    public class LongAnswerQuestionVM : QuestionVM
    {

    }
    public class MultipleChoiceQuestionVM : QuestionVM
    { 
        public McqAnswerType McqAnswerType { get; set; }
        public List<ChoiceVM> Choices { get; set; } = new List<ChoiceVM>();
    }
    public class ChoiceVM
    {
        public int Id { get; set; }
        public string Body { get; set; }
    }
    public class TrueFalseQuestionVM : QuestionVM
    {
    }
    public class ShortAnswerQuestionVM : QuestionVM
    {
    }
}