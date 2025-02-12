"use client";

import { pages } from "@/utils/pages";
import Link from "next/link";
import { usePathname } from "next/navigation";

function BottomSidebar() {
  const LINK_MAIN = "/orders";
  const pathName = usePathname();

  return (
    <div className="fixed bottom-0 left-0 w-full bg-gray-100 lg:hidden z-40 flex justify-between">
      <div className="flex w-full max-w-[15rem] mx-auto justify-between pb-3">
        {pages?.map((page) => {
          const Icon = page.icon;
          const selected = page.link === pathName;
          const selectedStyle = selected
            ? "translate-y-[-0.6rem] bg-gray-700 text-white shadow-xl shadow-gray-500"
            : "";

          return (
            <Link
              href={page.link}
              key={page.name}
              className={`w-10 h-10 grid place-items-center rounded-full transition-all ${selectedStyle}`}
            >
              <Icon size={selected ? 16 : 20} />
            </Link>
          );
        })}
      </div>
    </div>
  );
}

export { BottomSidebar };
