import { CenterSection } from "@/components/center-section";
import { fontInter, fontRoboto, fontSaira } from "@/fonts";
import Link from "next/link";
import { BsThreeDots } from "react-icons/bs";
import { FaHashtag } from "react-icons/fa";
import { GoDotFill } from "react-icons/go";
import { MdEmail } from "react-icons/md";

const Employees = () => {
  return (
    <div className="flex w-full p-2 flex-col gap-2">
      <header className="font-semibold flex justify-between items-center">
        <h1 className={fontSaira}>Funcionários</h1>

        <div className="flex items-center gap-2">
          <div className="p-1 px-3 bg-gray-100 text-sm rounded-full ">
            21/20
          </div>
        </div>
      </header>

      <div className="flex gap-2 items-center flex-wrap">
        <Link
          href="#"
          className="flex p-5 opacity-90 hover:opacity-100 hover:shadow-xl  flex-1 w-full max-w-[15rem] border-b-4 rounded-md items-center flex-col shadow bg-white"
        >
          <header className="flex justify-between w-full items-center">
            <div className="bg-emerald-500 text-white p-1 px-3 flex gap-2 items-center text-xs rounded-full">
              <GoDotFill />
              <span>Ativo</span>
            </div>
            <button className="p-1 px-2">
              <BsThreeDots size={15} />
            </button>
          </header>

          <section className="flex flex-col gap-0 mt-4 items-center">
            <div className="w-14 h-14 bg-gray-200 rounded-full shadow-inner" />
            <div className="flex flex-col gap-[-1rem] items-center mt-3">
              <h1
                className={`${fontSaira} text-gray-500 font-semibold text-lg`}
              >
                José Victor
              </h1>
              <span
                className={`${fontRoboto} text-gray-500 opacity-70 text-sm`}
              >
                CEO
              </span>
            </div>
          </section>

          <section className="w-full flex rounded-md bg-gray-100 text-sm border p-2 mt-4 flex-col gap-2">
            <div className="flex gap-2 items-center text-sm">
              <FaHashtag size={14} />
              <span className={fontInter}>025090</span>
            </div>
            <div className="flex gap-2">
              <MdEmail size={15} className="mt-1"/>
              <div className="flex flex-col gap-1">
                <span
                  className={`${fontRoboto} bg-white p-[0.1rem] border px-3 rounded-full`}
                >
                  email@gmail.com
                </span>
                <span
                  className={`${fontRoboto} p-[0.1rem] bg-white border px-2 rounded-full`}
                >
                  (83) 98803-2789
                </span>
              </div>
            </div>
          </section>

          <footer className="text-sm mt-5 w-full flex opacity-90">
            Criado em 24 de set. de 2024
          </footer>
        </Link>
      </div>
    </div>
  );
};

export default function Home() {
  return (
    <CenterSection className="p-0 px-2 mt-4">
      <Employees />
    </CenterSection>
  );
}
