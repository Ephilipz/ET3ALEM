import { Choice } from "./choice";
import { QuestionAttempt } from "./question-attempt";

export class MCQAttempt extends QuestionAttempt {
    Choices: Array<Choice>;
    constructor(Id, QuizQuestionId, Grade, Choices = []){
        super(Id, QuizQuestionId, Grade);
        this.Choices = Choices;
    }
}
