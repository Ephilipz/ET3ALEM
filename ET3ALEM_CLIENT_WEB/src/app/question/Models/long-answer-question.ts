import { Question } from "./question"
import { QuestionType } from "./question-type.enum";

export class LongAnswerQuestion extends Question {
    public constructor(id: number = 0, body: string = '', comment: string = '') {
        super(id, body, comment);
        this.QuestionType = QuestionType.LongAnswer;
    }
}