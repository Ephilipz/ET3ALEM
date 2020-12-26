import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Quiz } from '../../Model/quiz';
import { QuizService } from '../../services/quiz.service'; 
// import { documentToHtmlString } from '@contentful/rich-text-html-renderer';

@Component({
  selector: 'app-quiz-details',
  templateUrl: './quiz-details.component.html',
  styleUrls: ['./quiz-details.component.css']
})
export class QuizDetailsComponent implements OnInit {

  quiz: Quiz = null;
  isLoaded = false;
  // private instructionsHTML = '';

  constructor(private route : ActivatedRoute, private quizService: QuizService, private toastr: ToastrService) { }
  
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let code = params['code'];
      this.quizService.getBasicQuizFromCode(code).subscribe(
        (quiz) => {
          this.quiz = quiz;
          // this.instructionsHTML = documentToHtmlString(quiz.Instructions);
          this.isLoaded = true;
        },
        (err)=>{
          this.toastr.error('unable to load this quiz');
          this.isLoaded = true;
        }
      )
    });
  }  

}
