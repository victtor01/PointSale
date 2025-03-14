import { IPositionEmployee } from "@/interfaces/IEmployee";
import { queryClient } from "@/providers/query-client-provider";
import { UpdatePositionData } from "@/schemas/udpate-position.schema";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";

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

  const update = async (positionId: string, data: UpdatePositionData) => {
    try {
      await api.put(`/positions/${positionId}`, data);

      await queryClient.invalidateQueries({
        queryKey: ["positions"],
      });

      toast.success("Cargo atualizado com sucesso!");
    } catch (error: unknown) {
      console.log(error);
    }
  };

  return {
    useAllPositions,
    useFindById,
    update,
  };
};
