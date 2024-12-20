"use client";

import { fontSaira } from "@/fonts";
import { usePathname, useRouter } from "next/navigation";
import { IconType } from "react-icons";
import { FaPlay } from "react-icons/fa";
import { TbLayoutDashboardFilled } from "react-icons/tb";
import { motion } from "framer-motion";
import { BiSolidFoodMenu } from "react-icons/bi";

type ButtonToLinkProps = {
  children: React.ReactNode;
  link: string;
};

type Page = {
  name: string;
  icon: IconType;
  link: string;
};

const pages = [
  { name: "Dashboard", icon: TbLayoutDashboardFilled, link: "/home" },
  { name: "Minhas mesas", icon: FaPlay, link: "/tables" },
  { name: "Produtos", icon: BiSolidFoodMenu, link: "#" },
] satisfies Page[];

function ButtonToLink({ children, link }: ButtonToLinkProps) {
  const currentLink = usePathname();
  const selected = !!currentLink?.startsWith(link);
  const router = useRouter();
  return (
    <motion.button
      data-selected={selected}
      transition={{ type: "spring" }}
      whileTap={{ scale: 0.8 }}
      onClick={() => router.push(link)}
      className="w-[2.5rem] h-[2.5rem] bg-gray-200 rounded-xl grid place-items-center opacity-90 hover:opacity-100 shadow-inner
      data-[selected=true]:bg-gray-800 data-[selected=true]:text-gray-100  data-[selected=true]:opacity-100 transition-colors group/button "
    >
      {children}
    </motion.button>
  );
}

function MinSidebar() {
  return (
    <div className="h-screen flex p-2">
      <motion.div className="w-auto overflow-auto bg-gray-100 p-3 flex flex-col gap-2 rounded-2xl shadow-lg shadow-gray-300 z-30">
        <header className="flex">
          <button
            className="w-[2.5rem] h-[2.5rem] bg-indigo-600 rounded-xl text-white hover:rounded-[100%] transition-all
          grid place-items-center opacity-80 hover:opacity-100"
          >
            <h1 className={fontSaira}>JV</h1>
          </button>
        </header>

        <section className="flex w-full flex-1 flex-col justify-center gap-2">
          {pages?.map(({ icon: Icon, link }, index) => (
            <ButtonToLink key={index} link={link}>
              <Icon />

              <div className="hidden group-hover:">

              </div>
            </ButtonToLink>
          ))}
        </section>
      </motion.div>
    </div>
  );
}

export { MinSidebar };
