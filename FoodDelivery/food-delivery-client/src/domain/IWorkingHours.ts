export interface IWorkingHours {
    id: string;
    weekDay: DayOfWeek;
    openingTime: string;
    closingTime: string;
}

export enum DayOfWeek {
    SUNDAY,
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY,
    SUNDAY2
}

export const DayNames = {
    [DayOfWeek.SUNDAY]: 'days.sunday',
    [DayOfWeek.MONDAY]: 'days.monday',
    [DayOfWeek.TUESDAY]: 'days.tuesday',
    [DayOfWeek.WEDNESDAY]: 'days.wednesday',
    [DayOfWeek.THURSDAY]: 'days.thursday',
    [DayOfWeek.FRIDAY]: 'days.friday',
    [DayOfWeek.SATURDAY]: 'days.saturday',
    [DayOfWeek.SUNDAY2]: 'days.sunday'
}
