"use client";

import { fontSaira, fontValela } from "@/fonts";
import { IOrder } from "@/interfaces/IOrder";
import {
  IOrderProduct,
  OrderProductStatus,
  OrderProductStatusColors, 
} from "@/interfaces/IOrderProducts";
import { dayjs } from "@/utils/dayjs";
import { AnimatePresence, motion } from "framer-motion";
import { useState } from "react";
import { BiSolidDownArrow } from "react-icons/bi";
import { FaArrowRight } from "react-icons/fa";

type ORDER_STATUS = "CURRENT" | "PAID" | "CANCELLED";

type OrderStatusProps = {
  orderStatus: string;
  tableNumber: number | string;
  updatedAt: string;
  children: React.ReactNode;
};

const ORDER_STATUS_PT = {
  CURRENT: "em andamento",
  PAID: "pago",
  CANCELLED: "cancelado",
} as const;

const ORDER_STATUS_COLORS = {
  CURRENT: "bg-indigo-600",
  PAID: "bg-emerald-500",
  CANCELLED: "bg-rose-600",
} as const;

const Informations = (props: { order: IOrder }) => {
  const { order } = props;
  const { ordersProducts: orderProducts } = order;

  return (
    <motion.div
      initial={{ maxHeight: 0 }}
      animate={{ maxHeight: 300 }}
      exit={{ maxHeight: 0 }}
      key={props.order.id}
      layoutId={props.order.id}
      className="bg-white flex overflow-hidden"
    >
      <div className="flex flex-col h-full w-full">
        <section className="flex flex-col overflow-auto relative bg-white">
          {orderProducts?.map((orderProduct: IOrderProduct, index: number) => {
            const STATUS_PULSE: OrderProductStatus = "IN_PROGRESS";

            const colorStatus = OrderProductStatusColors[orderProduct.status];
            const { updatedAt } = orderProduct;

            return (
              <button
                key={index}
                className="flex hover:bg-white hover:shadow-lg hover:z-20 gap-2 pr-5 items-center first:border-t
                border-b p-1 bg-white opacity-90 hover:opacity-100 group/button relative last:border-b-0 flex-wrap text-nowrap"
              >
                <header className="w-10 h-10 bg-gray-100 border rounded-md"></header>
                <div className="p-1 flex-1 text-sm font-semibold px-2 bg-gray-100 rounded-md">
                  {orderProduct.product?.name}
                </div>

                <div className="flex-1 flex text-left text-sm font-semibold px-2  rounded-md">
                  <div className="text-gray-700 w-8 text-md h-8 rounded-md border-2 border-gray-600 grid place-items-center">
                    <span className={fontValela}>
                      {orderProduct?.quantity || 0}
                    </span>
                  </div>
                </div>

                <div className="p-1 flex-1 hidden md:flex">
                  {dayjs(updatedAt).fromNow()}
                </div>

                <div
                  className="flex items-center gap-2 flex-1 data-[pulse=true]:animate-pulse"
                  data-pulse={STATUS_PULSE === orderProduct.status}
                >
                  <div
                    style={{ backgroundColor: colorStatus }}
                    className={`p-1 px-2 text-gray-200 rounded-md text-[0.7rem] font-semibold shadow-gray-700`}
                  >
                    {orderProduct.status}
                  </div>
                  <div
                    style={{ backgroundColor: colorStatus }}
                    className={`p-2 rounded-full shadow-lg shadow-gray-700`}
                  />
                </div>

                <div className="flex absolute right-2">
                  <div className="text-white grid place-items-center group-hover/button:opacity-100 scale-0 group-hover/button:scale-100 transition-all opacity-0 w-[1.8rem] h-[1.8rem] rounded-xl bg-gray-800">
                    <FaArrowRight />
                  </div>
                </div>
              </button>
            );
          })}
        </section>
      </div>
    </motion.div>
  );
};

const OrderContainer = (props: OrderStatusProps) => {
  const [open, setOpen] = useState<boolean>(false);
  const handleOpen = () => setOpen((prev) => !prev);

  const { orderStatus, tableNumber, children, updatedAt } = props;

  const statusColor = ORDER_STATUS_COLORS[orderStatus as ORDER_STATUS];
  const statusLegend = ORDER_STATUS_PT[orderStatus as ORDER_STATUS];

  return (
    <div className="flex flex-col rounded-lg overflow-hidden bg-white border">
      <div className="flex p-3 items-center gap-2 z-20">
        <header className="flex flex-1 gap-4 items-center">
          <div className={`w-4 h-4 ${statusColor} rounded-full`} />
          <div className="w-[7rem] flex">
            <h1
              className={`${fontSaira} font-semibold text-gray-600 text-nowrap`}
            >
              {statusLegend}
            </h1>
          </div>

          <div className="flex-1 flex">
            <div className="p-1 bg-gray-700 border-4 border-gray-500 text-gray-200 px-3 rounded-xl">
              <h2 className={`${fontSaira} font-semibold text-xs `}>
                {tableNumber}
              </h2>
            </div>
          </div>

          <div className="p-1 px-2 bg-gray-100 rounded-md shadow font-semibold text-xs opacity-50">
            {dayjs(updatedAt).fromNow(false)}
          </div>
        </header>

        <button
          type="button"
          onClick={handleOpen}
          className="p-2 px-3 bg-gray-200 text-md rounded-md opacity-60"
        >
          <div data-open={open} className="data-[open=true]:rotate-180">
            <BiSolidDownArrow />
          </div>
        </button>
      </div>

      <AnimatePresence mode="wait">{open && children}</AnimatePresence>
    </div>
  );
};

const OrderComponent = {
  Container: OrderContainer,
  Informations: Informations,
} as const;

export { OrderComponent };
