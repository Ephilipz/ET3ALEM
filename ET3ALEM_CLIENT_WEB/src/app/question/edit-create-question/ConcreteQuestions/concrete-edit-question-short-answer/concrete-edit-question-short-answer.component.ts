import { Component, Input, OnInit } from '@angular/core';
import { plainToClass } from 'class-transformer';
import { ToastrService } from 'ngx-toastr';
import { ShortAnswerQuestion } from 'src/app/question/Models/short-answer-question';
import { Helper } from 'src/app/Shared/Classes/helpers/Helper';
import { AC_ConcreteEditQuestion } from '../ac-concrete-question';

@Component({
  selector: 'app-concrete-edit-question-short-answer',
  templateUrl: './concrete-edit-question-short-answer.component.html',
  styleUrls: ['./concrete-edit-question-short-answer.component.css']
})
export class ConcreteEditQuestionShortAnswerComponent extends AC_ConcreteEditQuestion implements OnInit {

  @Input() inputQuestion: ShortAnswerQuestion;
  possibleAnswers: Array<{ Id: number, value: string }> = [];

  //used to keep track of the generated ids so no ids are the same
  private idsStart: number = Helper.randomInteger(0, 100);

  constructor(private toastrService: ToastrService) {
    super();
  }

  ngOnInit(): void {
    this.inputQuestion = plainToClass(ShortAnswerQuestion, this.inputQuestion);
    if (!this.inputQuestion.PossibleAnswers) {
      this.addAnswer();
    }
    else {
      this.possibleAnswers = this.inputQuestion.PossibleAnswers.split(',').map(res => {
        return { Id: ++this.idsStart, value: res }
      });
    }
  }

  addAnswer() {
    this.possibleAnswers.push({ Id: ++this.idsStart, value: null });
  }

  removeAnswer(id) {
    let index = this.possibleAnswers.findIndex(c => c.Id == id);
    if (index > -1) {
      this.possibleAnswers.splice(index, 1);
    }
  }

  validate() {
    //no answer was entered
    if (!this.possibleAnswers.find(ans => ans.value && ans.value.trim().length > 0))
      this.toastrService.warning('No correct answer was added to the short answer question')
  }

  public getQuestion(){
    this.inputQuestion.PossibleAnswers = this.possibleAnswers.filter(res => res.value && res.value.trim().length > 0).map(res => res.value.trim()).join(',');
    return this.inputQuestion;
  }

  public saveQuestion() {
    super.saveQuestion();
    //get non null and non empty values from possible answers list and add them to the question
    // this.inputQuestion.PossibleAnswers = this.possibleAnswers.filter(res => res.value && res.value.trim().length > 0).map(res => res.value.trim()).join(',');
    return this.getQuestion();
  }

}
