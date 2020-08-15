import { autoinject } from "aurelia-framework";
import { IAlertData } from "../../types/IAlertData";
import { AppState } from "../../state/app-state";
import { NavigationInstruction, RouteConfig, Router } from "aurelia-router";
import { OrderService } from "../../service/order-service";
import { AlertType } from "../../types/AlertType";
import { IOrderView } from "../../domain/IOrder";

@autoinject
export class OrderIndex {

    private loading = false;

    private _alert: IAlertData | null = null;
    private _orders: IOrderView[] = [];

    private _restaurantId: string | null = null;

    get isEdited(): boolean {
        return false;
    }

    constructor(private orderService: OrderService, private appState: AppState, private router: Router) {
    }

    async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this._restaurantId = params.id;
            await this.getOrders(this._restaurantId);
        }
    }

    totalCost(orderView: IOrderView): string {
        return (orderView.deliveryCost + orderView.foodCost).toFixed(2)
    }

    localDate(orderView: IOrderView): string {
        return new Date(orderView.createdAt).toLocaleString('en-GB');
    }

    async getOrders(id: string) {
        this.loading = true;
        await this.orderService.getAll(id).then(
            response => {
                if (response.isSuccessful) {
                    this._alert = null;
                    this._orders = response.data;
                } else {
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.messages,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            });
        this.loading = false;
    }
}
