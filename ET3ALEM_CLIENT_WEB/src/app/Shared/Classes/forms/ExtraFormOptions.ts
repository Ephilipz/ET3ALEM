import { FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';

export class ExtraFormOptions {


    /**
     * Returns formControl error messages
     * @param formGrp formGroup object that contains the form control
     * @param name form control name
     * @param displayName (optional) the name of the field to display in the error message
     */
    public getFormErrors(formGrp: FormGroup, name: string, displayName: string = name, checkMatching = false): string {
        //field doesn't match the parent
        if (formGrp.hasError('notSame') && checkMatching) {
            return `Passwords do not match`;
        }

        let control = formGrp.get(name);
        return this.getControlErrors(control, displayName);
    }

    public getControlErrors(control: FormControl | AbstractControl, displayName: string): string {
        if (!control || control.valid || control.disabled) return null;

        let currentErrors = control.errors;

        //field has a required error
        if (currentErrors[Validators.required.name]) {
            return `${displayName} cannot be empty`;
        }


        //field has a min length error
        if (currentErrors['minlength']) {
            return `${displayName} must be at least ${currentErrors['minlength']['requiredLength']} characters`;
        }

        //field has a max length error
        if (currentErrors['maxlength']) {
            return `${displayName} must not exceed ${currentErrors['maxlength']['requiredLength']} characters`;
        }

        if (currentErrors['max']) {
            return `${displayName} must not exceed ${currentErrors['max']['max']}`;
        }

        if (currentErrors['min']) {
            return `${displayName} must be at least ${currentErrors['min']['min']}`;
        }


        return `Invalid ${displayName}`;
    }
}