"use client";

import { api } from "@/utils/api";
import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";
import { useForm, UseFormReturn } from "react-hook-form";
import { toast } from "react-toastify";
import { z } from "zod";

const schemaLogin = z.object({
  email: z.string().email({ message: "O email é inválido!" }).min(1, "Email vazio!"),
  password: z.string().min(1, { message: "A senha deve contar ao menos 1 caractere!" }).max(30),
});

type SchemaLoginProps = {
  email: string;
  password: string;
};

type useFormLoginResponse = {
  form: UseFormReturn<SchemaLoginProps, unknown, undefined>;
};

const useFormLogin = (): useFormLoginResponse => {
  const form = useForm<SchemaLoginProps>({
    resolver: zodResolver(schemaLogin),
  });

  return { form };
};

const _PAGE_URL_SELECT_STORE = "/select-store"

const useLogin = () => {
  const router = useRouter();

  const submitLogin = async (data: SchemaLoginProps): Promise<void> => {
    try {
      await new Promise(resolve => setTimeout(resolve, 2000));
      const { email, password } = data;
      const body = { email, password };
      await api.post("/auth", body);
      toast.success("login sucessfull");
      router.replace(_PAGE_URL_SELECT_STORE);
    } catch (error: unknown) {
      console.log("Houve um erro ao tentar fazer o login!", error);
      throw new Error("teste");
    }
  };

  return {
    submitLogin,
  };
};

export { useFormLogin, useLogin };

