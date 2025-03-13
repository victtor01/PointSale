import { IPositionEmployee } from "@/interfaces/IEmployee";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

export const usePositions = () => {
  const useAllPositions = () => {
    const { data: positions, isLoading } = useQuery<IPositionEmployee[]>({
      queryKey: ["positions"],
      queryFn: async () => (await api.get("/positions")).data,
    });

    return {
      positions,
      isLoading,
    };
  };

  const useFindById = (positionId?: string | null) => {
    const { data: position, isLoading } = useQuery<IPositionEmployee>({
      queryKey: ["positions", positionId],
      queryFn: async () => (await api.get(`/positions/${positionId}`)).data,
    });

    return {
      position,
      isLoading,
    };
  };

  return {
    useAllPositions,
    useFindById,
  };
};
