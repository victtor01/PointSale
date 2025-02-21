"use client";

import { fontSaira } from "@/fonts";
import { useStore } from "@/hooks/select-store/use-store";
import { IStore } from "@/interfaces/IStore";
import { IoAddCircle } from "react-icons/io5";
import { useSelectStore } from "./hooks";
import { FaLock } from "react-icons/fa";
import { formatToBRL } from "@/utils/formatBRL";
import { motion } from "framer-motion";

const calculatePercentage = (valor: number, total: number): number =>
  total === 0 ? 0 : parseFloat(((valor / total) * 100).toFixed(2));

interface ButtonProps {
  children: React.ReactNode;
  store: IStore;
  loked: boolean;
}

function Button(props: ButtonProps) {
  const { children, store, loked } = props;
  const { redirectToStore } = useSelectStore();

  return (
    <motion.div initial={{ y: 20, opacity: 0.5 }} animate={{ y: 0, opacity: 1 }} className="flex flex-col  w-full flex-wrap text-wrap gap-2 items-center text-center">
      <button
        type="button"
        onClick={() => redirectToStore(store)}
        className="relative flex flex-col opacity-90 hover:opacity-100 bg-white w-full
        rounded-xl border transition-all p-5"
      >
        <header className="w-full flex">
          <h1 className={`${fontSaira} font-semibold text-gray-500`}>
            {store?.name}
          </h1>
        </header>

        {loked && (
          <div className="flex w-6 h-6 bg-gray-700 rounded-full items-center justify-center text-white absolute top-[-10px] left-[-10px]">
            <FaLock size={10} />
          </div>
        )}

        {children}
      </button>
    </motion.div>
  );
}

function SelectStore() {
  const { storesResponse, isLoading } = useStore();

  if (isLoading) {
    return (
      <div className="flex flex-col gap-2 w-full h-screen">
        <div className="bg-indigo-600 text-gray-200 p-2 px-4 m-auto rounded">
          Carregando...
        </div>
      </div>
    );
  }

  if (!storesResponse) {
    return (
      <div className="mt-10">
        <button className="p-2 bg-violet-600 text-white border border-violet-500 rounded opacity-90 hover:opacity-100 shadow-md">
          Crie sua primeira loja!
        </button>
      </div>
    );
  }

  return (
    <div className="bg-gradient-radial from-blue-50 to-white dark:bg-neutral-950 w-full h-screen overflow-auto flex">
      <div className="text-gray-700 dark:text-gray-200 flex flex-col items-center w-full">
        <header className="text-2xl font-semibold text-center w-full shadow-[inset_0px_-12px_50px_rgba(0,0,0,0.3)] items-center justify-center gap-5 flex bg-purple-600 p-[6rem] flex-col">
          <span className={`${fontSaira} text-white opacity-90`}>
            Organizze
          </span>
          <h1 className={`${fontSaira} text-white opacity-80`}>
            Selecione a loja que você deseja continuar!
          </h1>
        </header>
        <section className="flex flex-col gap-4 mt-8 justify-center w-full max-w-[30rem] flex-wrap px-5 mx-auto z-30">
          {storesResponse?.map(({ store, revenue }) => {
            const porcetage = store?.revenueGoal
              ? calculatePercentage(revenue, store?.revenueGoal)
              : 0;

            return (
              <Button loked={!!store?.password} key={store.id} store={store}>
                <div className="flex flex-col gap-2 w-full ">
                  <div className="flex items-center gap-3">
                    <div className="w-full bg-gray-200 min-h-4 flex-1 rounded-full relative overflow-hidden min-w-[10rem]">
                      <motion.div
                        initial={{ width: 0 }}
                        transition={{ type: "spring" }}
                        animate={{ width: `${porcetage}%` }}
                        className="absolute h-full w-10 bg-purple-600"
                      />
                    </div>
                    <span
                      className={`${fontSaira} opacity-60 font-semibold grid place-items-center rounded text-sm`}
                    >
                      {porcetage.toString()}%
                    </span>
                  </div>
                  <span className="flex gap-2 items-center text-sm font-semibold text-gray-400">
                    {formatToBRL(revenue)}
                    <b>-</b>
                    {formatToBRL(store?.revenueGoal ?? 0)}
                  </span>
                </div>
              </Button>
            );
          })}

          <button className="w-full p-4 grid opacity-90 hover:opacity-100 gap place-items-center rounded-lg bg-white border-2 border-dashed ">
            <IoAddCircle size={20} />
            <span className="font-semibold text-gray-500">Nova loja</span>
          </button>
        </section>
      </div>
    </div>
  );
}

export default SelectStore;
