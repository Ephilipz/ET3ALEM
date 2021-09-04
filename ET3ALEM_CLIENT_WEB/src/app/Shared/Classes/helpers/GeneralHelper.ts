import * as moment from 'moment';

export class GeneralHelper {

    static deepCopy(input: any) {
        return JSON.parse(JSON.stringify(input))
    }

    static BtoMB(B: number, precision = 2): number {
        return +Math.round(B / 1048576).toFixed(precision);
    }

    static randomInteger(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    static getLocalDateFromUTC(date: Date): Date {
        return moment.utc(date).local().toDate();
    }

    static getUTCFromLocal(date: Date): Date {
        return moment(date).utc().toDate();
    }

    static getProperty(obj, path) {
        return path.split('.').reduce((o, p) => o && o[p], obj);
    }

}
