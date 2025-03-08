import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

const useStoreInformations = () => {
  const getAllInformations = () => {
    const { data: store, isLoading } = useQuery({
      queryKey: ["stores", "my"],
      queryFn: async () => await api.get("/stores/my"),
    });

				return {
					store,
					isLoading
				}
  };

  return {
    getAllInformations,
  };
};

export { useStoreInformations };
