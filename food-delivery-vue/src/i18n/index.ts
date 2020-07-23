import { Locales } from "./locales";
import en from './languages/en.json';
import et from './languages/et.json';
import ru from './languages/ru.json';

export const messages = {
    [Locales.EN]: en,
    [Locales.ET]: et,
    [Locales.RU]: ru
};

export const dateTimeFormats = {
    en: {
        short: {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        },
        long: {
            year: 'numeric',
            month: 'short',
            day: 'numeric',
            hour: 'numeric',
            minute: 'numeric'
        }
    },
    et: {
        short: {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        },
        long: {
            year: 'numeric',
            month: 'short',
            day: 'numeric',
            hour: 'numeric',
            minute: 'numeric'
        }
    },
    ru: {
        short: {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        },
        long: {
            year: 'numeric',
            month: 'short',
            day: 'numeric',
            hour: 'numeric',
            minute: 'numeric'
        }
    }
}

export const defaultLocale = Locales.EN;
