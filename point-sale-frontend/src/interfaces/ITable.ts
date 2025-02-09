import { IOrder } from "./IOrder";

export interface ITable {
  id: string,
  number: string,
  orders: IOrder[];
}
