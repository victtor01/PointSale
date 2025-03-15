import { IPermissionInfo } from "@/interfaces/IPermissionInfo";
import { api } from "@/utils/api";
import { useQuery } from "@tanstack/react-query";

export const usePermissions = () => {
  const useGetAllPermissions = () => {
    const { data: permissions, isLoading } = useQuery<IPermissionInfo[]>({
      queryKey: ["permissions"],
      queryFn: async () => (await api.get("/permissions")).data,
    });

    return {
      permissions,
      isLoading,
    };
  };

  return {
    getAllPermissions: useGetAllPermissions,
  };
};
