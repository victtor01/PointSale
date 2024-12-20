"use client";

import { fontSaira } from "@/fonts";
import { IStore } from "@/interfaces/IStore";
import { motion, Variants } from "framer-motion";
import Link from "next/link";
import { useLoginForm } from "./hooks";

interface LoginFormProps {
  store: IStore;
}

const animationOpacity = {
  initial: { opacity: 0 },
  animate: { opacity: 1 },
} satisfies Variants;

function LoginForm({ store: { id } }: LoginFormProps) {
  const { login, form } = useLoginForm({ storeId: id });
  const { handleSubmit, formState } = form;

  return (
    <motion.form
      onSubmit={handleSubmit(login)}
      variants={animationOpacity}
      initial="initial"
      animate="animate"
      className="flex flex-col gap-2 p-8 text-gray-600 w-full m-auto"
    >
      <header className="flex items-center gap-2 justify-between w-full">
        <h2 className={`font-semibold text-lg ${fontSaira}`}>
          Entre com a senha!
        </h2>
        <Link
          href="/select-store"
          className="flex p-1 font-semibold text-sm px-2 text-gray-500 bg-gray-200 rounded opacity-80 hover:opacity-100"
        >
          Back
        </Link>
      </header>

      <section className="flex flex-col gap-2 mt-4">
        <input
          {...form.register("password")}
          type="text"
          className="p-3 text-lg bg-gray-100 w-full text-gray-600 rounded border-opacity-70 placeholder:text-gray-300 font-semibold outline-none"
          placeholder="1234"
          maxLength={20}
        />

        <Link
          href="#"
          className="text-sm font-semibold text-indigo-600 opacity-80 hover:opacity-100"
        >
          Esqueci minha senha
        </Link>
      </section>

      <footer className="w-full mt-2">
        <button
          className={`${fontSaira} p-3 px-3 font-semibold rounded bg-indigo-600 transition-opacity w-full text-gray-200 opacity-90 hover:opacity-100`}
        >
          Entrar
        </button>
      </footer>
    </motion.form>
  );
}

export { LoginForm };
