"use client";

import { pages } from "@/utils/pages";
import Link from "next/link";
import { usePathname } from "next/navigation";

export function BottomSidebar() {
  const pathName = usePathname();

  return (
    <div className="fixed bottom-0 left-0 w-full bg-gray-100 lg:hidden z-40 flex justify-between">
      <div className="flex w-full max-w-[50rem] px-7 mx-auto justify-between pb-2">
        {pages?.map((page) => {
          const Icon = page.icon;
          const selected = page.link === pathName;
          const selectedStyle = selected
            ? "translate-y-[-0.6rem] bg-gray-700 text-white shadow-xl shadow-gray-500"
            : "";

          return (
            <Link
              href={page.link}
              key={page.link}
              className="items-center flex flex-col"
            >
              <div
                className={`w-10 h-10 grid place-items-center rounded-full transition-all ${selectedStyle}`}
              >
                <Icon size={selected ? 16 : 20} />
              </div>
              <span className="text-xs opacity-60">{page.name}</span>
            </Link>
          );
        })}
      </div>
    </div>
  );
}
