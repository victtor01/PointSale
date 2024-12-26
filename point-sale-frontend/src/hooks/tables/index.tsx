import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query"

const useAllTables = () => {
  const { data, isLoading } = useQuery({
    queryKey: ["tables"],
    queryFn: async () => {
      return (await api.get("/tables")).data
    }
  });

  return {
    data, isLoading
  }
}

export { useAllTables }