import { IconType } from "react-icons";
import { BiSolidFoodMenu } from "react-icons/bi";
import { BsFillBoxFill } from "react-icons/bs";
import { FaRocket } from "react-icons/fa";
import { GoHomeFill } from "react-icons/go";
import { HiUsers } from "react-icons/hi2";
import { MdTableBar } from "react-icons/md";

export type Page = {
  name: string;
  icon: IconType;
  link: string;
};

export const pages = [
  { name: "Dashboard", icon: GoHomeFill, link: "/home" },
  { name: "Mesas", icon: MdTableBar, link: "/tables" },
  { name: "Ordens", icon: BiSolidFoodMenu, link: "/orders" },
  { name: "Pedidos", icon: FaRocket, link: "/queue" },
  { name: "Produtos", icon: BsFillBoxFill, link: "/products" },
  { name: "Funcionários", icon: HiUsers, link: "##" },
] satisfies Page[];
