export interface IWorkingHours {
    id: string;
    weekday: DayOfWeek;
    openingTime: string;
    closingTime: string;
}

export interface IWorkingHoursCreate {
    restaurantId: string;
    weekday: DayOfWeek;
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
    SATURDAY
}
