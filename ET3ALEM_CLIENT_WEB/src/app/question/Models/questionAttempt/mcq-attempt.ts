import { Choice } from "../choice";
import { QuestionAttempt } from "./question-attempt";
import {Type} from "class-transformer";

export class MCQAttempt extends QuestionAttempt {
    @Type(() => Choice)
    Choices: Array<Choice>;

    constructor(Id, QuizQuestion, Grade, Choices = []) {
        super(Id, QuizQuestion, Grade);
        this.Choices = Choices;
    }
}
