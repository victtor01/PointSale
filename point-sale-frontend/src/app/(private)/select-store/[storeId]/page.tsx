import { fontSaira } from "@/fonts";
import { LoginForm } from "./login-form";
import { fetchServer } from "@/utils/api-server";
import { IStore } from "@/hooks/select-store/use-store";

interface SelectStoreWithIdProps {
  params: Promise<{ storeId: string }>;
  children: Promise<React.ReactNode>;
}

async function SelectStoreWithId(props: SelectStoreWithIdProps) {
  const params = await props.params;
  const store = await fetchServer<IStore>({ url: `stores/${params?.storeId}` });

  return (
    <div className="flex m-auto w-full max-w-[55rem] min-h-[30rem] overflow-hidden bg-white rounded-lg">
      <section className="flex flex-1 bg-gradient-45 from-indigo-600 to-violet-600">
        <header className="grid place-items-center flex-1 ">
          <h1 className={`${fontSaira} text-2xl text-white`}>
            Entrar na loja <b className="font-semibold">{store?.name}</b>
          </h1>
        </header>
      </section>

      <section className="flex flex-1 bg-white">
        <LoginForm />
      </section>
    </div>
  );
}

export default SelectStoreWithId;
