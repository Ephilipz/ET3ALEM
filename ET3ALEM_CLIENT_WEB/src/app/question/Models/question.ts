import { Exclude } from 'class-transformer';
import { GeneralHelper } from 'src/app/Shared/Classes/helpers/GeneralHelper';
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
        const newQuestion: Question = GeneralHelper.deepCopy(this);
        newQuestion.Id = GeneralHelper.randomInteger(0, 100) * -1;
        this.duplicateQuestionAnswer(newQuestion);
        return newQuestion;
    }

    protected duplicateQuestionAnswer(newQuestion: Question) {
    };
}
