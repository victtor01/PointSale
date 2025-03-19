import { IProduct } from "@/interfaces/IProduct";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

export function useProducts() {
  const useGetAllProducts = () => {
    const { data: products, isLoading } = useQuery<IProduct[]>({
      queryKey: ["products"],
      queryFn: async () => (await api.get("/products"))?.data,
    });

    return {
      products,
      isLoading,
    };
  };

  return {
    useGetAllProducts,
  };
}
