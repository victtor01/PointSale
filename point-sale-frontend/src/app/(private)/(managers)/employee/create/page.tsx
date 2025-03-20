"use client";

import { CenterSection } from "@/components/center-section";
import { CustomInputCurrency } from "@/components/input-salary";
import { fontOpenSans, fontSaira } from "@/fonts";
import { useRouter } from "next/navigation";
import { FaArrowLeftLong, FaCheck } from "react-icons/fa6";

export default function Create() {
  const router = useRouter();

  return (
    <div className="py-5 w-full max-w-[50rem] mx-10">
      <header className="flex items-center gap-4 justify-between">
        <div className="flex items-center gap-4">
          <button
            onClick={() => router.back()}
            className="opacity-80 hover:opacity-100 w-9 h-9 bg-gray-200 grid place-items-center text-gray-500 rounded-full"
          >
            <FaArrowLeftLong />
          </button>

          <h1 className={`${fontOpenSans} text-lg text-gray-500 font-semibold`}>
            Criar novo funcionário
          </h1>
        </div>
      </header>

      <section className="flex flex-col text-gray-500 mt-4 gap-3">
        <div className="p-5 bg-white  rounded-xl border gap-4 flex flex-col">
          <label htmlFor="" className="flex flex-col flex-1">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Username
            </span>

            <div className="flex gap-4 items-center w-full">
              <input
                type="text"
                className="w-full p-2 bg-gray-100 rounded-md flex-1 shadow-inner outline-none"
                placeholder="nome do funcionário"
                value={123456}
              />
              <button className="p-1 px-3 bg-gray-700 rounded text-indigo-100 font-semibold">
                <span className={fontOpenSans}>Gerar</span>
              </button>
            </div>
          </label>

          <label htmlFor="" className="flex flex-col">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Primeiro nome *
            </span>

            <input
              type="text"
              className="w-full p-2 bg-white rounded-md border outline-none"
              placeholder="nome do funcionário"
            />
          </label>

          <label htmlFor="" className="flex flex-col">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Sobrenome
            </span>

            <input
              type="text"
              className="w-full p-2 bg-white rounded-md border outline-none"
              placeholder="nome do funcionário"
            />
          </label>
        </div>

        <div className="p-5 bg-white  rounded-lg border gap-4 flex flex-col">
          <label htmlFor="" className="flex flex-col">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Salário
            </span>

            <CustomInputCurrency
              value=""
              onChangeValue={() => null}
              className="border"
            />
          </label>

          <label htmlFor="" className="flex flex-col gap-2">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Cargos
            </span>
            <div className="flex flex-wrap">
              <button
                type="button"
                data-includes={true}
                onClick={() => null}
                className="items-center flex overflow-hidden bg-white rounded-md border
              data-[includes=true]:ring-2 data-[includes=true]:ring-indigo-500"
              >
                <div
                  data-includes={true}
                  className="w-8 h-full border-gray-400/30 border-r bg-white grid place-items-center
                data-[includes=true]:bg-indigo-600 text-white"
                >
                  {true && <FaCheck size={10} />}
                </div>
                <span
                  data-includes={true}
                  className="text-md font-semibold px-2 text-gray-500 p-1
                data-[includes=true]:bg-indigo-100"
                >
                  Manager
                </span>
              </button>
            </div>
          </label>
        </div>
      </section>

      <footer className="flex w-full mt-4">
        <button className="px-5 py-2 font-semibold bg-indigo-500 text-indigo-50 rounded-md opacity-90 hover:opacity-100">
          <span className={`${fontSaira}`}>Salvar</span>
        </button>
      </footer>
    </div>
  );
}
