import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { QuestionCollection } from './models/question-collection';

@Injectable({
  providedIn: 'root'
})
export class QuestionCollectionService {
  private baseRoute = environment.baseUrl + '/api/QuestionCollection';

  constructor(private http: HttpClient) { }
  delete(id: number) {
    return this.http.delete<QuestionCollection>(this.baseRoute, {
      params: {
        id: id.toString()
      }
    });
  }
  getCollections() {
    return this.http.get<Array<QuestionCollection>>(this.baseRoute);
  }
  getCollection(id: number) {
    return this.http.get<QuestionCollection>(this.baseRoute + `/${id}`);
  }
  createCollection(collection: QuestionCollection) {
    return this.http.post(this.baseRoute, collection);
  }
  updateCollection(collection: QuestionCollection) {
    return this.http.put(this.baseRoute + `/${collection.Id}`, collection);
  }
}
