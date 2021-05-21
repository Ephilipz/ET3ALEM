import { QuestionAttempt } from "../../Models/question-attempt";

export abstract class AC_ConcreteAnswerQuestion {
    questionAttempt: QuestionAttempt = null;
    abstract getAnswers();
}
