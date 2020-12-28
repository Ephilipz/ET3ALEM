import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Quiz } from '../Model/quiz';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class QuizService {

  private baseRoute = environment.baseUrl + '/api/Quiz';
  
  constructor(private http: HttpClient) { }
  
  createQuiz(quiz: Quiz) {
    return this.http.post(this.baseRoute, quiz);
  }
  
  getQuiz(id: Number) {
    return this.http.get<Quiz>(this.baseRoute + `/${id}`);
  }
  
  getQuizTitleFromCode(code: string) {
    return this.http.get(this.baseRoute + '/GetQuizTitleFromCode/' + code);
  }
  
  getQuizzes() {
    return this.http.get<Array<Quiz>>(this.baseRoute);
  }

  getBasicQuizFromCode(code: string) {
    return this.http.get<Quiz>(this.baseRoute + '/GetBasicQuizByCode/' + code);
  }
  
  updateQuiz(quiz: Quiz) {
    return this.http.put(this.baseRoute + `/${quiz.Id}`, quiz);
  }
  
  delete(id: Number) {
    return this.http.delete<Quiz>(this.baseRoute, {
      params: {
        'id': id.toString()
      }
    });
  }

}
