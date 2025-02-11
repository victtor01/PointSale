"use client";

import { OrderComponent } from "@/components/order-component";
import { IOrder } from "@/interfaces/IOrder";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

const useOrders = () => {
  const getAllOrders = () => {
    const { data: orders } = useQuery<IOrder[]>({
      queryKey: ["orders"],
      queryFn: async () => (await api.get("/orders")).data,
    });

    return {
      orders,
    };
  };

  return {
    getAllOrders,
  };
};

export default function Orders() {
  const { getAllOrders } = useOrders();
  const { orders } = getAllOrders();

  return (
    <section className="flex w-full flex-col mt-4">
      {orders?.map((order: IOrder) => {
        const { updatedAt, table, orderStatus } = order;
        return (
          <OrderComponent.Container
            orderStatus={orderStatus.toString()}
            tableNumber={table?.number || 0}
            updatedAt={updatedAt}
            key={order.id}
          >
            <OrderComponent.Informations order={order} />
          </OrderComponent.Container>
        );
      })}
    </section>
  );
}
