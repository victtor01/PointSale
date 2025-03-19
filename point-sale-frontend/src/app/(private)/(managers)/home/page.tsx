"use client";

import { CenterSection } from "@/components/center-section";
import { dashboards } from "./dashboards";
import { ListOfPositions } from "./positions";
import { useEmployee } from "@/hooks/use-employee";
import { useProducts } from "@/hooks/use-products";

export default function Home() {
  const { useGetAllEmployees } = useEmployee();
  const { useGetAllProducts } = useProducts();

  const { employees } = useGetAllEmployees();
  const { products } = useGetAllProducts();

  return (
    <CenterSection className="p-0 px-4 mt-4 pb-[20rem] z-10">
      <section className="w-full flex gap-2 items-center">
        <dashboards.employees quantityOf={employees?.length} />
        <dashboards.products quantityOf={products?.length} />
      </section>

      <section className="mt-5">
        <ListOfPositions />
      </section>
    </CenterSection>
  );
}
