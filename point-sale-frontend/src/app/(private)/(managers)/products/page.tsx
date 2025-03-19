import { CenterSection } from "@/components/center-section";
import { fontOpenSans, fontSaira } from "@/fonts";
import { BiPlus } from "react-icons/bi";

export default function Products() {
  return (
    <>
      <header className="flex w-full flex-col bg-gray-950 text-gray-200 p-3 overflow-hidden">
        <CenterSection className="flex flex-col h-auto pb-2">
          <div className="flex w-full justify-between items-center">
            <h1
              className={`${fontOpenSans} text-2xl font-semibold text-blue-100`}
            >
              Meus produtos
            </h1>

            <button className="p-1 px-3 flex gap-2 items-center bg-indigo-500 opacity-95 hover:opacity-100 shadow-xl shadow-black text-indigo-100 rounded-md">
              <span>Novo</span>
              <BiPlus />
            </button>
          </div>

          <div className="w-full flex mt-4">
            <div className="p-2 items-end gap-3 bg-gray-900 px-4 w-full max-w-[20rem] border border-gray-800 rounded-md flex">
              <h1 className="text-[1.8rem] font-semibold">4</h1>
              <span>Produtos cadastrados</span>
            </div>
          </div>
        </CenterSection>
      </header>

      <CenterSection className="p-0 px-3 mt-4">
        <section className="w-full text-left rounded-md bg-white border overflow-hidden">
          <table className="w-full">
            <thead className="bg-indigo-50/50 text-gray-600 font-normal">
              <tr className={fontSaira}>
                <th scope="col" className="px-2 py-1">
                  Nome produto
                </th>
                <th scope="col" className="px-2 py-1">
                  Preço
                </th>
                <th scope="col" className="px-2 py-1">
                  Categorias
                </th>
                <th scope="col" className="px-2 py-1">
                  Ações
                </th>
              </tr>
            </thead>

            <tbody className="divide-y">
              <tr className="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700 border-gray-200">
                <th
                  scope="row"
                  className="px-2 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
                >
                  Apple MacBook Pro 17"
                </th>
                <td className="px-2 py-4">Laptop</td>
                <td className="px-2 py-4">$2999</td>
                <td className="px-2 py-4">
                  <a
                    href="#"
                    className="font-medium text-blue-600 dark:text-blue-500 hover:underline"
                  >
                    Edit
                  </a>
                </td>
              </tr>
              <tr className="odd:bg-white odd:dark:bg-gray-900 even:bg-gray-50 even:dark:bg-gray-800 border-b dark:border-gray-700 border-gray-200">
                <th
                  scope="row"
                  className="px-2 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
                >
                  Apple MacBook Pro 17"
                </th>
                <td className="px-2 py-4">Laptop</td>
                <td className="px-2 py-4">$2999</td>
                <td className="px-2 py-4">
                  <a
                    href="#"
                    className="font-medium text-blue-600 dark:text-blue-500 hover:underline"
                  >
                    Edit
                  </a>
                </td>
              </tr>
            </tbody>
          </table>
        </section>
      </CenterSection>
    </>
  );
}
