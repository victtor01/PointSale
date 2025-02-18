"use client"

import { IStore } from "@/interfaces/IStore"
import { api } from "@/utils/api"
import { useQuery } from "@tanstack/react-query"

interface ResponseGetAllStores {
  store: IStore,
  revenue: number;
}

const useStore = () => {
  const { data: storesResponse, isLoading } = useQuery<ResponseGetAllStores[]>({
    queryKey: ["stores", "my"],
    queryFn: async () => (await api.get("/stores/my")).data
  })

  return {
    storesResponse,
    isLoading,
  }
}

export { useStore, type IStore }