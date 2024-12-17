"use client";

import { fontSaira } from "@/fonts";
import { IStore, useStore } from "@/hooks/select-store/use-store";
import { IoAddCircle } from "react-icons/io5";
import { MdLocalGroceryStore } from "react-icons/md";
import { useSelectStore } from "./hooks";

interface ButtonProps {
  children: React.ReactNode;
  storeId: string;
}

function Button({ children, storeId }: ButtonProps) {
  const { redirectToStore } = useSelectStore();

  return (
    <div className="flex flex-col w-28 flex-wrap text-wrap gap-2 items-center text-center">
      <button
        type="button"
        onClick={() => redirectToStore(storeId)}
        className="grid place-items-center opacity-90 hover:opacity-100 bg-white w-28 h-28 
        rounded-md shadow hover:shadow-lg transition-shadow border-2 border-transparent hover:border-gray-500"
      >
        <MdLocalGroceryStore size={25} />
      </button>

      {children}
    </div>
  );
}

function SelectStore() {
  const { stores, isLoading } = useStore();

  if (isLoading) {
    return (
      <div className="flex flex-col gap-2 mt-10">
        <div className="bg-indigo-600 text-gray-200 p-2 px-4 rounded">
          Carregando...
        </div>
      </div>
    );
  }

  if (!stores) {
    return (
      <div className="mt-10">
        <button className="p-2 bg-violet-600 text-white border border-violet-500 rounded opacity-90 hover:opacity-100 shadow-md">
          Crie sua primeira loja!
        </button>
      </div>
    );
  }

  return (
    <div className="bg-gradient-radial from-gray-100 to-white dark:bg-neutral-950 w-full h-screen overflow-auto flex">
      <div className="text-gray-700 dark:text-gray-200 flex flex-col m-auto items-center">
        <header className="text-2xl font-semibold">
          <h1 className={fontSaira}>
            Selecione a loja que você deseja continuar!
          </h1>
        </header>
        <section className="flex gap-4 mt-8 items-start">
          {stores?.map((store) => (
            <Button key={store.id} storeId={store.id}>
              <span className={`${fontSaira} opacity-80 font-semibold`}>
                {store?.name || "sem nome!"}
              </span>
            </Button>
          ))}

          <button className="w-28 h-28 opacity-90 hover:opacity-100 grid place-items-center rounded-lg bg-white border-2 border-dashed ">
            <IoAddCircle size={20} />
          </button>
        </section>
      </div>
    </div>
  );
}

export default SelectStore;
