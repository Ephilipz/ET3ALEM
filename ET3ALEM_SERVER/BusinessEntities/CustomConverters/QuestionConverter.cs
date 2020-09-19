using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessEntities.CustomConverters
{
    public class QuestionConverter : JsonConverter<Question>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Question value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Question ReadJson(JsonReader reader, Type objectType, Question existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);
            Question target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }
        private Question Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");
            switch ((QuestionType)jObject.GetValue("questionType", StringComparison.InvariantCultureIgnoreCase).Value<int>())
            {
                case QuestionType.MCQ:
                    return new MultipleChoiceQuestion();
                case QuestionType.TrueFalse:
                    return new TrueFalseQuestion();
                default:
                    throw new ArgumentNullException("jObject");
            }
        }
    }
}
