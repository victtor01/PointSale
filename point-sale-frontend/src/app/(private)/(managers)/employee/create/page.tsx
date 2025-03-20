import { CenterSection } from "@/components/center-section";
import { fontOpenSans, fontSaira } from "@/fonts";
import { FaArrowLeftLong } from "react-icons/fa6";

export default function Create() {
  return (
    <CenterSection className="py-5">
      <header className="flex items-center gap-4 justify-between">
        <div className="flex items-center gap-4">
          <button className="opacity-80 hover:opacity-100 w-9 h-9 bg-gray-200 grid place-items-center text-gray-500 rounded-full">
            <FaArrowLeftLong />
          </button>

          <h1 className={`${fontOpenSans} text-lg text-gray-500 font-semibold`}>
            Criar novo funcionário
          </h1>
        </div>
      </header>

      <section className="flex flex-col text-gray-500 mt-4 gap-3">
        <div className="p-5 bg-white  rounded-xl border gap-4 flex flex-col">
          <div className="flex gap-2 items-center">
            <label htmlFor="" className="flex flex-col">
              <span className={`${fontOpenSans} text-md font-semibold`}>
                Username
              </span>

              <input
                type="text"
                className="w-full p-2 bg-white rounded-md border outline-none"
                placeholder="nome do funcionário"
              />
            </label>
          </div>

          <label htmlFor="" className="flex flex-col">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Primeiro nome *
            </span>

            <input
              type="text"
              className="w-full p-2 bg-white rounded-md border outline-none"
              placeholder="nome do funcionário"
            />
          </label>

          <label htmlFor="" className="flex flex-col">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Sobrenome
            </span>

            <input
              type="text"
              className="w-full p-2 bg-white rounded-md border outline-none"
              placeholder="nome do funcionário"
            />
          </label>
        </div>

        <div className="p-5 bg-white  rounded-lg border gap-4 flex flex-col">
          <label htmlFor="" className="flex flex-col">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Salário
            </span>

            <input
              type="text"
              className="w-full p-2 bg-white rounded-md border outline-none"
              placeholder="nome do funcionário"
            />
          </label>

          <label htmlFor="" className="flex flex-col">
            <span className={`${fontOpenSans} text-md font-semibold`}>
              Cargos
            </span>

            <input
              type="text"
              className="w-full p-2 bg-white rounded-md border outline-none"
              placeholder="nome do funcionário"
            />
          </label>
        </div>
      </section>

      <footer className="flex w-full mt-4">
        <button className="px-5 py-2 font-semibold bg-indigo-500 text-indigo-50 rounded-md opacity-90 hover:opacity-100">
          <span className={`${fontSaira}`}>Salvar</span>
        </button>
      </footer>
    </CenterSection>
  );
}
