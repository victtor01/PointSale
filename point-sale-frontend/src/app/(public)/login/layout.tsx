import { fontFiraCode } from "@/fonts";
import { FaArrowRight } from "react-icons/fa";

export default function LayoutLogin({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="flex w-full flex-col h-screen overflow-auto bg-gradient-radial dark:from-zinc-950 dark:to-black">
      <div className="fixed top-0 left-0 w-full h-screen overflow-hidden">
        <div className="grid-image"></div>
      </div>

      <header className="w-full p-4 flex text-zinc-900 dark:text-white items-center border-b-2 dark:border-neutral-900 dark:bg-zinc-950 bg-white z-20 justify-between">
        <div className={`${fontFiraCode} text-xl`}>
          Organizze
        </div>
        <button className="border text-white bg-neutral-800 dark:border-zinc-500opacity-80 px-4 text-sm font-semibold hover:opacity-100 flex items-center gap-2 justify-center p-2 rounded">
          Crie sua conta!
          <FaArrowRight size={15} />
        </button>
      </header>

      {children}
    </div>
  );
}
