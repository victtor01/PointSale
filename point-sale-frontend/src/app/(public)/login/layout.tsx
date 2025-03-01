import { fontSaira } from "@/fonts";
import { FaArrowRight } from "react-icons/fa";

interface LayoutLoginProps {
  children: React.ReactNode;
}

export default function LayoutLogin({ children }: LayoutLoginProps) {
  return (
    <div className="flex w-full flex-col h-screen overflow-auto bg-gradient-radial from-white to-white dark:from-zinc-950 dark:to-black">
      <header className="w-full p-4 flex text-zinc-900 dark:text-white items-center z-20 justify-between">
        <div className={`${fontSaira} text-xl flex gap-2 items-center`}>
          <div className="flex items-center justify-center w-6 h-6 bg-indigo-500 rounded-full "></div>
          <h2 className="text-indigo-500 font-semibold">Organizze</h2>
        </div>
        <button className="flex items-center gap-2 text-gray-500 dark:text-gray-100">
          Crie sua conta!
          <FaArrowRight size={15} />
        </button>
      </header>

      {children}
    </div>
  );
}
