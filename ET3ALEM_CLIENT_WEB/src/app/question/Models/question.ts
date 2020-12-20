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
}
