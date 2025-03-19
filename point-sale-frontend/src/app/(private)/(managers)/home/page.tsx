"use client";

import { CenterSection } from "@/components/center-section";
import { dashboards } from "./dashboards";
import { ListOfPositions } from "./positions";

export default function Home() {
  return (
    <CenterSection className="p-0 px-4 mt-4 pb-[20rem] z-10">
      <section className="w-full flex gap-2 items-center">
        <dashboards.employees />
        <dashboards.products />
      </section>

      <section className="mt-5">
        <ListOfPositions />
      </section>
    </CenterSection>
  );
}
