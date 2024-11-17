import { fontFiraCode, fontOpenSans } from "@/fonts";
import Link from "next/link";
import { FaLock } from "react-icons/fa";
import { MdEmail } from "react-icons/md";

export default function Login() {
  return (
    <div
      className={`${fontOpenSans} z-20 flex flex-col gap-2 m-auto w-full max-w-md bg-white dark:bg-zinc-950 rounded-xl border dark:border-neutral-700/30 p-8`}
    >
      <header className={`${fontFiraCode} grid gap-2`}>
        <h1 className="text-xl">Faça o login!</h1>
      </header>

      <section className="grid gap-4 mt-4">
        <label htmlFor="email" className="grid gap-2">
          <span className="opacity-60">Email</span>
          <div className="flex gap-2 border dark:border-neutral-800/70 w-full items-center p-3 rounded-md focus-within:ring-2 focus-within:ring-violet-600">
            <MdEmail className="opacity-30" size={18} />
            <input
              type="email"
              id="email"
              autoComplete="off"
              placeholder="jonhDoe@gmail.com"
              className="bg-transparent rounded outline-none w-full"
            />
          </div>
        </label>

        <label htmlFor="password" className="grid gap-2">
          <span className="opacity-60">Password</span>
          <div className="flex gap-2 border dark:border-neutral-800/70 w-full items-center p-3 rounded-md focus-within:ring-2 focus-within:ring-violet-600">
            <FaLock className="opacity-30" size={16} />
            <input
              type="password"
              id="password"
              autoComplete="off"
              placeholder="jonhDoe@gmail.com"
              className="bg-transparent rounded outline-none w-full"
            />
          </div>
        </label>
        <Link href="" className="flex opacity-80 hover:opacity-100">
          Esqueceu a senha?
        </Link>
      </section>

      <footer className="flex w-full mt-4">
        <button className="bg-violet-500 w-full rounded dark:bg-violet-600 text-white opacity-90 hover:opacity-100 p-2">
          Entrar
        </button>
      </footer>
    </div>
  );
}
