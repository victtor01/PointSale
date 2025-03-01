"use client";

import { OrderComponent } from "@/components/order-component";
import { fontSaira } from "@/fonts";
import { useOrders } from "@/hooks/use-orders";
import { IOrder } from "@/interfaces/IOrder";
import Link from "next/link";
import { BsPlus } from "react-icons/bs";
import { IoMdAlert } from "react-icons/io";
import { SimpleDashboard } from "./dashboard";

export default function Orders() {
  const { getAllOrders } = useOrders();
  const { orders } = getAllOrders();

  return (
    <>
      <header className="flex h-auto mt-4 border border-gray-300 rounded-lg bg-white  relative justify-between items-center p-2 text-gray-500 font-semibold z-20">
        <div className="absolute pointer-events-none top-0 w-[30%] h-[5rem] bg-indigo-600 rounded-none left-[10%] blur-[50px] opacity-20"></div>
        <h1 className={`${fontSaira} text-lg`}>Meus Pedidos</h1>
        <div className="relative group/link">
          <Link
            href="/orders/create"
            className="bg-white dark:bg-gray-700 border p-1 px-2 text-gray-500 text-md rounded-md flex gap-2 items-center"
          >
            <BsPlus size={20} />
            Pedido
          </Link>
          <div className="right-0 bg-white items-center gap-2 hidden group-hover/link:flex border rounded px-2 w-[14rem] p-1 h-xl absolute mt-1 z-30 text-sm">
            <IoMdAlert size={20} />
            Para criar uma nova ordem, é necessário ter mesas disponíveis
          </div>
        </div>
      </header>

      <SimpleDashboard />

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
    </>
  );
}
