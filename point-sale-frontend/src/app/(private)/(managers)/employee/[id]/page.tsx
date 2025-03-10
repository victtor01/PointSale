"use client";

import { CenterSection } from "@/components/center-section";
import { CustomInputCurrency } from "@/components/input-salary";
import { fontRoboto, fontSaira } from "@/fonts";
import { useEmployee } from "@/hooks/use-employee";
import { usePositions } from "@/hooks/use-positions";
import { IEmployee, IPositionEmployee } from "@/interfaces/IEmployee";
import { useParams } from "next/navigation";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { BiSolidPhone } from "react-icons/bi";
import { FaCheck, FaHashtag, FaUser } from "react-icons/fa";
import { MdEmail } from "react-icons/md";

interface FormData {
  price: string;
  username: string;
}

interface ParamsOf extends Record<string, string> {
  id: string;
}

export const formatCurrency = (value: number | string) => {
  if (typeof value === "string") {
    value = parseFloat(value);
  }
  return value.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
};

const useDetailsEmployee = (employee?: IEmployee) => {
  const form = useForm<FormData>();

  const { reset } = form;

  useEffect(() => {
    console.log(employee);
    form.reset({
      price: employee?.salary || "",
      username: employee?.username || "",
    });
  }, [employee, reset]);

  return {
    form,
  };
};

export default function Details() {
  const { id } = useParams<ParamsOf>();
  const { getAllPositions } = usePositions();
  const { positions } = getAllPositions();
  const { findById } = useEmployee();
  const { employee } = findById(id);
  const { form } = useDetailsEmployee(employee);
  const { register, watch, setValue } = form;

  const positionsOfEmployee: string[] =
    employee?.positions?.map((p) => p.id) || [];

  return (
    <CenterSection className="mt-5">
      <header className="flex gap-2 justify-between items-center">
        <div className="flex items-center gap-2">
          <h1 className={`${fontRoboto} text-gray-600 rounded text-xl`}>
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
            {...register("username")}
            aria-label="teste"
            className="p-2 bg-gray-200 rounded-md cursor-not-allowed shadow-inner shadow-gray-300"
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

          <CustomInputCurrency
            value={watch("price")}
            onChangeValue={(value) => setValue("price", value || "0")}
          />
        </label>
      </section>

      <section className="flex flex-col gap-2 mt-10">
        <header className="text-lg flex text-gray-600 font-semibold justify-between">
          <h1 className={fontRoboto}>Cargos</h1>
          <div>
            <button className="p-1 px-3 font-semibold text-gray-500 bg-white border text-sm rounded-md">
              Novo cargo
            </button>
          </div>
        </header>

        <div className="flex gap-2">
          {positions?.map((position: IPositionEmployee, index: number) => {
            const includes: boolean = positionsOfEmployee.includes(position.id);
            return (
              <button
                key={index}
                data-includes={includes}
                className="items-center flex gap-2 overflow-hidden bg-white shadow rounded-md
                data-[includes=true]:border-2 data-[includes=true]:border-indigo-500"
              >
                <div
                  data-includes={includes}
                  className="w-8 h-full border-gray-400/30 border-r bg-white grid place-items-center
                  data-[includes=true]:bg-indigo-600 text-white"
                >
                  {includes && <FaCheck size={10} />}
                </div>
                <span className="text-md font-semibold pr-3 text-gray-500 p-1">
                  {position.name}
                </span>
              </button>
            );
          })}
        </div>
      </section>

      <footer className="flex items-center justify-between mt-4">
        <button className="px-3 p-1 bg-indigo-600 rounded-md text-white">
          <span className={fontSaira}>Salvar</span>
        </button>
      </footer>
    </CenterSection>
  );
}
