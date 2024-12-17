"use client"

import { api } from "@/utils/api"
import { useQuery } from "@tanstack/react-query"

interface IStore {
  name: string,
  id: string,
}

const useStore = () => {
  const { data: stores, isLoading } = useQuery<IStore[]>({
    queryKey: ["stores", "my"],
    queryFn: async () => (await api.get("/stores/my")).data
  })

  return {
    stores,
    isLoading,
  }
}

export { useStore, type IStore }