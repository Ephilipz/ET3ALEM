import { QuizQuestion } from "src/app/quiz/Model/quizQuestion";
import { Helper } from "src/app/Shared/Classes/helpers/Helper";
import { LongAnswer } from "./long-answer";
import { QuestionAttempt } from "./question-attempt";

export class LongAnswerAttempt extends QuestionAttempt {
    Answer: LongAnswer;
    public constructor(Id: number, QuizQuestion: QuizQuestion, Grade: number, Answer: LongAnswer) {
        super(Id, QuizQuestion, Grade);
        this.Answer = Helper.deepCopy(Answer);
    }
}