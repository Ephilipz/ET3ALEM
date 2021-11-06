import { GeneralHelper } from 'src/app/Shared/Classes/helpers/GeneralHelper';

export class Choice {
    Id: number;
    Body: string;
    IsAnswer: boolean = false;
    constructor(id: number = GeneralHelper.randomInteger(0,100)*-1){
        this.Id = id;
    }
}
