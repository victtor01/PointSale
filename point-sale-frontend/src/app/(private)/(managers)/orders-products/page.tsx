"use client";

import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import {
  IOrderProduct
} from "@/interfaces/IOrderProducts";
import { IProduct } from "@/interfaces/IProduct";
import { api } from "@/utils/api";
import { getColorByStatus } from "@/utils/orders-products-utils";
import { useQuery } from "@tanstack/react-query";

type ResponseGetAllProducts = {
  orders: IOrderProduct[];
};

const useOrdersProducts = () => {
  const useGetAllProducts = () => {
    const { data: ordersProducts } = useQuery<ResponseGetAllProducts>({
      queryKey: ["orders-products"],
      queryFn: async () => (await api.get("/orders-products")).data,
    });

    const orders = ordersProducts?.orders || [];

    return {
      orders,
    };
  };

  return {
    getAllProducts: useGetAllProducts,
  };
};

function Product({ product }: { product: IProduct }) {
  return (
    <div className="flex flex-1 justify-between px-2">
      <div className={`${fontSaira} font-semibold text-gray-600 text-sm`}>
        {product.name}
      </div>
    </div>
  );
}

function Table() {
  return (
    <div className="flex">
      <div className="w-8 h-8 bg-gray-900 border-2 border-gray-600 grid place-items-center text-white rounded-md">
        12
      </div>
    </div>
  );
}

function OrderProduct({ order }: { order: IOrderProduct }) {
  const color = getColorByStatus(order.status);

  return (
    <div className="flex flex-1">
      <div
        style={{ color, borderColor: color }}
        className={`p-1 px-2 text-xs rounded-md opacity-60 border`}
      >
        {order?.status}
      </div>
    </div>
  );
}

export default function OrderProducts() {
  const { getAllProducts } = useOrdersProducts();
  const { orders } = getAllProducts();

  return (
    <CenterSection className="w-full px-0 pt-0 flex flex-col bg-white shadow rounded-b-2xl">
      <header className="p-2 border-b font-semibold text-gray-500 text-sm">
        <h1 className={fontSaira}>Pedidos</h1>
      </header>

      <div className="flex flex-col">
        {orders?.map((orderProduct: IOrderProduct) => {
          const { product } = orderProduct;
          if (product) {
            return (
              <div
                key={orderProduct.id}
                className="flex items-center border-b w-full p-2 hover:bg-gray-50 justify-between"
              >
                <Product product={product} />
                <OrderProduct order={orderProduct} />
                <Table />
              </div>
            );
          }
        })}
      </div>
    </CenterSection>
  );
}
