"use client";

import { CenterSection } from "@/components/center-section";
import { CustomInputCurrency } from "@/components/input-salary";
import { fontSaira } from "@/fonts";
import { useForm } from "react-hook-form";
import { BiPhone, BiSolidPhone } from "react-icons/bi";
import { FaHashtag, FaUser } from "react-icons/fa";
import { MdAttachMoney, MdEmail } from "react-icons/md";

interface FormData {
  price: string;
}

const useDetailsEmployee = () => {
  const form = useForm<FormData>();

  return {
    form,
  };
};

export default function Details() {
  const { form } = useDetailsEmployee();
  const { register } = form;

  return (
    <CenterSection className="mt-5">
      <header className="flex gap-2 justify-between items-center">
        <div className="flex items-center gap-2">
          <h1
            className={`${fontSaira} text-gray-700 rounded font-semibold text-xl`}
          >
            Detalhes
          </h1>
        </div>

        <div>
          <button className="border border-current text-red-500 text-sm p-1 px-3 rounded-md opacity-80 hover:opacity-100">
            Excluir
          </button>
        </div>
      </header>

      <section className="w-full flex flex-col mt-4 gap-4">
        <label htmlFor="" className="text-md font-semibold flex flex-col">
          <div className="flex gap-2 items-center">
            <FaHashtag />
            <span>Usuário</span>
          </div>

          <input
            type="text"
            aria-label="teste"
            className="p-2 bg-gray-200 rounded-md cursor-not-allowed shadow-inner shadow-gray-300"
            value={"124235"}
            disabled
          />
        </label>

        <label htmlFor="" className="text-md font-semibold flex flex-col">
          <div className="flex gap-2 items-center px-1">
            <FaUser size={14} />
            <span>Primerio nome *</span>
          </div>

          <input
            type="text"
            className="p-2 bg-white rounded-md shadow outline-none focus:ring-4 ring-indigo-500/40"
            placeholder="Jonh Doe"
          />
        </label>

        <div className="flex gap-4 w-full">
          <label
            htmlFor=""
            className="text-md font-semibold flex flex-col flex-1"
          >
            <div className="flex gap-2 items-center">
              <MdEmail />
              <span>Email</span>
            </div>

            <input
              type="email"
              className="p-2 bg-white rounded-md shadow outline-none focus:ring-4 ring-indigo-500/40"
              placeholder="JonhDoe@gmail.com"
            />
          </label>
          <label
            htmlFor=""
            className="text-md font-semibold flex flex-col flex-1"
          >
            <div className="flex gap-2 items-center">
              <BiSolidPhone />
              <span>Telefone</span>
            </div>

            <input
              type="text"
              className="p-2 bg-white rounded-md shadow outline-none focus:ring-4 ring-indigo-500/40"
              placeholder="12345678901"
            />
          </label>
        </div>

        <label
          htmlFor=""
          className="text-md font-semibold flex flex-col flex-1"
        >
          <div className="flex gap-2 items-center">
            <span>R$ Salário</span>
          </div>

          <CustomInputCurrency register={register("price")} />
        </label>
      </section>

      <footer className="flex items-center justify-between mt-4">
        <button className="px-3 p-1 bg-indigo-600 rounded-md text-white">
          <span className={fontSaira}>Salvar</span>
        </button>
      </footer>
    </CenterSection>
  );
}
