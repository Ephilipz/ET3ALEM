import { QuizQuestion } from "src/app/quiz/Model/quizQuestion";
import { GeneralHelper } from "src/app/Shared/Classes/helpers/GeneralHelper";
import { LongAnswer } from "../long-answer";
import { QuestionAttempt } from "./question-attempt";

export class LongAnswerAttempt extends QuestionAttempt {
    LongAnswer?: LongAnswer;
    public constructor(Id: number, QuizQuestion: QuizQuestion, Grade: number, Answer: LongAnswer) {
        super(Id, QuizQuestion, Grade);
        this.LongAnswer = GeneralHelper.deepCopy(Answer);
    }
}
