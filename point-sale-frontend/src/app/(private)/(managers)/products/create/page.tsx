"use client";

import { CenterSection } from "@/components/center-section";
import { DefaultLoader } from "@/components/default-loader";
import { CustomInputCurrency } from "@/components/input-salary";
import { fontOpenSans } from "@/fonts";
import { useProducts } from "@/hooks/use-products";
import {
  ProductsPropsSchema,
  productSchema,
} from "@/schemas/create-product-schema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";
import { Controller, useForm } from "react-hook-form";
import { BiPlus } from "react-icons/bi";
import { FaArrowLeftLong } from "react-icons/fa6";
import { IoMdAlert } from "react-icons/io";
import { RiSubtractFill } from "react-icons/ri";
import { toast } from "react-toastify";

const CreateProductUtils = () => {
  const form = useForm<ProductsPropsSchema>({
    resolver: zodResolver(productSchema),
    defaultValues: {
      quantity: 0,
    },
  });

  return {
    form,
  };
};

export default function CreateProduct() {
  const { form } = CreateProductUtils();
  const { useCreateProduct } = useProducts();
  const router = useRouter();

  const { register, control, handleSubmit, formState } = form;
  const { isSubmitting } = formState;

  return (
    <CenterSection className="p-6 mx-10 mt-10 flex w-full flex-col bg-white shadow-lg rounded-lg">
      <header className="flex items-center gap-4 justify-between mb-6">
        <div className="flex items-center gap-4">
          <button
            type="button"
            onClick={() => router.back()}
            className="opacity-80 hover:opacity-100 w-9 h-9 bg-gray-200 grid place-items-center text-gray-500 rounded-full transition-all duration-200"
          >
            <FaArrowLeftLong />
          </button>

          <h1 className={`${fontOpenSans} text-xl text-gray-700 font-semibold`}>
            Criar novo Produto
          </h1>
        </div>
      </header>

      {Object.keys(formState.errors).length > 0 && (
        <div className="flex mb-4">
          <div className="text-rose-600 p-3 px-5 bg-red-100 font-semibold rounded-md flex gap-2 items-center">
            <IoMdAlert />
            <div>{Object.values(formState.errors)[0]?.message}</div>
          </div>
        </div>
      )}

      <form
        className="flex flex-col overflow-hidden gap-4 w-full"
        onSubmit={handleSubmit((data) => {
          useCreateProduct(data);
          toast.success("Produto criado com sucesso!");
          router.push("/products");
        })}
      >
        <section className="flex flex-col gap-4 w-full">
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
              className="border"
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
              className="outline-none bg-gray-200 rounded-md p-3 font-semibold text-lg transition-all duration-200"
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

        <footer className="flex w-full mt-6">
          <button
            data-loading={isSubmitting}
            disabled={isSubmitting}
            type="submit"
            className="px-4 py-2 rounded flex opacity-90 hover:opacity-100 gap-2 items-center bg-gray-800 text-indigo-100 font-semibold transition-all duration-200
												data-[loading=true]:opacity-50"
          >
            {isSubmitting && <DefaultLoader />}
            <span>Criar produto</span>
          </button>
        </footer>
      </form>
    </CenterSection>
  );
}
