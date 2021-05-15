import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { plainToClass } from 'class-transformer';
import { ToastrService } from 'ngx-toastr';
import { QuizAttempt } from '../../Model/quiz-attempt';
import { QuizAttemptService } from '../../services/quiz-attempt.service';

@Component({
  selector: 'app-grade-quiz',
  templateUrl: './grade-quiz.component.html',
  styleUrls: ['./grade-quiz.component.css']
})
export class GradeQuizComponent implements OnInit {

  constructor(private route: ActivatedRoute, private quizAttemptService: QuizAttemptService, private toastr: ToastrService, private router: Router) { }

  quizAttempt: QuizAttempt = null;
  isLoaded = false;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.quizAttemptService.getQuizAttemptWithQuiz(id).subscribe(
        (quizAttempt) => {
          this.quizAttempt = plainToClass(QuizAttempt, quizAttempt);
          this.quizAttempt.UpdateQuestionTypes();
          this.isLoaded = true;
        },
        (err) => {
          this.toastr.error('unable to load quiz');
        }
      )
    });
  }

  updateQuizGrade() {
    this.quizAttemptService.updateQuizAttemptGrade(this.quizAttempt).subscribe(
      (res) => {
        this.toastr.success('Quiz grade updated successfully');
        this.router.navigate(['../../grades/' + this.quizAttempt.QuizId], { relativeTo: this.route })
      },
      (err) => this.toastr.error('Unable to update quiz grade')
    )
  }

  getCurrentGrade() {
    let sum = 0;
    this.quizAttempt.QuestionsAttempts.forEach(q => sum += +q.Grade);
    return +sum.toFixed(2);
  }

}
