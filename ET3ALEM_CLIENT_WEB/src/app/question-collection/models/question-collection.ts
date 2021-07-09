import { Transform, Type } from "class-transformer";
import { RegisterUser } from "src/app/auth/Model/User";
import { Question } from "src/app/question/Models/question";
import { QuestionTypeResolver } from "src/app/question/shared/question-type-resolver";

export class QuestionCollection {
    public Id: number;
    public Name: string;
    @Transform((value, obj, type) => QuestionTypeResolver.getSpecificQuestionList(value))
    public Questions: Array<Question>;
    public UserId: string;
    public User: RegisterUser;
    @Type(() => Date)
    public CreatedDate: Date;

    constructor(Id = 0, Name = 'Collection', Questions: Array<Question> = []){
        this.Id = Id;
        this.Name = Name;
        this.Questions = Questions;
    }
}
