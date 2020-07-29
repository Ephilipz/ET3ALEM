import { Question } from './question';
import { QuestionType } from './question-type.enum';
import { Choice } from './choice';

export class MultipleChoiceQuestion extends Question {
    Choices: Array<Choice>;
    QuestionType: QuestionType

    constructor() {
        super();
        this.QuestionType = QuestionType.MCQ;
    }
}