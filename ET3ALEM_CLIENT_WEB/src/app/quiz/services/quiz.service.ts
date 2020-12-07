import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Quiz } from '../Model/quiz';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class QuizService {

  constructor(private http: HttpClient) { }

  createQuiz(quiz: Quiz) {
    return this.http.post(environment.baseUrl + '/api/Quiz', quiz);
  }

  getQuiz(id: Number) {
    return this.http.get<Quiz>(environment.baseUrl + '/api/Quiz/' + id);
  }
  
  getQuizTitleFromCode(code: string) {
    return this.http.get(environment.baseUrl + '/api/Quiz/GetQuizTitleFromCode/' + code);
  }

  getQuizzes() {
    return this.http.get<Array<Quiz>>(environment.baseUrl + '/api/Quiz');
  }

  updateQuiz(quiz: Quiz) {
    return this.http.put(environment.baseUrl + '/api/Quiz/' + quiz.Id, quiz);
  }

  delete(id: Number) {
    return this.http.delete<Quiz>(environment.baseUrl + '/api/Quiz', {
      params: {
        'id': id.toString()
      }
    });
  }
}
