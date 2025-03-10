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

  const findById = (employeeId: string) => {
    const { data: employee, isLoading } = useQuery<IEmployee>({
      queryKey: ["employees", employeeId],
      queryFn: async () => (await api.get(`/employee/${employeeId}`)).data,
    });

    return {
      employee,
      isLoading,
    };
  };

  return {
    getAllEmployees,
    findById,
  };
};
