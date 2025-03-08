import { IOrderProduct } from "./IOrderProducts";
import { ITable } from "./ITable";

export interface IOrder {
  id: string;
  tableId: string;
  orderStatus: string | number;
  ordersProducts: IOrderProduct[];
  createdAt: string;
  updatedAt: string;
  table?: ITable | null;
}
