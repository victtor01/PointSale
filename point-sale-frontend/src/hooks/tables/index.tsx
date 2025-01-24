import { ITable } from "@/interfaces/ITable";
import { queryClient } from "@/providers/query-client-provider";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";
import { toast } from "react-toastify";

type CreateTableProps = {
  number: string;
};

const useAllTables = () => {
  const { data, isLoading } = useQuery<ITable[]>({
    queryKey: ["tables"],
    queryFn: async () => {
      return (await api.get("/tables")).data;
    },
  });

  return {
    data,
    isLoading,
  };
};

const createTable = async (props: CreateTableProps) => {
  try {
    const { number } = props;
    const response = await api.post("/tables", { number });
    const data = response.data;
    queryClient.setQueryData(["tables"], (prev: ITable[]) => [...prev, data]);
    toast.success("Criado com sucesso!");
    return response;
  } catch (error) {
    console.log(error);
  }
};

export { useAllTables, createTable };
