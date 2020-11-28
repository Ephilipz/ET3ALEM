import { Helper } from 'src/app/Shared/Classes/helpers/Helper';

export class Choice {
    Id: number;
    Body: string;
    IsAnswer: boolean = false;
    constructor(id: number = Helper.randomInteger(0,100)*-1){
        this.Id = id;
    }
}
