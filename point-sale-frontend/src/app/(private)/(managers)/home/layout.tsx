import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { IStore } from "@/interfaces/IStore";
import { fetchServer } from "@/utils/api-server";
import { GoPencil } from "react-icons/go";

type LayoutProps = {
  children: React.ReactNode;
};

type BannerProps = {
  id: string;
  title: string;
};

type ResponseStoreInformations = {
  revenue: string;
  store: IStore;
};

function Banner(props: BannerProps) {
  const { id, title } = props;
  return (
    <div className="flex w-full justify-center relative mt-2">
      <CenterSection className="px-[1rem] py-0 w-full">
        <div className="p-3 bg-white border rounded-md justify-between flex items-center">
          <div className="flex gap-2 items-center">
            <div className="flex w-7 h-7 bg-white rounded-md border"></div>
            <span
              className={`${fontSaira} font-semibold text-gray-600 dark:text-gray-100 text-lg`}
            >
              {title}
            </span>
          </div>

          <button className="bg-gray-100 w-7 h-7 rounded grid place-items-center">
            <GoPencil />
          </button>
        </div>
      </CenterSection>
    </div>
  );
}

export default async function Layout({ children }: LayoutProps) {
  const data = await fetchServer<ResponseStoreInformations>({
    url: "stores/informations",
  });

  if (!data?.revenue || !data?.store) {
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
      <Banner title={data.store.name} id={data.store.id} />
      {children}
    </section>
  );
}
