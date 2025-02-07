"use client";

import { fontOpenSans, fontSaira } from "@/fonts";
import { useFormLogin, useLogin } from "@/hooks/login/use-login";
import { FaGoogle, FaLock } from "react-icons/fa";
import { IoIosAlert } from "react-icons/io";
import { MdEmail } from "react-icons/md";

export default function Login() {
  const { submitLogin } = useLogin();
  const { form } = useFormLogin();
  const { handleSubmit, formState, register } = form;
  const { errors } = formState;

  return (
    <div
      className={`${fontOpenSans} z-20 flex flex-col gap-2 m-auto w-full max-w-md bg-white dark:bg-zinc-950 rounded-3xl shadow dark:shadow-black p-8`}
    >
      <header className={`${fontSaira} grid gap-2`}>
        <h1 className="text-[1.4rem] text-indigo-600 dark:text-white font-semibold text-center">
          Entre com o google ou faça login para continuar!
        </h1>
      </header>

      <section className="flex gap-2 w-full mt-10">
        <button className="border text-gray-600 bg-gray-50 rounded-md p-3 px-4 flex items-center gap-2 w-full opacity-80 hover:opacity-100">
          <FaGoogle size={20} />
          <span>Entrar com o Google</span>
        </button>
      </section>

      <div className="flex w-full items-center justify-center">
        <div className="w-full h-[1px] bg-gray-300"></div>
        <span className="text-gray-400 px-4">ou</span>
        <div className="w-full h-[1px] bg-gray-300"></div>
      </div>

      <form onSubmit={handleSubmit(submitLogin)} className="grid gap-1">
        <label htmlFor="email" className="grid gap-2">
          <span className="opacity-60">Email</span>
          <div className="flex border dark:border-neutral-800/70 w-full items-center rounded-md">
            <MdEmail className="opacity-30 w-12" size={18} />
            <input
              type="email"
              id="email"
              autoComplete="off"
              {...register("email")}
              placeholder="jonhDoe@gmail.com"
              className={`bg-transparent border-l px-2 outline-none flex-1 py-2 text-lg text-gray-600 dark:text-gray-200 ${fontOpenSans}`}
            />
          </div>

          <span
            data-visible={!!errors?.email?.message}
            className={`${fontSaira} data-[visible=true]:flex items-center opacity-90 text-sm font-semibold text-rose-600 gap-2 hidden`}
          >
            <IoIosAlert size={20}/>
            {errors?.email?.message}
          </span>
        </label>

        <label htmlFor="password" className="grid gap-2 mt-2">
          <span className="opacity-60">Password</span>
          <div className="flex border dark:border-neutral-800/70 w-full items-center rounded-md">
            <FaLock className="opacity-30 w-12" size={16} />
            <input
              type="password"
              {...register("password")}
              id="password"
              autoComplete="off"
              placeholder="••••••••••"
              className={`bg-transparent border-l px-2 outline-none flex-1 py-2 text-lg text-gray-600 dark:text-gray-200 ${fontOpenSans}`}
            />
          </div>

          <span
            data-visible={!!errors?.password?.message}
            className={`${fontSaira} data-[visible=true]:flex opacity-90 text-sm font-semibold text-rose-600 items-center gap-2 hidden`}
          >
            <IoIosAlert size={20}/>
            {errors?.password?.message}
          </span>
        </label>

        <div className="flex items-center justify-start gap-2 mt-1">
          <button
            type="button"
            className="w-5 h-5 border rounded border-gray-300"
          />
          <span className="text-gray-600 dark:text-gray-200 text-sm">
            Mostrar a senha
          </span>
        </div>

        <footer className="flex w-full mt-4">
          <button
            type="submit"
            className="bg-indigo-600 w-full transition-opacity duration-300 rounded dark:bg-violet-600 text-white opacity-90 hover:opacity-100 p-2 py-3"
          >
            Entrar
          </button>
        </footer>
      </form>
    </div>
  );
}
