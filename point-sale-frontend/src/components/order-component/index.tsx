import { fontRoboto, fontSaira } from "@/fonts";
import { IOrder } from "@/interfaces/IOrder";
import { useState } from "react";
import { BiSolidDownArrow } from "react-icons/bi";
import { AnimatePresence, motion, MotionProps } from "framer-motion";
import { IOrderProduct } from "@/interfaces/IOrderProducts";
import { dayjs } from "@/utils/dayjs";

type ORDER_STATUS = "CURRENT" | "PAID" | "CANCELLED" 

type OrderStatusProps = {
  orderStatus: string;
  tableNumber: number | string;
  createdAt: string;
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
  CANCELLED: "bg-rose-600"
} as const

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
      className="bg-white border-b border-l border-r"
    >
      <div className=" mt-[-1rem] p-4 pt-[2rem]">
        <header className="w-full">
          <h1 className={fontRoboto}>Informações sobre o pedido</h1>
        </header>

        <section>
          {orderProducts?.map((orderProduct: IOrderProduct, index: number) => {
            const { createdAt } = orderProduct;
            return (
              <div key={index}>
                <div>{dayjs(createdAt).format("YYYY-MM-DD:HH:mm")}</div>
              </div>
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

  const { orderStatus, tableNumber, children, createdAt } = props;

  const statusColor = ORDER_STATUS_COLORS[orderStatus as ORDER_STATUS]
  const statusLegend = ORDER_STATUS_PT[orderStatus as ORDER_STATUS]

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
            {dayjs(createdAt).fromNow(false)}
          </div>
        </header>

        <button
          type="button"
          onClick={handleOpen}
          className="p-2 px-3 bg-gray-200 text-md rounded-md opacity-60"
        >
          <BiSolidDownArrow />
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
