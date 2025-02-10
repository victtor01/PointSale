import { IOrderProduct } from "./IOrderProducts";
import { ITable } from "./ITable";

export interface IOrder {
  id: string;
  tableId: string;
  orderStatus: string | number;
  orderProducts: IOrderProduct[];
  createdAt: string;
  table?: ITable | null;
}
