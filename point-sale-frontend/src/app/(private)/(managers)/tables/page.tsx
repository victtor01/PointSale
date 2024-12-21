"use client";

import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { useState } from "react";
import { BsPlus } from "react-icons/bs";
import { FaArrowRight, FaPlay } from "react-icons/fa";


type TableProps = {
  table: {
    number: number;
    quantityOfOrders: number;
  };
};

function Table(props: TableProps) {
  const { table } = props;

  return (
    <div className="flex group relative items-center justify-between select-none gap-4 p-2 px-3 bg-white shadow transition-all rounded-lg opacity-90 hover:opacity-100">
      <div
        className={`pointer-events-none flex-1 flex flex-col font-semibold p-2 bg-gray-200/70 rounded-md ${fontSaira}`}
      >
        <span className="flex text-sm">Número da mesa</span>
        <span className={`text-3xl font-semibold ${fontSaira}`}>
          {table.number}
        </span>
      </div>

      <div
        className={`pointer-events-none flex-1 flex-col font-semibold ${fontSaira}`}
      >
        <span className="flex text-sm capitalize">pedidos hoje</span>
        <span
          className={`text-lg flex font-semibold text-white w-10 h-10 items-center justify-center bg-gradient-45 from-gray-900 to-gray-800 border-4 border-gray-300 rounded-xl ${fontSaira}`}
        >
          {table.quantityOfOrders}
        </span>
      </div>

      <div className="flex items-center absolute right-2 top-[50%] translate-y-[-50%]">
        <button
          className="flex w-9 items-center justify-center text-violet-200 h-9 translate-x-[2rem]
          rounded-xl bg-gray-800 to-gray-800 opacity-0 hover:opacity-100 group-hover:translate-x-[1.5rem]
          group-hover:opacity-100 transition-all z-20 "
        >
          <FaArrowRight />
        </button>
      </div>
    </div>
  );
}

function AllTables() {
  const [tables, setTables] = useState([
    { number: 32, quantityOfOrders: 12 },
    { number: 63, quantityOfOrders: 3 },
  ]);

  return (
    <section className="grid grid-cols-2 gap-5">
      {tables?.map((table) => (
        <Table key={table.number} table={table} />
      ))}
    </section>
  );
}

function Tables() {
  return (
    <CenterSection className="p-0">
      <header className="flex w-full py-4 px-5 rounded-b-xl bg-gray-100 justify-between text-gray-600 dark:text-gray-200">
        <div className="font-semibold text-lg">
          <div className="flex gap-2 items-center drop-shadow-lg">
            <FaPlay size={10} />
            <h1 className={`text-lg font-semibold ${fontSaira}`}>Mesas</h1>
          </div>
        </div>
        <div className={fontSaira}>
          <button className="text-md flex gap-1 items-center bg-white shadow px-2 p-1 font-semibold rounded-md opacity-90 hover:opacity-100">
            <BsPlus size={20} />
            Criar
          </button>
        </div>
      </header>

      <section
        className={`flex w-full gap-5 ${fontSaira} mt-5 items-center select-none`}
      >
        <div className="flex flex-col gap-1">
          <span className="font-semibold opacity-80">Modo de exibição</span>
          <select name="" id="">
            <option value=""></option>
          </select>
        </div>
      </section>

      <section
        className={`flex w-full gap-5 ${fontSaira} mt-5 items-center select-none`}
      >
        <div className="flex-1 flex gap-2 text-center">
          <span className="text-[2rem] text-gray-700 font-semibold text-shadow">
            15
          </span>
          <span className="text-md font-semibold self-end mb-2">
            Pedidos no total
          </span>
        </div>

        <div className="p-1 px-3 bg-gray-200 rounded-full font-semibold opacity-50">
          2/25
        </div>
      </section>

      <AllTables />
    </CenterSection>
  );
}

export default Tables;
