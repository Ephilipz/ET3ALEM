import {Component, OnInit} from '@angular/core';
import {ToastrService} from "ngx-toastr";
import {QuizService} from "../../services/quiz.service";
import {UngradedQuizTableVM} from "../../Model/ungraded-quiz-table-vm";
import {QuizAttempt} from "../../Model/quiz-attempt";
import {QuizAttemptService} from "../../services/quiz-attempt.service";
import {plainToClass} from "class-transformer";

@Component({
  selector: 'app-ungraded-quizzes',
  templateUrl: './ungraded-quizzes.component.html',
  styleUrls: ['./ungraded-quizzes.component.css']
})

export class UngradedQuizzesComponent implements OnInit {

  quizList: Array<UngradedQuizTableVM> = [];
  isLoaded = false;

  ungradedAttemptsForQuizzes = new Map<number, Array<QuizAttempt>>();
  displayedColumns = ['User', 'Date', 'Actions']

  constructor(private toast: ToastrService,
              private quizService: QuizService,
              private quizAttemptService: QuizAttemptService) {
  }

  ngOnInit(): void {
    this.getUngradedQuizzes();
  }

  private getUngradedQuizzes() {
    this.quizService.getUngradedQuizzes().subscribe(
      (result) => {
        this.quizList = result;
      },
      (error) => {
        this.toast.error('Unable to load ungraded quizzes')
      },
      () => {
        this.isLoaded = true;
      }
    )
  }

  public async getUngradedAttemptsForQuiz(quiz: UngradedQuizTableVM) {
    const quizId = quiz.QuizId;
    if (this.ungradedAttemptsForQuizzes.has(quizId)) {
      return;
    }
    await this.getUngradedAttemptsForQuizFormAPI(quizId);
  }

  private async getUngradedAttemptsForQuizFormAPI(quizId: number) {
    let attempts = await this.quizAttemptService.getUngradedQuizAttemptsForQuiz(quizId).toPromise();
    attempts = attempts.map(attempt => plainToClass(QuizAttempt, attempt));
    this.ungradedAttemptsForQuizzes.set(quizId, attempts);
  }
}
