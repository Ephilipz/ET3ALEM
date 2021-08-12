export class LongAnswer {
    Id: number;
    Answer: String;
    LongAnswerAttemptId: number;
    public constructor(Id: number, Answer: string, LongAnswerAttemptId: number) {
        this.Id = Id;
        this.Answer = Answer;
        this.LongAnswerAttemptId = LongAnswerAttemptId
    }
}