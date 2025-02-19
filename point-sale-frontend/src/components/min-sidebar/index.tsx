"use client";

import { fontSaira } from "@/fonts";
import { usePathname, useRouter } from "next/navigation";
import { motion } from "framer-motion";
import { pages } from "@/utils/pages";

type ButtonToLinkProps = {
  children: React.ReactNode;
  link: string;
};

function ButtonToLink({ children, link }: ButtonToLinkProps) {
  const currentLink = usePathname();
  const selected = currentLink === link;
  const router = useRouter();

  return (
    <button
      data-selected={selected}
      onClick={() => router.push(link)}
      className="w-[2.8rem] h-[2.8rem] bg-gray-200 relative rounded-lg grid place-items-center opacity-90 hover:opacity-100 shadow-inner
      data-[selected=true]:bg-gray-900 data-[selected=true]:text-gray-100 data-[selected=true]:opacity-100 group/button "
    >
      {children}

      <div
        data-selected={selected}
        className="flex absolute top-[50%] translate-y-[-50%] transition-all left-[-0.7rem] w-[0.6rem] h-0 rounded bg-gray-900
        data-[selected=true]:h-[2rem] opacity-0 data-[selected=true]:opacity-100"
      />
    </button>
  );
}

function MinSidebar() {
  return (
    <div className="h-screen lg:flex hidden">
      <motion.div className="w-auto overflow-auto bg-white p-3 border-r flex flex-col gap-2 shadow-gray-300 z-30 items-center">
        <header className="flex">
          <button
            className="w-[2.5rem] h-[2.5rem] bg-gray-900 rounded-full border-4 border-gray-800 text-white hover:rounded-[100%] transition-all
            grid place-items-center opacity-80 hover:opacity-100"
          >
            <h1 className={fontSaira}>JV</h1>
          </button>
        </header>

        <section className="flex w-full flex-1 flex-col justify-center gap-2">
          {pages?.map(({ icon: Icon, link }, index) => (
            <ButtonToLink key={index} link={link}>
              <Icon />
            </ButtonToLink>
          ))}
        </section>
      </motion.div>
    </div>
  );
}

export { MinSidebar };
