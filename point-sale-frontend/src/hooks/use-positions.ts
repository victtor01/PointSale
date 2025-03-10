import { IPositionEmployee } from "@/interfaces/IEmployee";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

export const usePositions = () => {
  const getAllPositions = () => {
    const { data: positions, isLoading } = useQuery<IPositionEmployee[]>({
      queryKey: ["positions"],
      queryFn: async () => (await api.get("/positions")).data,
    });

    return {
      positions,
      isLoading,
    };
  };

  return {
    getAllPositions,
  };
};
