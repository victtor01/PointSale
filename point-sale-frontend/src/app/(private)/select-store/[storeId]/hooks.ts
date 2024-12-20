"use client";

import { useLoginStore } from "@/hooks/select-store/use-select-store";
import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { z } from "zod";

const formToLoginSchema = z.object({
  password: z
    .string({ message: "Digite a senha!" })
    .min(1, { message: "A senha requer no mínimo 1 caractere!" })
    .max(20, { message: "Senha longa demais!" }),
});

type PropsLoginForm = {
  storeId: string;
};

type PropsToLogin = {
  password: string;
};

const useLoginForm = ({ storeId }: PropsLoginForm) => {
  const { selectStore } = useLoginStore();
  const router = useRouter();

  const form = useForm<PropsToLogin>({
    resolver: zodResolver(formToLoginSchema),
  });

  const login = async (props: PropsToLogin) => {
    try {
      const { password } = props;
      await selectStore({ storeId, password });
      toast.success("Logado com sucesso!");
      router.push("/home");
    } catch (error) {
      toast.error("Senha incorreta, tente novamente!");
    }
  };

  return {
    form,
    login,
  };
};

export { useLoginForm };

