import { IOrder } from "@/interfaces/IOrder";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

const useOrders = () => {
  const getAllOrders = () => {
    const { data: orders } = useQuery<IOrder[]>({
      queryKey: ["orders"],
      queryFn: async () => (await api.get("/orders/managers")).data,
    });

    return {
      orders,
    };
  };

  return {
    getAllOrders,
  };
};

export { useOrders };
