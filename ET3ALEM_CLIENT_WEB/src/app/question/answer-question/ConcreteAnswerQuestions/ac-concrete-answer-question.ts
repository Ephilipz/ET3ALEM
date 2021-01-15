import { Question } from "../../Models/question";

export abstract class AC_ConcreteAnswerQuestion {
    question: any = null;
    abstract getAnswers();
}
