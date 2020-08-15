import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { RestaurantService } from "../../service/restaurant-service";
import { IAlertData } from "../../types/IAlertData";
import { IRestaurant, IRestaurantEdit } from "../../domain/IRestaurant";
import { AlertType } from "../../types/AlertType";
import { ItemTypeService } from "../../service/item-type-service";
import { ItemInTypeService } from "../../service/item-in-type-service";
import { ItemService } from "../../service/item-service";
import { IItem, IItemCreate, IItemEdit, IItemView } from "../../domain/IItem";
import { IItemType, IItemTypeCreate, IItemTypeEdit } from "../../domain/IItemType";
import { DayOfWeek, IWorkingHours, IWorkingHoursCreate, IWorkingHoursEdit } from "../../domain/IWorkingHours";
import { WorkingHoursService } from "../../service/working-hours-service";

@autoinject
export class RestaurantDetails {

    private loading = false;

    private _alert: IAlertData | null = null;
    private _restaurant: IRestaurant | null = null;

    private _restaurantId: string | null = null;

    get isEdited(): boolean {
        return this._isRestaurantEdited || this._isWorkingHourEdited
    }

    constructor(private restaurantService: RestaurantService, private workingHoursService: WorkingHoursService, private appState: AppState, private router: Router) {
    }

    async getRestaurant(id: string) {
        this.restaurantService.get(id).then(
            response => {
                if (response.isSuccessful) {
                    this._alert = null;
                    this._restaurant = response.data;
                    console.log(this._restaurant)
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
    }

    _dayNames = {
        [DayOfWeek.SUNDAY]: 'Sunday',
        [DayOfWeek.MONDAY]: 'Monday',
        [DayOfWeek.TUESDAY]: 'Tuesday',
        [DayOfWeek.WEDNESDAY]: 'Wednesday',
        [DayOfWeek.THURSDAY]: 'Thursday',
        [DayOfWeek.FRIDAY]: 'Friday',
        [DayOfWeek.SATURDAY]: 'Saturday'
    }

    get workingHours(): IWorkingHours[] {
        const workingHours = this._restaurant.workingHourses ?? [];
        return workingHours.sort((h1, h2) => h1.weekDay - h2.weekDay)
    }

    getWeekdayName(day: DayOfWeek): string {
        return this._dayNames[day];
    }

    getTime(dateString: string): string {
        const date = new Date(dateString);
        return date.toLocaleString('et-EE').match(/\d+:\d+( [AP]M)?/)?.shift() ?? "";
    }

    async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this._restaurantId = params.id;
            await this.getRestaurant(this._restaurantId);
        }
    }

    // RESTAURANT EDITOR/CREATOR

    private _name: string | null = null;
    private _deliveryCost: number | null = null;
    private _phone: string | null = null;
    private _address: string | null = null;
    private _description: string | null = null;
    private _isRestaurantEdited = false;

    get validateRestaurant(): boolean {
        return !!(this._name && this._restaurantId);
    }

    async submitRestaurant(): Promise<void> {
        this.loading = true;
        if (this.validateRestaurant) {
            const restaurantEdit: IRestaurantEdit = {
                address: this._address,
                deliveryCost: Number(this._deliveryCost),
                id: this._restaurantId,
                phone: this._phone,
                name: this._name,
                description: this._description
            }
            console.log("update", restaurantEdit)
            await this.restaurantService.put(this._restaurantId, restaurantEdit);
            await this.getRestaurant(this._restaurantId);
        }
        this.restaurantInputMode(false, null)
        this.loading = false;
    }

    restaurantInputMode(turnOn: boolean, restaurant: IRestaurant | null) {
        this._isRestaurantEdited = turnOn;
        console.log(restaurant)
        this._name = restaurant?.name ?? null;
        this._description = restaurant?.description ?? null;
        this._deliveryCost = restaurant?.deliveryCost ?? null;
        this._address = restaurant?.address ?? null;
        this._phone = restaurant?.phone ?? null;
        console.log(this._name, this._address, this._description, this._phone, this._restaurantId)
    }

    private _isWorkingHourEdited = false;
    private _id: string | null = null;
    private _openingTime: string | null = null;
    private _weekDay: DayOfWeek | null = null;
    private _closingTime: string | null = null;

    get validateWorkingHour(): boolean {
        return !(this._openingTime && this._closingTime && this._weekDay !== null);
    }

    get availableWorkingHours(): number[] {
        const numbers = [0, 1, 2, 3, 4, 5, 6]
        const wh = this._restaurant.workingHourses.map(e => e.weekDay)
        return numbers.filter(e => !wh.includes(e))
    }

    async submitWorkingHour(): Promise<void> {
        this.loading = true;
        console.log(this._openingTime, this._closingTime)
        if (!this.validateWorkingHour) {
            const opening = new Date(0)
            opening.setHours(Number(this._openingTime.split(":")[0]) + 3);
            opening.setMinutes(Number(this._openingTime.split(":")[1]));
            const closing = new Date(0)
            closing.setHours(Number(this._closingTime.split(":")[0]) + 3);
            closing.setMinutes(Number(this._closingTime.split(":")[1]));
            const itemCreate: IWorkingHoursCreate = {
                closingTime: closing.toISOString(),
                restaurantId: this._restaurantId,
                weekDay: this._weekDay,
                openingTime: opening.toISOString()
            }
            if (this._id) {
                const itemEdit: IWorkingHoursEdit = { id: this._id, ...itemCreate }
                console.log("update", itemEdit)
                await this.workingHoursService.put(this._id, itemEdit);
            } else {
                console.log("create", itemCreate)
                await this.workingHoursService.post(itemCreate);
            }
            await this.getRestaurant(this._restaurantId);
        }
        this.workingHourInputMode(false, null)
        this.loading = false;
    }

    workingHourInputMode(turnOn: boolean, item: IWorkingHours | null) {
        this._isWorkingHourEdited = turnOn;
        this._id = item?.id ?? null;
        this._openingTime = item?.openingTime ?? null;
        this._weekDay = item?.weekDay ?? null;
        this._closingTime = item?.closingTime ?? null;
        console.log(this._id, this._openingTime, this._weekDay, this._closingTime)
    }

    async deleteWorkingHour(id: string): Promise<void> {
        this.loading = true;
        console.log("delete")
        await this.workingHoursService.delete(id);
        await this.getRestaurant(this._restaurantId);
        this.loading = false;
    }
}
