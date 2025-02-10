"use client"

import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

const useOrders = () => {
  const getAllOrders = () => {
    const { data: orders } = useQuery({
      queryKey: ["orders"],
      queryFn: async () => (await api.get("/orders")).data,
    });
    
    return {
      orders
    }
  }

  return {
    getAllOrders
  }
};

export default function Orders() {
  const { getAllOrders } = useOrders()
  const { orders } = getAllOrders();

  console.log(orders)

  return (
    <section>
      <header></header>
    </section>
  );
}
