import { IStore } from "@/interfaces/IStore";
import { fetchServer } from "@/utils/api-server";
import { Banner } from "./banner";

type LayoutProps = {
  children: React.ReactNode;
};

type ResponseStoreInformations = {
  revenue: string;
  store: IStore;
};

export default async function Layout({ children }: LayoutProps) {
  const data = await fetchServer<ResponseStoreInformations>({
    url: "stores/informations",
  });

  if (!data?.store) {
    return (
      <div className="flex flex-col absolute top-[50%] gap-3 left-[50%] text-xl translate-x-[-50%]">
        <div className="text-gray-600 font-semibold">
          Selecione a loja novamente
        </div>
        <button className="p-3 px-2 bg-gray-800 rounded text-white text-sm">
          Selecionar!
        </button>
      </div>
    );
  }

  return (
    <section className="h-auto w-full">
      <div className="flex absolute top-0 left-0 w-full h-[10rem] bg-gray-100 overflow-hidden">
      </div>
      <div className="flex flex-col gap-2 z-20 mt-4">
        <Banner title={data.store.name} id={data.store.id} />
        {children}
      </div>
    </section>
  );
}
