"use client";

import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { BiPlus } from "react-icons/bi";
import { BsFillBoxFill } from "react-icons/bs";
import { Product } from "./product";
import { useProducts } from "@/hooks/use-products";
import { IProduct } from "@/interfaces/IProduct";
import Link from "next/link";

export default function Products() {
  const { useGetAllProducts } = useProducts();
  const { products } = useGetAllProducts();

  return (
    <>
      <header className="flex w-full flex-col h-auto mt-5">
        <CenterSection className="flex flex-col h-auto pb-2 px-3">
          <div className="flex w-full justify-between items-center">
            <h1 className={`${fontSaira} text-xl font-semibold text-gray-600`}>
              Meus produtos
            </h1>

            <div className="flex gap-2 items-center">
              <Link
                href="/products/create"
                className="p-2 px-3 flex gap-2 items-center text-indigo-50 bg-indigo-500/80 shadow-xl shadow-indigo-500/30 opacity-95 hover:opacity-100 rounded-md"
              >
                <BiPlus />
                <span>Produto</span>
              </Link>
            </div>
          </div>

          <div className="w-full flex mt-4">
            <div className="p-2 items-center shadow justify-between gap-3 bg-white text-gray-500 px-4 py-5 w-full max-w-[20rem] rounded-md flex">
              <div className="font-semibold flex items-center gap-2">
                <h1 className="text-[1.8rem] font-semibold">4</h1>
                <span className="mt-2">Produtos cadastrados</span>
              </div>
              <div className="w-10 h-10 grid place-items-center bg-gray-50 rounded-full text-gray-300 ">
                <BsFillBoxFill />
              </div>
            </div>
          </div>
        </CenterSection>
      </header>

      <CenterSection className="px-3 pb-0 mt-5">
        <section className="flex gap-2">
          <input
            type="text"
            className="flex-1 p-2 bg-white rounded-md border outline-none"
            placeholder="pesquise por algum produto..."
          />

          <button className="p-1 px-3 font-semibold border-4 border-indigo-200 text-indigo-50 bg-indigo-500/80 rounded-xl">
            <span className={fontSaira}>GO</span>
          </button>
        </section>
      </CenterSection>

      <CenterSection className="p-0 px-3 mt-4">
        <section className="w-full text-left rounded-xl bg-white border overflow-hidden">
          <table className="w-full">
            <thead className="bg-indigo-50/30 text-gray-500 font-normal">
              <tr className={`${fontSaira}`}>
                <th scope="col" className="px-2 py-3">
                  Nome
                </th>
                <th scope="col" className="px-2 py-3">
                  Descrição
                </th>
                <th scope="col" className="px-2 py-3">
                  Preço
                </th>
                <th scope="col" className="px-2 py-3">
                  Categorias
                </th>
                <th scope="col" className="px-2 py-3">
                  No cardápio
                </th>
                <th scope="col" className="px-2 py-3">
                  Ações
                </th>
              </tr>
            </thead>

            <tbody className="divide-y">
              {products?.map((product: IProduct) => {
                return (
                  <Product
                    key={product.id}
                    data={{
                      id: product.id,
                      name: product?.name,
                      description: product?.description,
                      price: product.price,
                      categories: [],
                    }}
                  />
                );
              })}
            </tbody>
          </table>
        </section>
      </CenterSection>
    </>
  );
}
