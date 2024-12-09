"use client"

import { fontOpenSans, fontSaira } from "@/fonts";
import { useLogin } from "@/hooks/login/use-login";
import { FaGoogle, FaLock } from "react-icons/fa";
import { MdEmail } from "react-icons/md";

export default function Login() {
  const { submitLogin } = useLogin();

  return (
    <div
      className={`${fontOpenSans} z-20 flex flex-col gap-2 m-auto w-full max-w-md bg-white dark:bg-zinc-950 rounded-xl border dark:border-neutral-700/30 p-8`}
    >
      <header className={`${fontSaira} grid gap-2`}>
        <h1 className="text-[1.4rem] text-gray-500 dark:text-white font-semibold text-center">
          Entre com o google ou faça login para continuar!
        </h1>
      </header>

      <section className="flex gap-2 w-full mt-1">
        <button className="border text-gray-600 rounded-md p-3 px-4 flex items-center gap-2 w-full opacity-80 hover:opacity-100">
          <FaGoogle size={20} />
          <span>Entrar com o Google</span>
        </button>
      </section>

      <div className="flex w-full items-center justify-center">
        <div className="w-full h-[1px] bg-gray-300"></div>
        <span className="text-gray-400 px-4">ou</span>
        <div className="w-full h-[1px] bg-gray-300"></div>
      </div>

      <section className="grid gap-1">
        <label htmlFor="email" className="grid gap-2">
          <span className="opacity-60">Email</span>
          <div className="flex gap-2 border dark:border-neutral-800/70 w-full pl-3 items-center rounded-md">
            <MdEmail className="opacity-30" size={18} />
            <input
              type="email"
              id="email"
              autoComplete="off"
              placeholder="jonhDoe@gmail.com"
              className={`bg-transparent rounded outline-none w-full py-3 text-lg text-gray-600 dark:text-gray-200 ${fontSaira}`}
            />
          </div>
        </label>

        <label htmlFor="password" className="grid gap-2">
          <span className="opacity-60">Password</span>
          <div className="flex gap-2 border dark:border-neutral-800/70 pl-3 w-full items-center rounded-md">
            <FaLock className="opacity-30" size={16} />
            <input
              type="password"
              id="password"
              autoComplete="off"
              placeholder="jonhDoe@gmail.com"
              className={`bg-transparent rounded outline-none w-full py-3 text-lg text-gray-600 dark:text-gray-200 ${fontSaira}`}
            />
          </div>
        </label>

        <div className="flex items-center justify-start gap-2 mt-1">
          <button className="w-5 h-5 border rounded border-gray-300" />
          <span className="text-gray-600 dark:text-gray-200 text-sm">
            Mostrar a senha
          </span>
        </div>
      </section>

      <footer className="flex w-full mt-4">
        <button
        type="button"
          onClick={submitLogin}
          className="bg-indigo-600 w-full transition-opacity duration-300 rounded dark:bg-violet-600 text-white opacity-90 hover:opacity-100 p-2 py-3"
        >
          Entrar
        </button>
      </footer>
    </div>
  );
}
