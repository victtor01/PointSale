"use client";

import { OrderComponent } from "@/components/order-component";
import { useOrders } from "@/hooks/use-orders";
import { IOrder } from "@/interfaces/IOrder";


export default function Orders() {
  const { getAllOrders } = useOrders();
  const { orders } = getAllOrders();

  return (
    <section className="flex w-full flex-col ">
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
