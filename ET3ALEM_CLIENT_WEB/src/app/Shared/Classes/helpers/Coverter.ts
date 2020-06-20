
export class Converter {

    static BtoMB(B: number, precision = 2): number {
        return +Math.round(B / 1048576).toFixed(precision);
    }
}