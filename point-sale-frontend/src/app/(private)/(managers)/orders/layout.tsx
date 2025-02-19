import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import Link from "next/link";
import { BsPlus } from "react-icons/bs";
import { IoMdAlert } from "react-icons/io";

interface LayoutOrdersProps {
  children: React.ReactNode;
}

export default function LayoutOrders(props: LayoutOrdersProps) {
  const { children } = props;
  return (
    <CenterSection className="w-full px-0 pt-0 flex flex-col bg-white shadow rounded-b-2xl">
      <header className="flex h-auto bg-white border-x justify-between items-center p-2 text-gray-500 font-semibold">
        <h1 className={`${fontSaira} text-lg`}>Meus Pedidos</h1>
        <div className="relative group/link">
          <Link
            href="/orders/create"
            className="bg-gray-200 p-1 px-2 text-gray-500 text-md rounded-md flex gap-2 items-center"
          >
            <BsPlus size={20} />
            Pedido
          </Link>

          <div className="right-0 bg-white items-center gap-2 hidden group-hover/link:flex border rounded px-2 w-[14rem] p-1 h-xl absolute mt-1 z-30 text-sm">
            <div>
              <IoMdAlert size={20} />
            </div>
            Para criar uma nova ordem, é necessário ter mesas disponíveis
          </div>
        </div>
      </header>
      {children}
    </CenterSection>
  );
}
