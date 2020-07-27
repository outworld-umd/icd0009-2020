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
                        this._item = {
                            id: "1",
                            name: "Burger Meal",
                            price: 2.3,
                            description: "Very vkusno",
                            restaurantId: "4",
                            nutritionInfos: [
                                {
                                    id: "1",
                                    name: "Sugar",
                                    amount: 50,
                                    unit: "g"
                                },
                                {
                                    id: "2",
                                    name: "Calories",
                                    amount: 600,
                                    unit: "kcal"
                                }
                            ],
                            options: [
                                {
                                    id: "option1",
                                    name: "Fries",
                                    isSingle: true,
                                    isRequired: true,
                                    choices: [
                                        {
                                            id: "choice11",
                                            name: "Small fries",
                                            additionalPrice: 0
                                        },
                                        {
                                            id: "choice12",
                                            name: "Medium fries",
                                            additionalPrice: 0.3
                                        },
                                        {
                                            id: "choice13",
                                            name: "Small fries",
                                            additionalPrice: 0.5
                                        }
                                    ]
                                },
                                {
                                    id: "option2",
                                    name: "Drinks",
                                    isSingle: false,
                                    isRequired: true,
                                    choices: [
                                        {
                                            id: "choice21",
                                            name: "Coca-Cola Cherry",
                                            additionalPrice: 0.3
                                        },
                                        {
                                            id: "choice22",
                                            name: "Coca-Cola Vanilla",
                                            additionalPrice: 0.5
                                        },
                                        {
                                            id: "choice23",
                                            name: "Coca-Cola Peach",
                                            additionalPrice: 0.5
                                        }
                                    ]
                                },
                                {
                                    id: "option3",
                                    name: "Sauce",
                                    isSingle: true,
                                    isRequired: false,
                                    choices: [
                                        {
                                            id: "choice31",
                                            name: "Gay Sauce",
                                            additionalPrice: 0
                                        },
                                        {
                                            id: "choice32",
                                            name: "Not Gay Sauce",
                                            additionalPrice: 0.3
                                        }
                                    ]
                                },
                                {
                                    id: "option4",
                                    name: "Other",
                                    isSingle: false,
                                    isRequired: false,
                                    choices: [
                                        {
                                            id: "choice41",
                                            name: "Condoms",
                                            additionalPrice: 0.69
                                        },
                                        {
                                            id: "choice42",
                                            name: "Plastic Bag",
                                            additionalPrice: 20
                                        }
                                    ]
                                }
                            ]
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

    async submitChoice(): Promise<void> {
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
            await this.itemService.get(this._itemId).then(
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
        this.choiceInputMode(false, null, null)
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
        console.log("delete", id)
        await this.itemChoiceService.delete(id);
        await this.itemService.get(this._itemId).then(
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


    // ITEMOPTION EDITOR/CREATOR

    private _isOptionEdited = false;
    private _optionName: string | null = null;
    private _optionSingle = false;
    private _optionRequired = false;

    get validateOption(): boolean {
        return !(this._optionName && this._itemId);
    }

    async submitOption(): Promise<void> {
        if (!this.validateOption) {
            const optionCreate: IOptionCreate = {
                isSingle: this._optionSingle,
                itemId: this._itemId,
                name: this._optionName,
                isRequired: this._optionRequired
            }
            if (this._optionId) {
                const optionEdit: IOptionEdit = { id: this._itemId, ...optionCreate }
                console.log("update", optionEdit)
                await this.itemOptionService.put(this._itemId, optionEdit);
            } else {
                console.log("create", optionCreate)
                await this.itemOptionService.post(optionCreate);
            }
            await this.itemService.get(this._itemId).then(
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
        this.optionInputMode(false, null)
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
        console.log("delete", id)
        await this.itemOptionService.delete(id);
        await this.itemService.get(this._itemId).then(
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
                await this.nutritionInfoService.put(this._itemId, infoEdit);
            } else {
                console.log("create", infoCreate)
                await this.nutritionInfoService.post(infoCreate);
            }
            await this.itemService.get(this._itemId).then(
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
        this.infoInputMode(false, null)
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
        console.log("delete", id)
        await this.nutritionInfoService.delete(id);
        await this.itemService.get(this._itemId).then(
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
