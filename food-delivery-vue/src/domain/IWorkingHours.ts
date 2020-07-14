export interface IWorkingHours {
    id: string;
    weekday: DayOfWeek;
    openingTime: Date;
    closingTime: Date;
}

export enum DayOfWeek {
    SUNDAY,
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY
}
