import { useLoginStore } from "@/hooks/select-store/use-select-store";
import { IStore } from "@/interfaces/IStore";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";

const _PAGE_URL_SELECT_STORE = "/select-store";

const useSelectStore = () => {
  const { selectStore } = useLoginStore();
  const router = useRouter();

  const redirectToStore = async (store: IStore) => {
    const { id, password } = store;

    const url = `${_PAGE_URL_SELECT_STORE}/${id}`;

    try {
      if (password?.length) {
        router.push(url);
        return;
      } else {
        await selectStore({ storeId: store.id });
        toast.success("Loja seleciona com sucesso!", {
          toastId: "select-store-sucess",
        });
        
        router.push("/home");
      }
    } catch (error) {
      console.log(error);
      toast.error("Houve um erro ao tentar selecionar a loja!");
    }
  };

  return {
    redirectToStore,
  };
};

export { useSelectStore };

