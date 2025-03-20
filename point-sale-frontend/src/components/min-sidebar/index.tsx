"use client";

import { fontRoboto, fontSaira } from "@/fonts";
import { pages } from "@/utils/pages";
import { motion } from "framer-motion";
import { usePathname, useRouter } from "next/navigation";
import { IoMdSettings } from "react-icons/io";

type ButtonToLinkProps = {
  children: React.ReactNode;
  link: string;
};

function ButtonToLink({ children, link }: ButtonToLinkProps) {
  const currentLink = usePathname();
  const selected = currentLink.startsWith(link);
  const router = useRouter();

  return (
    <button
      data-selected={selected}
      onClick={() => router.push(link)}
      className="w-[3rem] h-[3rem] group relative rounded-2xl grid border-white place-items-center opacity-60 hover:opacity-100
      data-[selected=true]:shadow-xl data-[selected=true]:opacity-100 group/button border transition-all
      data-[selected=true]:bg-gray-800 data-[selected=true]:text-indigo-200 data-[selected=true]:border-transparent 
      data-[selected=true]:shadow-indigo-300 text-indigo-950
      data-[selected=true]:scale-[1.03] duration-[0.5s]
      data-[selected=true]:bg-gradient-radial from-indigo-700/40 to-transparent"
    >
      {children}
    </button>
  );
}

function MinSidebar() {
  return (
    <div className="lg:flex hidden flex-col bg-white rounded-xl z-10">
      <motion.div className="flex-1 w-auto overflow-visible rounded-xl to-transparent  p-0 px-3 flex flex-col gap-2 z-30 items-center">
        <header className="flex mt-1">
          <button
            className="w-[2.6rem] h-[2.6rem] rounded-xl text-gray-500 bg-indigo-100/50 shadow shadow-indigo-200/50 transition-all
            grid place-items-center opacity-90 hover:opacity-100 text-sm font-semibold bg-gradient-radial from-indigo-200/10 to-transparent"
          >
            <h1 className={fontRoboto}>J</h1>
          </button>
        </header>

        <section className="flex w-full flex-col justify-center gap-2 mt-4 flex-1">
          {pages?.map(({ icon: Icon, link, name }, index) => (
            <ButtonToLink key={index} link={link}>
              <Icon size={18} />
              <div
                className="border p-1 group-hover:opacity-100  opacity-0 pointer-events-none 
                flex-nowrap text-nowrap absolute left-[100%] ml-[1rem] transition-all bg-white rounded"
              >
                <span
                  className={`${fontSaira} text-gray-500 dark:text-gray-100`}
                >
                  {name}
                </span>
              </div>
            </ButtonToLink>
          ))}
        </section>
      </motion.div>

      <footer className="w-full flex items-center justify-center   pb-4">
        <button className="grid place-items-center  bg-gray-100 rounded-md w-10 h-10 group">
          <span className="group-hover:rotate-12 transition-all">
            <IoMdSettings size={20} />
          </span>
        </button>
      </footer>
    </div>
  );
}

export { MinSidebar };
