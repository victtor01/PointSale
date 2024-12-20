import { fontSaira } from "@/fonts";
import { LoginForm } from "./login-form";
import { fetchServer } from "@/utils/api-server";
import { IStore } from "@/interfaces/IStore";

interface SelectStoreWithIdProps {
  params: Promise<{ storeId: string }>;
  children: Promise<React.ReactNode>;
}

async function SelectStoreWithId(props: SelectStoreWithIdProps) {
  const params = await props.params;
  const store = await fetchServer<IStore>({ url: `stores/${params?.storeId}` });

  if (!store) {
    return (
      <div className="m-auto flex flex-col gap-2">
        <span className="font-semibold">Nenhuma loja encontrada!</span>
        <button className="text-white">Voltar para as minhas lojas!</button>
      </div>
    );
  }

  return (
    <div className="flex m-auto w-full h-auto min-h-[25rem] max-w-[55rem] shadow-2xl shadow-gray-300 overflow-hidden bg-white rounded-2xl">
      <section className="sm:flex hidden flex-1 bg-gradient-to-l from-indigo-600 to-indigo-700">
        <header className="grid place-items-center flex-1 ">
          <h1 className={`${fontSaira} text-xl text-white flex flex-col`}>
            Entrar na loja <b className="font-semibold text-3xl">{store?.name}</b>
          </h1>
        </header>
      </section>

      <section className="flex flex-1 bg-white p-5">
        <LoginForm store={store}></LoginForm>
      </section>
    </div>
  );
}

export default SelectStoreWithId;
