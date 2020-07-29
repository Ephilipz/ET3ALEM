import { QuestionType } from './question-type.enum';

export abstract class Question {
    Id: Number;
    Body: string;
    QuestionType: QuestionType;
}
