"use client";

import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { createTable } from "@/hooks/tables";
import { zodResolver } from "@hookform/resolvers/zod";
import { AnimatePresence, motion, MotionProps } from "framer-motion";
import Link from "next/link";
import { useRouter, useSearchParams } from "next/navigation";
import { useForm } from "react-hook-form";
import { BsPlus } from "react-icons/bs";
import { FaPlay } from "react-icons/fa";
import { z } from "zod";
import { AllTables } from "./all-tables";
import { BiSearch } from "react-icons/bi";

const animationBase = {
  initial: { opacity: 0, scale: 0.6 },
  animate: { opacity: 1, scale: 1 },
  exit: { opacity: 0, scale: 0.6 },
} satisfies MotionProps;

const createTableSchema = z.object({
  number: z.string().min(1),
});

type CreateTableSchemaProps = z.infer<typeof createTableSchema>;

const useFormTable = () => {
  const form = useForm<CreateTableSchemaProps>({
    resolver: zodResolver(createTableSchema),
  });

  return {
    form,
  };
};

function ModalCreateTable() {
  const { form } = useFormTable();
  const router = useRouter();

  const handleCreateTable = async (props: CreateTableSchemaProps) => {
    await createTable(props);
    router.push("?");
  };

  return (
    <motion.form
      onSubmit={form.handleSubmit(handleCreateTable)}
      variants={animationBase}
      initial="initial"
      animate="animate"
      exit="exit"
      className="flex rounded-2xl top-[100%] right-0 max-w-[20rem] bg-white shadow-xl p-8 w-full ml-auto mt-2 flex-col
      before:content-[''] before:w-1 before:h-[5rem] before:border-l-4 before:border-dotted before:absolute
      before:bottom-[100%] before:right-2 before:translate-y-[3rem] before:shadow-xl absolute"
    >
      <label htmlFor="number">
        <span className={`${fontSaira} font-semibold text-md`}>
          Número da mesa
        </span>
        <input
          type="number"
          max={1000}
          {...form.register("number")}
          className="flex w-full p-2 rounded outline-none bg-gray-100 font-semibold"
          placeholder="143"
        />
      </label>

      <footer className="flex mt-8 w-full justify-between">
        <button
          type="submit"
          className="bg-indigo-600 p-1 px-4 rounded-xl text-indigo-100 transition-all border-4 hover:border-indigo-400 border-transparent text-sm opacity-95 hover:opacity-100"
        >
          Criar
        </button>

        <Link
          href="?"
          className="bg-gray-200 flex items-center  p-1 px-3 rounded-xl text-sm opacity-80 hover:opacity-100"
        >
          Fechar
        </Link>
      </footer>
    </motion.form>
  );
}

function Tables() {
  const actionModalOption = useSearchParams();
  const action = actionModalOption.get("action") || null;

  return (
    <>
      <div className="w-full bg-gray-900 min-h-[8rem] relative overflow-hidden">
        <div className="grid-image" />

        <CenterSection>
          <span
            className={`${fontSaira} flex text-xl opacity-90 text-white font-semibold p-1 pt-4`}
          >
            José Victor
          </span>
        </CenterSection>
      </div>

      <CenterSection className="mt-[-4rem] px-4 relative">
        <header className="flex flex-col gap-4 relative w-full bg-white p-6 rounded-xl items-center border z-30 justify-between text-gray-600 dark:text-gray-200">
          <AnimatePresence>
            {action === "create" && <ModalCreateTable />}
          </AnimatePresence>
          <div className="flex w-full justify-between">
            <div className="font-semibold text-lg">
              <div className="flex gap-2 items-center drop-shadow-lg">
                <FaPlay size={10} />
                <h1 className={`text-lg font-semibold ${fontSaira}`}>Mesas</h1>
              </div>
            </div>
            <div className={`${fontSaira} relative`}>
              <Link
                href="?action=create"
                className="flex gap-2 text-gray-500 items-center border px-2 p-1 rounded font-semibold opacity-90 hover:opacity-100"
              >
                <BsPlus size={20} />
                Mesa
              </Link>
            </div>
          </div>
          <div className="flex w-full gap-2 items-center z-20">
            <input
              type="text"
              className="border p-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500 bg-transparent rounded-md w-full"
              placeholder="Pesquise aqui..."
            />

            <button className="px-6 p-2 bg-indigo-500 text-gray-200 rounded-md h-full">
              <BiSearch size={20} />
            </button>
          </div>
        </header>

        <AllTables key="5" />
      </CenterSection>
    </>
  );
}

export default Tables;
