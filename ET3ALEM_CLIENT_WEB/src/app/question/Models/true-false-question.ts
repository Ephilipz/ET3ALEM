import { Question } from './question';
import { QuestionType } from './question-type.enum';

export class TrueFalseQuestion extends Question {
    Answer: boolean;

    constructor(Id: Number = 0, Body: string = '', answer = true) {
        super(Id, Body);
        this.QuestionType = QuestionType.TrueFalse;
        this.Answer = answer;
    }
}
