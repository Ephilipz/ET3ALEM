import {Question} from "../../Models/question";

export abstract class AC_ConcreteEditQuestion {
  protected constructor() {
  }

  saveQuestion(): any {
    this.validate();
  }

  getQuestion(): Question {
    return this.inputQuestion;
  }

  protected abstract validate();

  inputQuestion: any = null;

  checkQuestionValidation() {
    this.validate();
  }
}
