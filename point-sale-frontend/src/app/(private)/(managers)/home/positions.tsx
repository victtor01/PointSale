import { fontSaira } from "@/fonts";
import { usePositions } from "@/hooks/use-positions";
import Link from "next/link";
import { BiPlus } from "react-icons/bi";
import { FaChevronRight } from "react-icons/fa";

export const ListOfPositions = () => {
  const { useAllPositions: getAllPositions } = usePositions();
  const { positions } = getAllPositions();

  return (
    <div className="flex flex-col gap-2 relative">
      <header className="w-full justify-between gap-2 items-center flex relative">
        <div className={`${fontSaira}`}>
          <h1 className="font-semibold">Cargos - {positions?.length}</h1>
        </div>

        <div>
          <button className="rounded-full w-9 h-9 bg-indigo-600 text-gray-100 border-4  border-indigo-400 hover:ring-2 grid place-items-center">
            <BiPlus size={22} />
          </button>
        </div>
      </header>

      <section className="flex flex-col border rounded-xl divide-y-2 bg-white">
        {positions?.map((position) => {
          return (
            <Link
              key={position.id}
              href={`?modal=edit-position&positionId=${position.id}`}
              className="flex justify-between gap-2 p-2 items-center
													hover:opacity-100 opacity-80 hover:bg-zinc-50"
            >
              <div className="flex flex-col">
                <div className="font-semibold text-lg">{position?.name}</div>
                <div className="text-sm opacity-70 flex">
                  {position?.employees?.length || 0} membros
                </div>
              </div>

              <div className="px-4 opacity-50">
                <FaChevronRight />
              </div>
            </Link>
          );
        })}
      </section>
    </div>
  );
};
