"use client";

import { CenterSection } from "@/components/center-section";
import { fontOpenSans, fontSaira } from "@/fonts";
import Link from "next/link";
import { FaUsers } from "react-icons/fa";
import { ListOfPositions } from "./positions";

const DashboardEmployee = () => {
  return (
    <Link
      href="/employee"
      className="bg-white hover:shadow-xl flex flex-col shadow rounded-xl w-full max-w-[25rem]"
    >
      <header className="w-full flex items-center p-2 justify-between gap-2 border-b">
        <div className="flex items-center gap-2">
          <FaUsers />
          <h1 className={`${fontSaira} font-semibold text-md text-gray-600`}>
            Funcionários
          </h1>
        </div>
      </header>

      <section className="mt-3 flex flex-col px-3 pb-2">
        <div className="flex items-end gap-2 font-semibold justify-between">
          <h1 className={`${fontSaira} font-semibold text-3xl`}>12</h1>
          <p className={`${fontOpenSans} mb-1`}>Ativos</p>
        </div>
        <div className="flex items-end gap-2 font-semibold -mt-1 justify-between text-rose-600">
          <h1 className={`${fontSaira} font-semibold text-2xl`}>2</h1>
          <p className={`${fontOpenSans} mb-1 text-sm`}>Dispensados</p>
        </div>
      </section>
    </Link>
  );
};

export default function Home() {
  return (
    <CenterSection className="p-0 px-4 mt-4 pb-[20rem] z-10">
      <section className="w-full flex gap-2 items-center">
        <DashboardEmployee />
      </section>

      <section className="mt-5">
        <ListOfPositions />
      </section>
    </CenterSection>
  );
}
