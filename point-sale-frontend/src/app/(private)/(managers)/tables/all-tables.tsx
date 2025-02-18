import { fontSaira } from "@/fonts";
import { useAllTables } from "@/hooks/tables";
import { ITable } from "@/interfaces/ITable";
import { Table } from "./table";

const MAX_COUNT_OF_STORES = 25;

function AllTables() {
  const { data: tables, isLoading } = useAllTables();

  if (isLoading) return "loading...";

  return (
    <div className="flex flex-col gap-1 flex-1 px-3">
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
