import { IconType } from "react-icons";
import { BiSolidFoodMenu } from "react-icons/bi";
import { GoHomeFill } from "react-icons/go";
import { HiRectangleStack } from "react-icons/hi2";
import { MdTableBar } from "react-icons/md";

export type Page = {
  name: string;
  icon: IconType;
  link: string;
};

export const pages = [
  { name: "Dashboard", icon: GoHomeFill, link: "/home" },
  { name: "Minhas mesas", icon: MdTableBar, link: "/tables" },
  { name: "Orders", icon: BiSolidFoodMenu, link: "/orders" },
  { name: "Orders-Products", icon:  HiRectangleStack, link: "/orders-products"}
] satisfies Page[];