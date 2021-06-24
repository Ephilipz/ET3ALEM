import { QuestionAttempt } from "src/app/question/Models/question-attempt";

export abstract class AC_ConcreteQuestionResult {
    abstract questionAttempt: QuestionAttempt = null;
}
