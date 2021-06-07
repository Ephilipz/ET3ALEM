import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { plainToClass } from 'class-transformer';
import { ToastrService } from 'ngx-toastr';
import { QuestionResultHeaderComponent } from 'src/app/question/question result/question-result-header/question-result-header.component';
import { QuizAttempt } from '../../../Model/quiz-attempt';
import { QuizAttemptService } from '../../../services/quiz-attempt.service';

@Component({
  selector: 'app-view-quiz-result',
  templateUrl: './view-quiz-result.component.html',
  styleUrls: ['./view-quiz-result.component.css']
})
export class ViewQuizResultComponent implements OnInit {

  quizAttempt: QuizAttempt;
  isLoaded = false;
  showGrade = true;
  @ViewChildren('QuestionResult') private QuestionResultComponents: QueryList<QuestionResultHeaderComponent>;

  constructor(private quizAttemptService: QuizAttemptService, private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.quizAttemptService.getQuizAttemptWithQuiz(id).subscribe(
        (quizAttempt) => {
          this.quizAttempt = plainToClass(QuizAttempt, quizAttempt);
          this.isLoaded = true;
        },
        (err) => {
          this.toastr.error('unable to load quiz');
        }
      )
    });
  }

  quizGrade(){
    let sum = 0;
    this.quizAttempt.QuestionsAttempts.forEach(questionAttempt => {
      sum += questionAttempt.QuizQuestion.Grade;
    });
    return sum;
  }

}
