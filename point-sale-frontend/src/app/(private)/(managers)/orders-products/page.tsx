"use client";

import { CenterSection } from "@/components/center-section";
import { OrderComponent } from "@/components/order-component";
import { fontSaira } from "@/fonts";
import { IOrderProduct } from "@/interfaces/IOrderProducts";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

type ResponseGetAllProducts = {
  orders: IOrderProduct[];
};

const useOrdersProducts = () => {
  const getAllProducts = () => {
    const { data: ordersProducts } = useQuery<ResponseGetAllProducts>({
      queryKey: ["orders-products"],
      queryFn: async () => (await api.get("/orders-products")).data,
    });

    const orders = ordersProducts?.orders || [];

    return {
      orders,
    };
  };

  return {
    getAllProducts,
  };
};


export default function OrderProducts() {
  const { getAllProducts } = useOrdersProducts();
  const { orders } = getAllProducts();

  return (
    <CenterSection className="w-full px-0 pt-0 flex flex-col bg-white shadow rounded-b-2xl">
      <header className="p-2 border-b font-semibold text-gray-500 text-xl">
        <h1 className={fontSaira}>Pedidos</h1>
      </header>

      <div className=" p-3">
        {orders?.map((order: IOrderProduct) => {
          return (
              <div key={order.id}>
                teste
              </div>
          );
        })}
      </div>
    </CenterSection>
  );
}
