import { QuizQuestion } from "src/app/quiz/Model/quizQuestion";
import { QuestionAttempt } from "./question-attempt";

export class ShortAnswerAttempt extends QuestionAttempt {
    Answer: string;

    constructor(Id: number = 0, QuizQuestion: QuizQuestion = null, Grade: number = 0, Answer: string = '') {
        super(Id, QuizQuestion, Grade);
        this.Answer = Answer;
    }
}