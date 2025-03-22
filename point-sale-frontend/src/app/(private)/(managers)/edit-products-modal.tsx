"use client";

import { ReadonlyURLSearchParams } from "next/navigation";
import { motion } from "framer-motion";
import { IoClose } from "react-icons/io5";
import Link from "next/link";
import { useProducts } from "@/hooks/use-products";
import { CenterSection } from "@/components/center-section";
import { Controller, useForm } from "react-hook-form";
import { fontOpenSans, fontSaira } from "@/fonts";
import { CustomInputCurrency } from "@/components/input-salary";
import { RiSubtractFill } from "react-icons/ri";
import { BiPlus } from "react-icons/bi";
import {
  ProductsPropsSchema,
  productSchema,
} from "@/schemas/create-product-schema";
import { zodResolver } from "@hookform/resolvers/zod";
import { IProduct } from "@/interfaces/IProduct";
import { SimpleLoader } from "@/components/simple-loader";

type EditProductProps = {
  params: ReadonlyURLSearchParams;
};

type useEditProductProps = {
  product: IProduct;
};

const ErrorModal = () => (
  <div className="fixed top-0 left-0 z-50 bg-zinc-900 w-full h-screen overflow-auto grid place-items-center">
    <div className="bg-rose-600 px-4 text-rose-100 rounded-md py-6 border-2 border-rose-400 opacity-80 text-lg">
      Houve um erro ao tentar encontrar o item que você deseja editar, tente
      novamente!
    </div>

    <Link
      href="?"
      className="bg-zinc-600 px-3 p-2 rounded-md text-white flex items-center gap-2 text-lg"
    >
      <IoClose />
      <span>Close</span>
    </Link>
  </div>
);

const useEditProduct = ({ product }: useEditProductProps) => {
  const form = useForm<ProductsPropsSchema>({
    resolver: zodResolver(productSchema),
    defaultValues: {
      name: product.name,
      description: product?.description,
      price: product.price,
      quantity: Number(product?.quantity) || 0,
    },
  });

  return {
    form,
  };
};

const FormEditProduct = ({ product }: { product: IProduct }) => {
  const { form } = useEditProduct({ product });
  const { useUpdate } = useProducts();
  const { register, control, handleSubmit } = form;

  return (
    <motion.div
      transition={{ type: "keyframes", duration: 0.1 }}
      initial={{ opacity: 0, scale: 1 }}
      animate={{ opacity: 1, scale: 1 }}
      exit={{ opacity: 0, scale: 1 }}
      className="flex fixed top-0 left-0 flex-col text-gray-500 w-full h-screen bg-gray-100 z-50 overflow-y-auto"
    >
      <header className="w-full p-2 border-b flex bg-white">
        <CenterSection className="flex p-0 gap-2 flex-row items-center justify-between">
          <div className="flex p-2 font-semibold">
            <h1>
              Editar <b>{product?.name}</b>
            </h1>
          </div>

          <Link
            href="?"
            className="w-8 h-8 border rounded-md grid place-items-center border-current opacity-60 hover:opacity-100"
          >
            <IoClose />
          </Link>
        </CenterSection>
      </header>

      <CenterSection>
        <form onSubmit={handleSubmit((data) => useUpdate(product?.id, data))}>
          <section className="mt-5 w-full flex flex-col gap-4 bg-white p-4 rounded-md">
            <label htmlFor="name" className="flex flex-col w-full">
              <span className={`${fontOpenSans} text-md font-semibold`}>
                Nome do produto
              </span>

              <input
                type="text"
                id="name"
                autoComplete="off"
                {...register("name")}
                className="w-full p-2 bg-white border rounded-md outline-none transition-all duration-200"
                placeholder="X-Bacon com frango"
              />
            </label>

            <label htmlFor="price" className="flex flex-col">
              <span className={`${fontOpenSans} text-md font-semibold`}>
                Preço
              </span>

              <CustomInputCurrency
                {...register("price")}
                className="p-2 border"
                value={form?.watch("price")?.toString()}
                onChangeValue={(value) =>
                  form.setValue("price", Number(value || 0))
                }
              />
            </label>

            <label htmlFor="description" className="flex flex-col">
              <span className={`${fontOpenSans} text-md font-semibold`}>
                Descrição do produto
              </span>

              <textarea
                {...register("description")}
                className="outline-none bg-gray-200 rounded-md p-2 font-semibold text-lg transition-all duration-200"
              />
            </label>

            <label htmlFor="quantity" className="flex flex-col">
              <span className={`${fontOpenSans} text-md font-semibold`}>
                Quantidade
              </span>

              <Controller
                control={control}
                name="quantity"
                render={({ field }) => {
                  const value = field?.value || 0;
                  return (
                    <div className="flex gap-2 items-center">
                      <button
                        type="button"
                        className="w-8 h-8 bg-white border grid place-items-center rounded-md transition-all duration-200"
                        onClick={() => field.onChange(Math.max(0, value - 1))}
                      >
                        <RiSubtractFill size={20} />
                      </button>
                      <div className="font-semibold w-10 h-10 bg-gray-100 rounded-xl text-xl grid place-items-center">
                        {field.value}
                      </div>
                      <button
                        type="button"
                        className="w-8 h-8 bg-white border grid place-items-center rounded-md transition-all duration-200"
                        onClick={() => field.onChange(value + 1)}
                      >
                        <BiPlus size={20} />
                      </button>
                    </div>
                  );
                }}
              />
            </label>
          </section>

          <footer className="w-full mt-4">
            <button
              type="submit"
              className="px-3 py-2 rounded-md bg-gray-800 text-indigo-100"
            >
              <span className={fontSaira}>Salvar</span>
            </button>
          </footer>
        </form>
      </CenterSection>
    </motion.div>
  );
};

export const EditProduct = ({ params }: EditProductProps) => {
  const productId = params?.get("productId");
  const { useGetById } = useProducts();
  if (!productId) return <ErrorModal />;

  const { product, isLoading } = useGetById(productId);

  if (isLoading)
    return (
      <div className="w-full h-screen fixed top-0 left-0 grid place-items-center">
        <SimpleLoader />
      </div>
    );

  if (!product?.id) return <ErrorModal />;

  return <FormEditProduct product={product} />;
};
