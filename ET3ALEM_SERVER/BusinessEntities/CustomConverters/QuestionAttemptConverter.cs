using System;
using BusinessEntities.Enumerators;
using BusinessEntities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinessEntities.CustomConverters
{
    public class QuestionAttemptConverter : JsonConverter<QuestionAttempt>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, QuestionAttempt value, JsonSerializer serializer)
        {
        }

        public override QuestionAttempt ReadJson(JsonReader reader, Type objectType, QuestionAttempt existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        private QuestionAttempt Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");
            return (QuestionType) jObject.GetValue("questionType", StringComparison.InvariantCultureIgnoreCase)
                    .Value<int>() switch
                {
                    QuestionType.MCQ => new MCQAttmept(),
                    QuestionType.TrueFalse => new TrueFalseAttempt(),
                    QuestionType.ShortAnswer => new ShortAnswerAttempt(),
                    QuestionType.LongAnswer => new LongAnswerAttempt(),
                    _ => throw new ArgumentNullException(nameof(jObject))
                };
        }
    }
}