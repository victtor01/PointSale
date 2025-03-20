"use client";

import { fontOpenSans, fontSaira } from "@/fonts";
import { useEmployee } from "@/hooks/use-employee";
import {
  createEmployeeSchema,
  CreateEmployeeSchemaProps,
} from "@/schemas/create-employee-schema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";
import { FormProvider, useForm } from "react-hook-form";
import { FaArrowLeftLong } from "react-icons/fa6";
import { updateEmployeeComponents } from "./update";

const useCreateEmployee = () => {
  const form = useForm<CreateEmployeeSchemaProps>({
    resolver: zodResolver(createEmployeeSchema),
  });

  const { create } = useEmployee();

  const update = async (data: CreateEmployeeSchemaProps) => {
    try {
      const created = await create(data);
      console.log(created);
    } catch (error: unknown) {
      console.log(error)
    }
  };

  return {
    form,
    update,
  };
};

export default function Create() {
  const { form, update } = useCreateEmployee();
  const router = useRouter();

  console.log(form?.formState?.errors);

  return (
    <div className="py-5 w-full max-w-[50rem] mx-10">
      <header className="flex items-center gap-4 justify-between">
        <div className="flex items-center gap-4">
          <button
            onClick={() => router.back()}
            className="opacity-80 hover:opacity-100 w-9 h-9 bg-gray-200 grid place-items-center text-gray-500 rounded-full"
          >
            <FaArrowLeftLong />
          </button>

          <h1 className={`${fontOpenSans} text-lg text-gray-500 font-semibold`}>
            Criar novo funcionário
          </h1>
        </div>
      </header>

      <form
        className="flex flex-col text-gray-500 mt-4 gap-3"
        onSubmit={form.handleSubmit(update)}
      >
        <FormProvider {...form}>
          <updateEmployeeComponents.personalInformation />
          <updateEmployeeComponents.salaryAndPositions />
          <updateEmployeeComponents.password />
        </FormProvider>
        <footer className="flex w-full mt-4">
          <button className="px-5 py-2 font-semibold bg-indigo-500 text-indigo-50 rounded-md opacity-90 hover:opacity-100">
            <span className={`${fontSaira}`}>Salvar</span>
          </button>
        </footer>
      </form>
    </div>
  );
}
