import { QuizQuestion } from "src/app/quiz/Model/quizQuestion";
import { Question } from "../../Models/question";

export abstract class AC_ConcreteAnswerQuestion {
    quizQuestion: QuizQuestion = null;
    abstract getAnswers();
}
