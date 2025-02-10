import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { BsPlus } from "react-icons/bs";

interface LayoutOrdersProps {
  children: React.ReactNode;
}

export default function LayoutOrders(props: LayoutOrdersProps) {
  const { children } = props;
  return (
    <main className="flex w-full h-screen overflow-auto">
      <CenterSection className="p-0 px-4">
        <header className="flex h-auto bg-white border justify-between items-center p-5 text-gray-500 rounded-b-2xl font-semibold">
          <h1 className={`${fontSaira} text-lg`}>Meus Pedidos</h1>
          <button className="bg-gray-200 p-1 px-2 text-gray-500 text-md rounded-md flex gap-2 items-center">
            <BsPlus size={20} />
            Pedido
          </button>
        </header>
        {children}
      </CenterSection>
    </main>
  );
}
