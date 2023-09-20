import { ExchangeRate } from "./exchange-rate.model";

export class ConversionResult {
    amount: number = 0;
    convertedAmount: number = 0;
    appliedRate: ExchangeRate = new ExchangeRate();
}