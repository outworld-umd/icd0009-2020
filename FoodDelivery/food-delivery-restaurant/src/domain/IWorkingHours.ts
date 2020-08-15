export interface IWorkingHours {
    id: string;
    weekDay: DayOfWeek;
    openingTime: string;
    closingTime: string;
}

export interface IWorkingHoursCreate {
    restaurantId: string;
    weekDay: DayOfWeek;
    openingTime: string;
    closingTime: string;
}

export interface IWorkingHoursEdit {
    id: string;
    restaurantId: string;
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
    SATURDAY
}
