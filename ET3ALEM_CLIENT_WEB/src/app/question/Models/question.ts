import { QuestionType } from './question-type.enum';

export abstract class Question {
    Id: Number;
    Body: string;
    QuestionType: QuestionType;

    constructor(id: Number, Body: string){
        this.Id = id;
        this.Body = Body;
    }
}
