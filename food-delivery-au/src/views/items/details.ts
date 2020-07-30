import { ItemService } from "../../service/item-service";
import { AppState } from "../../state/app-state";
import { NavigationInstruction, RouteConfig, Router } from "aurelia-router";
import { AlertType } from "../../types/AlertType";
import { autoinject } from "aurelia-framework";
import { ItemOptionService } from "../../service/item-option-service";
import { ItemChoiceService } from "../../service/item-choice-service";
import { NutritionInfoService } from "../../service/nutrition-info-service";
import { IAlertData } from "../../types/IAlertData";
import { IItem, IItemCreate, IItemEdit } from "../../domain/IItem";
import { INutritionInfo, INutritionInfoCreate, INutritionInfoEdit } from "../../domain/INutritionInfo";
import { IOption, IOptionCreate, IOptionEdit } from "../../domain/IOption";
import { IChoice, IChoiceCreate, IChoiceEdit } from "../../domain/IChoice";

@autoinject
export class ItemIndex {

    private loading = false;

    private _alert: IAlertData | null = null;
    private _item: IItem | null = null;
    private _itemId: string | null = null;

    get isEdited(): boolean {
        return this._isInfoEdited || this._isOptionEdited || this._isChoiceEdited
    }

    constructor(private itemService: ItemService, private itemOptionService: ItemOptionService, private itemChoiceService: ItemChoiceService, private nutritionInfoService: NutritionInfoService, private appState: AppState, private router: Router) {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this._itemId = params.id;
            this.itemService.get(this._itemId).then(
                response => {
                    if (response.isSuccessful) {
                        this._alert = null;
                        this._item = response.data;
                    } else {
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.messages,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                });
        }

    }

    // ITEMOPTION EDITOR/CREATOR

    private _isChoiceEdited = false;
    private _optionId: string | null = null;
    private _choiceId: string | null = null;
    private _choiceName: string | null = null;
    private _choicePrice: number | null = null;

    get validateChoice(): boolean {
        return !(this._choiceName);
    }

    async getItem(id: string) {
        await this.itemService.get(id).then(
            response => {
                if (response.isSuccessful) {
                    this._alert = null;
                    this._item = response.data;
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
    }

    async submitChoice(): Promise<void> {
        this.loading = true;
        if (!this.validateChoice) {
            const choiceCreate: IChoiceCreate = {
                additionalPrice: Number(this._choicePrice ?? 0),
                name: this._choiceName,
                itemOptionId: this._optionId
            }
            if (this._choiceId) {
                const choiceEdit: IChoiceEdit = { id: this._choiceId, ...choiceCreate }
                console.log("update", choiceEdit)
                await this.itemChoiceService.put(this._choiceId, choiceEdit);
            } else {
                console.log("create", choiceCreate)
                await this.itemChoiceService.post(choiceCreate);
            }
            await this.getItem(this._itemId);
        }
        this.choiceInputMode(false, null, null)
        this.loading = false;
    }

    choiceInputMode(turnOn: boolean, choice: IChoice | null, optionId: string | null) {
        this._isChoiceEdited = turnOn;
        this._optionId = optionId;
        this._choiceName = choice?.name ?? null;
        this._choicePrice = choice?.additionalPrice ?? null;
        this._choiceId = choice?.id ?? null;
        console.log(this._optionId, this._choiceId, this._choiceName, this._choicePrice)
    }

    async deleteChoice(id: string): Promise<void> {
        this.loading = true;
        console.log("delete", id)
        await this.itemChoiceService.delete(id);
        await this.getItem(this._itemId);
        this.loading = false;
    }


    // ITEMOPTION EDITOR/CREATOR

    private _isOptionEdited = false;
    private _optionName: string | null = null;
    private _optionSingle = false;
    private _optionRequired = false;

    get validateOption(): boolean {
        return !(this._optionName && this._itemId);
    }

    async submitOption(): Promise<void> {
        this.loading = true;
        if (!this.validateOption) {
            const optionCreate: IOptionCreate = {
                isSingle: this._optionSingle,
                itemId: this._itemId,
                name: this._optionName,
                isRequired: this._optionRequired
            }
            if (this._optionId) {
                const optionEdit: IOptionEdit = { id: this._optionId, ...optionCreate }
                console.log("update", optionEdit)
                await this.itemOptionService.put(this._optionId, optionEdit);
            } else {
                console.log("create", optionCreate)
                await this.itemOptionService.post(optionCreate);
            }
            await this.getItem(this._itemId);
        }
        this.optionInputMode(false, null);
        this.loading = false;
    }

    optionInputMode(turnOn: boolean, option: IOption | null) {
        this._isOptionEdited = turnOn;
        this._optionId = option?.id ?? null;
        this._optionName = option?.name ?? null;
        this._optionSingle = option?.isSingle ?? false;
        this._optionRequired = option?.isRequired ?? false;
        console.log(this._optionId, this._optionName, this._optionSingle, this._optionRequired, this._itemId)
    }

    async deleteOption(id: string): Promise<void> {
        this.loading = true;
        console.log("delete", id)
        await this.itemOptionService.delete(id);
        await this.getItem(this._itemId);
        this.loading = false;
    }

    // NUTRITIONINFO EDITOR/CREATOR

    private _isInfoEdited = false;
    private _infoId: string | null = null;
    private _infoName: string | null = null;
    private _infoAmount: number | null = null;
    private _infoUnit: string | null = null;

    get validateInfo(): boolean {
        return !(this._infoName && this._infoAmount && this._infoUnit !== null);
    }

    async submitInfo(): Promise<void> {
        this.loading = true;
        if (!this.validateInfo) {
            const infoCreate: INutritionInfoCreate = {
                amount: Number(this._infoAmount),
                itemId: this._itemId,
                unit: this._infoUnit,
                name: this._infoName
            }
            if (this._infoId) {
                const infoEdit: INutritionInfoEdit = { id: this._infoId, ...infoCreate }
                console.log("update", infoEdit)
                const response = await this.nutritionInfoService.put(this._infoId, infoEdit);
                console.log(response)
            } else {
                console.log("create", infoCreate)
                await this.nutritionInfoService.post(infoCreate);
            }
            await this.getItem(this._itemId);
        }
        this.infoInputMode(false, null);
        this.loading = false;
    }

    infoInputMode(turnOn: boolean, info: INutritionInfo | null) {
        this._isInfoEdited = turnOn;
        this._infoId = info?.id ?? null;
        this._infoName = info?.name ?? null;
        this._infoAmount = info?.amount ?? null;
        this._infoUnit = info?.unit ?? null;
        console.log(this._infoId, this._infoName, this._infoAmount, this._infoUnit)
    }

    async deleteInfo(id: string): Promise<void> {
        this.loading = true;
        console.log("delete", id)
        await this.nutritionInfoService.delete(id);
        await this.getItem(this._itemId);
        this.loading = false;
    }
}
