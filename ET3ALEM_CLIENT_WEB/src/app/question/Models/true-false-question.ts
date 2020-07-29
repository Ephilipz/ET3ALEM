import { Question } from './question';
import { QuestionType } from './question-type.enum';

export class TrueFalseQuestion extends Question{

    constructor(){
        super();
        this.QuestionType = QuestionType.TrueFalse;
    }
}
