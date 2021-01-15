export abstract class AC_ConcreteEditQuestion {
    getQuestion(): any{
        if(!this.validate())
            return null;
    }
    protected abstract validate(): boolean;
    inputQuestion: any = null;
}
