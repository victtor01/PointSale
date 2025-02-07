import { IconType } from "react-icons";
import { BiSolidFoodMenu } from "react-icons/bi";
import { FaPlay } from "react-icons/fa";
import { TbLayoutDashboardFilled } from "react-icons/tb";

export type Page = {
  name: string;
  icon: IconType;
  link: string;
};

export const pages = [
  { name: "Dashboard", icon: TbLayoutDashboardFilled, link: "/home" },
  { name: "Minhas mesas", icon: FaPlay, link: "/tables" },
  { name: "Produtos", icon: BiSolidFoodMenu, link: "/orders" },
] satisfies Page[];