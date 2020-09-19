import { Question } from './question';
import { QuestionType } from './question-type.enum';
import { Choice } from './choice';

export class MultipleChoiceQuestion extends Question {
    Choices: Array<Choice>;

    constructor(Id: Number = 0, Body: string = '', choices = [new Choice()]) {
        super(Id, Body);
        this.QuestionType = QuestionType.MCQ;
        this.Choices = choices
    }
}