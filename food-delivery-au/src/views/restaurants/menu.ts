import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { AppState } from '../../state/app-state';
import { autoinject } from 'aurelia-framework';
import { RestaurantService } from "../../service/restaurant-service";
import { IAlertData } from "../../types/IAlertData";
import { IRestaurant } from "../../domain/IRestaurant";
import { AlertType } from "../../types/AlertType";
import { ItemTypeService } from "../../service/item-type-service";
import { ItemInTypeService } from "../../service/item-in-type-service";
import { ItemService } from "../../service/item-service";
import { IItem, IItemCreate, IItemEdit, IItemView } from "../../domain/IItem";
import { IItemType, IItemTypeCreate, IItemTypeEdit } from "../../domain/IItemType";

@autoinject
export class RestaurantMenu {

    private _alert: IAlertData | null = null;
    private _restaurant: IRestaurant | null = null;
    private _items: IItem[] = [];

    private _restaurantId: string | null = null;

    get isEdited(): boolean {
        return this._isItemTypeEdited || this._isEdited || this._isItemEdited
    }

    constructor(private restaurantService: RestaurantService, private itemTypeService: ItemTypeService, private itemInTypeService: ItemInTypeService, private itemService: ItemService, private appState: AppState, private router: Router) {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this._restaurantId = params.id;
            this.restaurantService.get(params.id).then(
                response => {
                    if (response.isSuccessful) {
                        this._alert = null;
                        this._restaurant = response.data;
                    } else {
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.messages,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                });
            this.itemService.getAll(params.id).then(
                response => {
                    if (response.isSuccessful) {
                        this._alert = null;
                        this._items = response.data;
                    } else {
                        console.log("nerjhbejhfjebf")
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.messages,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                });
        }
    }

    // ITEMTYPE-ITEMINTYPE-ITEM EDITOR/CREATOR

    private _selected: string[] = [];
    private _initial: IItemView[] = [];
    private _currentId: string | null = null;

    editCategories(id: string, items: IItemView[]): void {
        this._isEdited = true;
        this._currentId = id;
        this._selected = items.map(c => c.id);
        this._initial = items;
    }

    closeEdit(): void {
        this._isEdited = false;
        this._selected = [];
        this._initial = [];
        this._currentId = null;
    }

    async saveEdit(): Promise<void> {
        for (const view of this._initial) {
            if (!this._selected.includes(view.id)) {
                console.log("delete itemInType", view.itemInTypeId);
                await this.itemInTypeService.delete(view.itemInTypeId);
            }
        }
        for (const id of this._selected) {
            if (!this._initial.map(c => c.id).includes(id)) {
                console.log("add item", id, "to itemType", this._currentId)
                await this.itemInTypeService.post({itemId: id, itemTypeId: this._currentId});
            }
        }
        await this.restaurantService.get(this._restaurantId).then(
            response => {
                console.log("updated")
                if (response.isSuccessful) {
                    this._alert = null;
                    this._restaurant = response.data;
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        this.closeEdit();
    }

    // ITEMTYPE EDITOR/CREATOR

    private _isItemTypeEdited = false;
    private _itemTypeId: string | null = null;
    private _itemTypeName: string | null = null;
    private _itemTypeIsSpecial: boolean | null = null;
    private _itemTypeDescription: string | null = null;
    private _isEdited = false;

    get validateItemType(): boolean {
        return !(this._itemTypeName && this._restaurantId);
    }

    async submitItemType(): Promise<void> {
        if (this._itemTypeName && this._restaurantId) {
            const itemTypeCreate: IItemTypeCreate = {
                name: this._itemTypeName,
                description: this._itemTypeDescription,
                isSpecial: this._itemTypeIsSpecial ?? false,
                restaurantId: this._restaurantId
            }
            if (this._itemTypeId) {
                const itemTypeEdit: IItemTypeEdit = { id: this._itemTypeId, ...itemTypeCreate }
                console.log("update", itemTypeEdit)
                await this.itemTypeService.put(this._itemTypeId, itemTypeEdit);
            } else {
                console.log("create", itemTypeCreate)
                await this.itemTypeService.post(itemTypeCreate);
            }
            await this.restaurantService.get(this._restaurantId).then(
                response => {
                    if (response.isSuccessful) {
                        this._alert = null;
                        this._restaurant = response.data;
                    } else {
                        console.log("nerjhbejhfjebf")
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.messages,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                });
        }
        this.itemTypeInputMode(false, null)
    }

    itemTypeInputMode(turnOn: boolean, itemType: IItemType | null) {
        this._isItemTypeEdited = turnOn;
        this._itemTypeId = itemType?.id ?? null;
        this._itemTypeName = itemType?.name ?? null;
        this._itemTypeDescription = itemType?.description ?? null;
        this._itemTypeIsSpecial = itemType?.isSpecial ?? null;
        console.log(this._itemTypeName, this._itemTypeId, this._itemTypeDescription, this._itemTypeIsSpecial, this._restaurantId)
    }

    async deleteItemType(id: string): Promise<void> {
        console.log("delete")
        await this.itemTypeService.delete(id);
        await this.restaurantService.get(this._restaurantId).then(
            response => {
                if (response.isSuccessful) {
                    this._alert = null;
                    this._restaurant = response.data;
                } else {
                    console.log("nerjhbejhfjebf")
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
    }

    // ITEM EDITOR/CREATOR

    private _isItemEdited = false;
    private _itemId: string | null = null;
    private _itemName: string | null = null;
    private _itemPrice: number | null = null;
    private _itemDescription: string | null = null;

    get validateItem(): boolean {
        return !(this._itemName && this._itemPrice && this._restaurantId);
    }

    async submitItem(): Promise<void> {
        if (!this.validateItem) {
            const itemCreate: IItemCreate = {
                name: this._itemName,
                description: this._itemDescription,
                restaurantId: this._restaurantId,
                price: Number(this._itemPrice)
            }
            if (this._itemId) {
                const itemEdit: IItemEdit = { id: this._itemId, ...itemCreate }
                console.log("update", itemEdit)
                await this.itemService.put(this._itemId, itemEdit);
            } else {
                console.log("create", itemCreate)
                await this.itemService.post(itemCreate);
            }
            await this.itemService.getAll(this._restaurantId).then(
                response => {
                    if (response.isSuccessful) {
                        this._alert = null;
                        this._items = response.data;
                    } else {
                        console.log("nerjhbejhfjebf")
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.messages,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                });
        }
        this.itemInputMode(false, null)
    }

    itemInputMode(turnOn: boolean, item: IItem | null) {
        this._isItemEdited = turnOn;
        this._itemId = item?.id ?? null;
        this._itemName = item?.name ?? null;
        this._itemDescription = item?.description ?? null;
        this._itemPrice = item?.price ?? null;
        console.log(this._itemName, this._itemId, this._itemDescription, this._itemPrice, this._restaurantId)
    }

    async deleteItem(id: string): Promise<void> {
        console.log("delete")
        await this.itemService.delete(id);
        await this.itemService.getAll(this._restaurantId).then(
            response => {
                if (response.isSuccessful) {
                    this._alert = null;
                    this._items = response.data;
                } else {
                    console.log("nerjhbejhfjebf")
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });;
    }
}
