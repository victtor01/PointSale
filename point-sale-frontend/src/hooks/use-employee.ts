import { IEmployee } from "@/interfaces/IEmployee";
import { UpdateEmployeeSchema } from "@/schemas/update-employee-schema";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";
import { z } from "zod";

export type IUpdateEmployee = z.infer<typeof UpdateEmployeeSchema>;

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

  const update = async (employeeId: string, data: IUpdateEmployee) => {
    try {
      await api.put(`/employee/${employeeId}`, data);
      toast("Atualizado com sucesso!");
    } catch (error) {
      console.log(error);
    }
  };

  return {
    getAllEmployees,
    findById,
    update,
  };
};
