import { IEmployee } from "@/interfaces/IEmployee";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

export const useEmployee = () => {
  const getAllEmployees = () => {
    const { data: employees, isLoading } = useQuery<IEmployee[]>({
      queryKey: ["employees"],
      queryFn: async () => (await api.get("/employee")).data,
    });

    return {
      employees,
      isLoading,
    };
  };

  return {
    getAllEmployees,
  };
};
