import { IconType } from "react-icons";
import { BiSolidFoodMenu } from "react-icons/bi";
import { GoHomeFill } from "react-icons/go";
import { HiRectangleStack } from "react-icons/hi2";
import { TbLayoutDashboardFilled } from "react-icons/tb";

export type Page = {
  name: string;
  icon: IconType;
  link: string;
};

export const pages = [
  { name: "Minhas mesas", icon: GoHomeFill, link: "/tables" },
  { name: "Dashboard", icon: TbLayoutDashboardFilled, link: "/home" },
  { name: "Orders", icon: BiSolidFoodMenu, link: "/orders" },
  { name: "Orders-Products", icon:  HiRectangleStack, link: "/orders-products"}
] satisfies Page[];