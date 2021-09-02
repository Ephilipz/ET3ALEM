using System;
using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinessEntities.CustomConverters
{
    public class QuestionConverter : JsonConverter<Question>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Question value, JsonSerializer serializer)
        {
        }

        public override Question ReadJson(JsonReader reader, Type objectType, Question existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jObject = JObject.Load(reader);
            var target = Create(jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        private Question Create(JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));
            return (QuestionType) jObject.GetValue("questionType", StringComparison.InvariantCultureIgnoreCase)
                    .Value<int>() switch
                {
                    QuestionType.MCQ => new MultipleChoiceQuestion(),
                    QuestionType.TrueFalse => new TrueFalseQuestion(),
                    QuestionType.ShortAnswer => new ShortAnswerQuestion(),
                    QuestionType.LongAnswer => new LongAnswerQuestion(),
                    _ => throw new ArgumentNullException(nameof(jObject))
                };
        }
    }
}