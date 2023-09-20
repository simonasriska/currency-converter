import { Currency } from "./currency.model";

export class ExchangeRate {
    baseCurrency: Currency = new Currency();
    targetCurrency: Currency = new Currency();
    rate: number = 0;
    date: Date = new Date();
}