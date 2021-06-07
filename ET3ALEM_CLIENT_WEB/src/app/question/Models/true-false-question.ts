import { Question } from './question';
import { QuestionType } from './question-type.enum';

export class TrueFalseQuestion extends Question {
    Answer: boolean;

    constructor(Id: number = 0, Body: string = '', answer = false, Comment = null) {
        super(Id, Body, Comment);
        this.QuestionType = QuestionType.TrueFalse;
        this.Answer = answer;
    }

    protected duplicateQuestionAnswer(newQuestion: TrueFalseQuestion) {
    }
}
