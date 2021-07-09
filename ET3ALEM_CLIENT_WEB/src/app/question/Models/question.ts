import { Exclude } from 'class-transformer';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper';
import { QuestionType } from './question-type.enum';

export abstract class Question {
    Id: number;
    Body: string;
    QuestionType: QuestionType;
    Comment: string;
    QuestionCollectionId: number;

    constructor(id: number, Body: string, Comment: string) {
        this.Id = id;
        this.Body = Body;
        this.Comment = Comment;
    }

    duplicateQuestion(): Question {
        const newQuestion: Question = Helper.deepCopy(this);
        newQuestion.Id = Helper.randomInteger(0, 100) * -1;
        this.duplicateQuestionAnswer(newQuestion);
        return newQuestion;
    }

    protected duplicateQuestionAnswer(newQuestion: Question) {
    };
}
