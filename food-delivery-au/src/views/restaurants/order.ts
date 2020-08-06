import { autoinject } from "aurelia-framework";
import { IAlertData } from "../../types/IAlertData";
import { AppState } from "../../state/app-state";
import { NavigationInstruction, RouteConfig, Router } from "aurelia-router";
import { OrderService } from "../../service/order-service";
import { AlertType } from "../../types/AlertType";
import { IOrder, IOrderEdit, IOrderView, OrderStatus } from "../../domain/IOrder";
import { IOrderRow } from "../../domain/IOrderRow";
import { IOrderItemChoice } from "../../domain/IOrderItemChoice";

@autoinject
export class OrderDetails {

    private loading = false;

    private _alert: IAlertData | null = null;
    private _order: IOrder | null = null;

    private _orderId: string | null = null;

    get isEdited(): boolean {
        return false;
    }

    constructor(private orderService: OrderService, private appState: AppState, private router: Router) {
    }

    async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this._orderId = params.id;
            await this.getOrder(this._orderId);
        }
    }

    totalCost(orderView: IOrderView): string {
        return (orderView.deliveryCost + orderView.foodCost).toFixed(2)
    }

    rowCost(row: IOrderRow): number {
        return row.cost * row.amount + row.choices.reduce((a: number, b: IOrderItemChoice) => a + b.amount * b.cost, 0)
    }

    async getOrder(id: string) {
        this.loading = true;
        await this.orderService.get(id).then(
            response => {
                if (response.isSuccessful) {
                    this._alert = null;
                    this._order = response.data;
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

    async changeStatus(status: OrderStatus): Promise<void> {
        this.loading = true;
        const order: IOrderEdit = this._order;
        order.orderStatus = status
        await this.orderService.put(this._orderId, order);
        this.loading = false;
    }
}
