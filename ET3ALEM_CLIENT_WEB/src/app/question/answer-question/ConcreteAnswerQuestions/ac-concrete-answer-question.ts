import {QuestionAttempt} from "../../Models/questionAttempt/question-attempt";

export abstract class AC_ConcreteAnswerQuestion {
    questionAttempt: QuestionAttempt = null;
    abstract getAnswers();
}
