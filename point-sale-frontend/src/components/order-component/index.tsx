"use client";

import { fontSaira } from "@/fonts";
import { IOrder } from "@/interfaces/IOrder";
import {
  IOrderProduct,
  OrderProductStatus,
  OrderProductStatusColors
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
  const { orderProducts } = order;

  return (
    <motion.div
      initial={{ maxHeight: 0 }}
      animate={{ maxHeight: 300 }}
      exit={{ maxHeight: 0 }}
      key={props.order.id}
      layoutId={props.order.id}
      className="bg-white border-b border-l border-r flex overflow-hidden"
    >
      <div className=" flex flex-col h-full w-full">
        <section className="flex flex-col overflow-auto relative bg-gradient-45 from-indigo-50 to-purple-50">
          {orderProducts?.map((orderProduct: IOrderProduct, index: number) => {
            const STATUS_PULSE: OrderProductStatus = "IN_PROGRESS";

            const colorStatus = OrderProductStatusColors[orderProduct.status];
            const { updatedAt } = orderProduct;
            return (
              <button
                key={index}
                className="flex hover:bg-white hover:shadow-lg hover:z-20 gap-2 pr-5 border-r items-center border-l 
                border-b p-1 bg-gray-50 opacity-90 hover:opacity-100 group/button relative"
              >
                <header className="w-10 h-10 bg-blue-100 rounded-md"></header>
                <div className="p-1 flex-[2]">{dayjs(updatedAt).fromNow()}</div>
                <div className="flex items-center gap-2 flex-1 border-l data-[pulse=true]:animate-pulse" data-pulse={STATUS_PULSE === orderProduct.status} >
                  <div
                  style={{ backgroundColor: colorStatus}}  
                    className={`p-1 px-2 text-gray-200 rounded-md text-[0.7rem] font-semibold shadow-gray-700`}
                  >
                    {orderProduct.status}
                  </div>
                  <div
                    style={{ backgroundColor: colorStatus}}  
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
    <div className="flex flex-col rounded-md">
      <div className="flex border bg-white p-3 items-center gap-2 z-20">
        <header className="flex flex-1 gap-4 items-center">
          <div className={`w-4 h-4 ${statusColor} rounded-full`} />
          <h1 className={`${fontSaira} font-semibold text-gray-600`}>
            {statusLegend}
          </h1>

          <div className="p-1 bg-gray-700 border-4 border-gray-500 text-gray-200 px-3 rounded-xl">
            <h2 className={`${fontSaira} font-semibold text-xs `}>
              {tableNumber}
            </h2>
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
