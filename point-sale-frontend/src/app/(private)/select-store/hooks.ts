import { redirect } from "next/navigation";

const _PAGE_URL_SELECT_STORE = "/select-store"

const useSelectStore = () => {
  
  const redirectToStore = (storeId: string) => {
    redirect(`${_PAGE_URL_SELECT_STORE}/${storeId}`);  
  };

  return {
    redirectToStore,
  };
};

export { useSelectStore };

