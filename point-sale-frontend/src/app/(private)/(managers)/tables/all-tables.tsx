import { fontSaira } from "@/fonts";
import { useAllTables } from "@/hooks/tables";
import { ITable } from "@/interfaces/ITable";
import { Table } from "./table";
import { useOrders } from "@/hooks/use-orders";

const MAX_COUNT_OF_STORES = 25;

function AllTables() {
  const { data: tables, isLoading } = useAllTables();
  const { getAllOrders } = useOrders();
  const { orders } = getAllOrders();

  if (isLoading) return "loading...";

  return (
    <div className="flex flex-col gap-1 flex-1">
      <section
        className={`flex w-full gap-5 ${fontSaira} mt-3 px-3 mb-5  items-center select-none bg-white border rounded-lg border-b-2 p-2`}
      >
        <div className="flex-1 flex gap-2 text-center items-center">
          <span className="text-[1.4rem] font-semibold text-shadow bg-indigo-600 w-[2.2rem] h-[2.2rem] grid place-items-center text-white rounded-md
          border-2 border-indigo-500">
            {orders?.length}
          </span>
          <span className="text-md font-semibold">
            Pedidos no total
          </span>
        </div>

        <div className="p-1 px-3 bg-gray-200 rounded-full font-semibold opacity-50">
          {tables?.length || 0} / {MAX_COUNT_OF_STORES}
        </div>
      </section>

      <section className="grid sm:grid-cols-2 gap-5 w-full">
        {tables?.map((table: ITable) => (
          <Table key={table.number} table={table} />
        ))}
      </section>
    </div>
  );
}

export { AllTables };
