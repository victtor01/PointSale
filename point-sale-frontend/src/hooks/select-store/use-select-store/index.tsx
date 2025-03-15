import { api } from "@/utils/api";

interface ISelectStore {
  storeId: string;
  password?: string | null;
}

interface IUseLoginStoreResponse {
  selectStore: (props: ISelectStore) => Promise<void>;
}

const useLoginStore = (): IUseLoginStoreResponse => {
  const selectStore = async (props: ISelectStore) => {
    try {
      const { storeId, password = null } = props;
      const URL_TO_LOGIN = `/auth/select/${storeId}`;
      const responseToLogin = await api.post(URL_TO_LOGIN, { password  });
      return responseToLogin.data;
    } catch {
      throw new Error("houve um erro ao tentar selecionar store")
    }
  };

  return {
    selectStore,
  };
};

export { useLoginStore };
