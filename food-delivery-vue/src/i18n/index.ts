import { Locales } from "./locales";
import en from './languages/en.json';
import et from './languages/et.json';
import ru from './languages/ru.json';

export const messages = {
    [Locales.EN]: en,
    [Locales.ET]: et,
    [Locales.RU]: ru
};

export const defaultLocale = Locales.EN;
