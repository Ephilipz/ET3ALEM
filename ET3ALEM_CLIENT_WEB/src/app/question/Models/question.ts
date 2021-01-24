import { Exclude } from 'class-transformer';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper';
import { QuestionType } from './question-type.enum';

export abstract class Question {
    Id: number;
    Body: string;
    QuestionType: QuestionType;
    QuestionCollectionId?: number;
    
    constructor(id: number, Body: string) {
        this.Id = id;
        this.Body = Body;
    }

    duplicateQuestion() : Question{
        const newQuestion: Question = Helper.deepCopy(this);
        newQuestion.Id = Helper.randomInteger(0,100)*-1;
        this.duplicateQuestionAnswer(newQuestion);
        return newQuestion;
    }

    protected abstract duplicateQuestionAnswer(newQuestion: Question);
}
