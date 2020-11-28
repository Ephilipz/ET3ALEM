import { QuestionType } from './question-type.enum';

export abstract class Question {
    Id: Number;
    Body: string;
    QuestionType: QuestionType;
    QuestionCollectionId?: Number;

    constructor(id: Number, Body: string) {
        this.Id = id;
        this.Body = Body;
    }
}
