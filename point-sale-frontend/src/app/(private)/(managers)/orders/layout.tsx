"use client";

import { CenterSection } from "@/components/center-section";
interface LayoutOrdersProps {
  children: React.ReactNode;
}

export default function LayoutOrders(props: LayoutOrdersProps) {
  const { children } = props;
  return (
    <CenterSection className="w-full pt-0 flex flex-col relative px-5">
      {children}
    </CenterSection>
  );
}
