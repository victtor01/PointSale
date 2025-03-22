import { IProduct } from "@/interfaces/IProduct";
import { queryClient } from "@/providers/query-client-provider";
import { ProductsPropsSchema } from "@/schemas/create-product-schema";
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

  const useCreateProduct = async (data: ProductsPropsSchema) => {
    try {
      const response = await api.post("/products", data);
      if (response?.data?.id) {
        queryClient.setQueriesData(
          { queryKey: ["products"] },
          (data: IProduct[]) => [...data, response.data]
        );
      } else {
        await queryClient.invalidateQueries({ queryKey: ["products"] });
      }
    } catch (error: unknown) {
      console.log(error);
    }
  };

  const useGetById = (productId: string) => {
    const { data: product, isLoading } = useQuery<IProduct>({
      queryKey: ["products", productId],
      queryFn: async () => (await api.get(`/products/${productId}`))?.data,
    });

    return {
      product,
      isLoading,
    };
  };

  const useUpdate = async (productId: string, data: ProductsPropsSchema) => {
    try {
      const res = await api.put(`/products/${productId}`, data);
      console.log(res);
    } catch (error: unknown) {
      console.log(error);
    }
  };

  return {
    useGetAllProducts,
    useCreateProduct,
    useGetById,
    useUpdate,
  };
}
