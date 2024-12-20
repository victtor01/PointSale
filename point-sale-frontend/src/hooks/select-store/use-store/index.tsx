"use client"

import { IStore } from "@/interfaces/IStore"
import { api } from "@/utils/api"
import { useQuery } from "@tanstack/react-query"

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