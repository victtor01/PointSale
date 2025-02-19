import { IOrder } from "./IOrder";
import { IProduct } from "./IProduct";

export interface IOrderProduct {
  id: string;
  quantity: string;
  options: object[];
  productId: string;
  updatedAt: string;
  status: string;
  createdAt: string;
  product?: IProduct;
  order?: IOrder
}

export type OrderProductStatus = "READY" | "CANCELED" | "DELIVERED" | "IN_PROGRESS" | "PENDING"

export const OrderProductStatusColors: Record<string, string> = {
  READY: "#4f46e5", // indigo-600
  CANCELED: "#e11d48", // rose-600
  DELIVERED: "#16a34a", // emerald-500
  IN_PROGRESS: "#f97316", // orange-500
  PENDING: "#262626", // neutral
};