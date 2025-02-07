import { fontSaira } from "@/fonts";
import { FaArrowRight } from "react-icons/fa";

interface LayoutLoginProps {
  children: React.ReactNode;
}

export default function LayoutLogin({ children }: LayoutLoginProps) {
  return (
    <div className="flex w-full flex-col h-screen overflow-auto bg-gradient-radial from-blue-50 to-white dark:from-zinc-950 dark:to-black">
      {/* 
        <div className="fixed top-0 left-0 w-full h-screen overflow-hidden">
          <div className="grid-image"></div>
        </div> 
      */}

      <header className="w-full p-4 flex text-zinc-900 dark:text-white items-center border-b dark:border-neutral-900 dark:bg-zinc-950 bg-white z-20 justify-between">
        <div className={`${fontSaira} text-xl flex gap-2 items-center`}>
          <div className="flex items-center justify-center w-6 h-6 bg-indigo-500 rounded-full "></div>
          <h2 className="text-indigo-500 font-semibold">Organizze</h2>
        </div>
        <button className=" shadow shadow-indigo-500/50 text-white bg-gradient-to-r from-indigo-500 to-blue-600 dark:border-zinc-500 opacity-80 px-4 text-sm font-semibold hover:opacity-100 flex items-center gap-2 justify-center p-2 rounded">
          Crie sua conta!
          <FaArrowRight size={15} />
        </button>
      </header>

      <div className="flex fixed top-[40%] right-[10%] rotate-12 bg-gradient-45 from-indigo-500 to-purple-600 w-[60vh] h-[60vh] rounded-[20%]">
      </div>

      <div className="flex fixed top-[-20%] left-[20%] rotate-[30deg] bg-gradient-45 from-purple-500 to-indigo-600 w-[60vh] h-[60vh] rounded-[20%]">
      </div>

      <section className="flex w-full h-screen overflow-auto px-5">

      {children}
      </section>
    </div>
  );
}
