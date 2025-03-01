"use client";

import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import Link from "next/link";
import { BsPlus } from "react-icons/bs";
import { IoMdAlert } from "react-icons/io";
import { SimpleDashboard } from "./dashboard";
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
