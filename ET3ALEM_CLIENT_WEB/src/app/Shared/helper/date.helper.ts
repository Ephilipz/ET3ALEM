import * as dayjs from 'dayjs';
import * as utc from 'dayjs/plugin/utc';
import * as duration from 'dayjs/plugin/duration';
dayjs.extend(utc);
dayjs.extend(duration);
export class DateHelper {

    public static get now(): Date {
        return new Date();
    }

    public static get utcNow(): Date {
        return dayjs.utc().toDate();
    }

    public static addDays(date: Date, numberOfDays: number): Date {
        return dayjs(date).add(numberOfDays, 'days').toDate();
    }

    public static subtractDays(date: Date, numberOfDays: number): Date {
        return dayjs(date).subtract(numberOfDays, 'days').toDate();
    }

    public static addSeconds(date: Date, numberOfSeconds: number): Date {
        return dayjs(date).add(numberOfSeconds, 'seconds').toDate();
    }

    public static subtractSeconds(date: Date, numberOfSeconds: number): Date {
        return dayjs(date).subtract(numberOfSeconds, 'seconds').toDate();
    }

    /**
     * first is same or before second
     */
    public static isSameOrBefore(firstDate: Date, secondDate: Date): boolean {
        return dayjs(firstDate).isSame(dayjs(secondDate)) || dayjs(firstDate).isBefore(dayjs(secondDate));
    }

    /**
     * first is before second
     */
    public static isBefore(firstDate: Date, secondDate: Date): boolean {
        return dayjs(firstDate).isBefore(dayjs(secondDate));
    }

    /**
     * first is after second
     */
    public static isAfter(firstDate: Date, secondDate: Date): boolean {
        return dayjs(firstDate).isAfter(dayjs(secondDate));
    }

    public static utc(dateString: string): Date {
        return dayjs.utc(dateString).toDate();
    }

    public static difference(firstDate: Date, secondDate: Date): number {
        return dayjs(firstDate).diff(secondDate);
    }

    public static asMinutes(inputDuration: number): number {
        return dayjs.duration(inputDuration).asMinutes();
    }

    public static asSeconds(inputDuration: number): number {
        return dayjs.duration(inputDuration).asSeconds();
    }

    public static getLocalDateFromUTC(date: Date): Date {
        return dayjs.utc(date).local().toDate();
    }

    static getUTCFromLocal(date: Date): Date {
        return dayjs(date).utc().toDate();
    }

}

