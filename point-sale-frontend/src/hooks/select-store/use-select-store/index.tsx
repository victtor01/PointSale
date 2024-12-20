import { api } from "@/utils/api";

interface ISelectStore {
  storeId: string;
  password?: string | null;
}

interface IUseLoginStoreResponse {
  selectStore: (props: ISelectStore) => Promise<any>;
}

const useLoginStore = (): IUseLoginStoreResponse => {
  const selectStore = async (props: ISelectStore): Promise<any> => {
    try {
      const { storeId, password = null } = props;
      const URL_TO_LOGIN = `/auth/select/${storeId}`;
      const responseToLogin = await api.post(URL_TO_LOGIN, { password  });
      console.log(responseToLogin);
      return responseToLogin.data;
    } catch (error: any) {
      throw new Error("houve um erro ao tentar selecionar store", error)
    }
  };

  return {
    selectStore,
  };
};

export { useLoginStore };
