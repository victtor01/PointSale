import { fontSaira } from "@/fonts";
import { useAllTables } from "@/hooks/tables";
import { FaArrowRight } from "react-icons/fa";

type ITable = {
  id: string;
  number: number;
  quantityOfOrders?: number | null;
};

type TableProps = {
  table: ITable;
};

function Table(props: TableProps) {
  const { table } = props;

  return (
    <div className="flex group relative items-center justify-between select-none gap-4 p-2 px-3 bg-white shadow transition-all rounded-lg opacity-90 hover:opacity-100">
      <div
        className={`pointer-events-none flex-1 flex flex-col font-semibold p-2 bg-gray-200/70 rounded-md ${fontSaira}`}
      >
        <span className="flex text-sm">Número da mesa</span>
        <span className={`text-3xl font-semibold ${fontSaira}`}>
          {table.number}
        </span>
      </div>

      <div
        className={`pointer-events-none flex-1 flex-col font-semibold ${fontSaira}`}
      >
        <span className="flex text-sm capitalize">pedidos hoje</span>
        <span
          className={`text-lg flex font-semibold text-white w-10 h-10 items-center justify-center bg-gradient-45 from-gray-900 to-gray-800 border-4 border-gray-300 rounded-xl ${fontSaira}`}
        >
          {table?.quantityOfOrders || 0}
        </span>
      </div>

      <div className="flex items-center absolute right-2 top-[50%] translate-y-[-50%]">
        <button
          className="flex w-9 items-center justify-center text-violet-200 h-9 translate-x-[2rem]
          rounded-xl bg-gray-800 to-gray-800 opacity-0 hover:opacity-100 group-hover:translate-x-[1.5rem]
          group-hover:opacity-100 transition-all z-20 "
        >
          <FaArrowRight />
        </button>
      </div>
    </div>
  );
}

function AllTables() {
  const { data: tables, isLoading } = useAllTables();

  if (isLoading) return "loading...";

  return (
    <div className="flex flex-col gap-1">
      <section
        className={`flex w-full gap-5 ${fontSaira} mt-5 items-center select-none`}
      >
        <div className="flex-1 flex gap-2 text-center">
          <span className="text-[2rem] text-gray-700 font-semibold text-shadow">
            15
          </span>
          <span className="text-md font-semibold self-end mb-2">
            Pedidos no total
          </span>
        </div>

        <div className="p-1 px-3 bg-gray-200 rounded-full font-semibold opacity-50">
          2/25
        </div>
      </section>

      <section className="grid grid-cols-2 gap-5 w-full">
        {tables?.map((table: ITable) => (
          <Table key={table.number} table={table} />
        ))}
      </section>
    </div>
  );
}

export { AllTables }