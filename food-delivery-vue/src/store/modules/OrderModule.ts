import { getModule, Module, Mutation, VuexModule } from "vuex-module-decorators";
import { IOrderRowTemp } from "@/domain/IOrderRow";
import store from "@/store";

@Module({ name: 'order' })
export default class OrderModule extends VuexModule {
    currentRestaurantId: string | null = null;
    currentRestaurantName: string | null = null;
    deliveryCost = 0;
    orderRows: IOrderRowTemp[] = [];

    get orderHasItems(): boolean {
        return this.orderRows.length !== 0;
    }

    get foodCost(): number {
        return this.orderRows.reduce(
            (a: number, b: IOrderRowTemp) => a + b.amount * b.cost + b.choices.reduce(
                (c, d) => c + d.amount * d.cost, 0), 0);
    }

    get amountOfItem() {
        return function (id: string) {
            return getModule(OrderModule, store).orderRows.filter((r: IOrderRowTemp) => r.itemId === id).reduce((a: number, b: IOrderRowTemp) => a + b.amount, 0)
        }
    }

    get totalAmount(): number {
        return this.orderRows.reduce((a: number, b: IOrderRowTemp) => a + b.amount, 0)
    }

    @Mutation
    deleteRowFromOrder(orderRow: IOrderRowTemp) {
        this.orderRows = this.orderRows.filter(o => o !== orderRow)
        if (!this.orderRows.length) {
            this.currentRestaurantName = null;
            this.currentRestaurantId = null;
            this.deliveryCost = 0;
        }
    }

    @Mutation
    setCurrentRestaurantId(id: string) {
        this.currentRestaurantId = id;
    }

    @Mutation
    setCurrentRestaurantName(name: string) {
        this.currentRestaurantName = name;
    }

    @Mutation
    setDeliveryCost(cost: number) {
        this.deliveryCost = cost;
    }

    @Mutation
    clearOrders() {
        this.orderRows = [];
    }

    @Mutation
    deleteFromOrder(id: string) {
        this.orderRows = this.orderRows.filter((o: IOrderRowTemp) => o.itemId !== id);
        if (!this.orderRows.length) {
            this.currentRestaurantName = null;
            this.currentRestaurantId = null;
            this.deliveryCost = 0;
        }
    }

    @Mutation
    addOrderRow(orderRowTemp: IOrderRowTemp) {
        if (orderRowTemp.amount) {
            this.orderRows.push(orderRowTemp)
        }
    }
}
