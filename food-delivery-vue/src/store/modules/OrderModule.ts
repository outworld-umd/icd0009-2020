import { getModule, Module, Mutation, VuexModule, Action } from "vuex-module-decorators";
import { IOrderRowTemp } from "@/domain/IOrderRow";
import store from "@/store";
import { IOrderCreate, IOrderTemp } from "@/domain/IOrder";
import router from "@/router";

@Module({ name: 'order' })
export default class OrderModule extends VuexModule {
    currentRestaurantId: string | null = null;
    currentRestaurantName: string | null = null;
    deliveryCost = 0;
    orderRows: IOrderRowTemp[] = [];
    loading = false;

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
    setLoading(loading: boolean) {
        console.log(this.loading)
        this.loading = loading;
        console.log(this.loading)
    }

    @Mutation
    increment(row: IOrderRowTemp) {
        const index = this.orderRows.findIndex(r => r === row);
        row.amount++;
        row.choices.forEach(c => c.amount++);
        this.orderRows[index] = row;
    }

    @Mutation
    decrement(row: IOrderRowTemp) {
        const index = this.orderRows.findIndex(r => r === row);
        row.amount--;
        row.choices.forEach(c => c.amount--);
        this.orderRows[index] = row;
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
        this.currentRestaurantName = null;
        this.currentRestaurantId = null;
        this.deliveryCost = 0;
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

    @Action
    async createOrder(orderTemp: IOrderTemp): Promise<boolean> {
        const orderCreate: IOrderCreate = {
            address: orderTemp.address,
            apartment: orderTemp.apartment,
            comment: orderTemp.comment,
            deliveryCost: this.deliveryCost,
            foodCost: this.foodCost,
            orderRows: this.orderRows,
            orderStatus: orderTemp.orderStatus,
            paymentMethod: orderTemp.paymentMethod,
            restaurantId: getModule(OrderModule, store).currentRestaurantId ?? undefined,
            restaurantName: getModule(OrderModule, store).currentRestaurantId ?? undefined
        }
        getModule(OrderModule, store).setLoading(true);
        // FOR TESTING START
        function sleep(ms: number) {
            return new Promise(resolve => setTimeout(resolve, ms));
        }
        await sleep(2000);
        // FOR TESTING END
        // const orderResponse = await OrderAPI.post(orderCreate)
        // const p = orderResponse.isSuccessful;
        getModule(OrderModule, store).setLoading(false);
        console.log(orderCreate)
        const p = false;
        if (p) this.clearOrders();
        else console.log('PIZDETS');
        return p;
    }
}
