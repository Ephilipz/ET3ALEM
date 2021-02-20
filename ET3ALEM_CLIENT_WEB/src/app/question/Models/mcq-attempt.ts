import { Choice } from "./choice";
import { QuestionAttempt } from "./question-attempt";

export class MCQAttempt extends QuestionAttempt {
    Choices: Array<Choice>;
    constructor(Id, QuizQuestion, Grade, Choices = []){
        super(Id, QuizQuestion, Grade);
        this.Choices = Choices;
    }
}
