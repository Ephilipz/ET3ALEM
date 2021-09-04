import { GeneralHelper } from "src/app/Shared/Classes/helpers/GeneralHelper";
import { Question } from "./question";
import { QuestionType } from "./question-type.enum";

export class ShortAnswerQuestion extends Question{
    PossibleAnswers: string;
    CaseSensitive: boolean;

    constructor(Id: number = 0, Body: string = '', Comment: string = null, PossibleAnswers: string = null, CaseSensitive: boolean = false){
        super(Id,Body, Comment);
        this.PossibleAnswers = PossibleAnswers;
        this.QuestionType = QuestionType.ShortAnswer;
        this.CaseSensitive = CaseSensitive;
    }

    public duplicateQuestionAnswer(newQuestion: ShortAnswerQuestion){
        newQuestion.PossibleAnswers = GeneralHelper.deepCopy(this.PossibleAnswers);
    }

}
