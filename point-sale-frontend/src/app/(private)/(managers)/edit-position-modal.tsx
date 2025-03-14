"use client";

import { CenterSection } from "@/components/center-section";
import { DefaultLoader } from "@/components/default-loader";
import { fontSaira } from "@/fonts";
import { usePermissions } from "@/hooks/use-permissions";
import { usePositions } from "@/hooks/use-positions";
import {
  UpdatePositionData,
  UpdatePositionSchema,
} from "@/schemas/udpate-position.schema";
import { zodResolver } from "@hookform/resolvers/zod";
import { motion } from "framer-motion";
import Link from "next/link";
import { ReadonlyURLSearchParams, useRouter } from "next/navigation";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { BiCheck } from "react-icons/bi";
import { IoClose } from "react-icons/io5";

interface Props {
  params: ReadonlyURLSearchParams;
}

const useEditPosition = (positionId: string | null) => {
  const { useFindById, update } = usePositions();

  const { position } = useFindById(positionId) || { position: null };

  const form = useForm<UpdatePositionData>({
    resolver: zodResolver(UpdatePositionSchema),
  });

  const { setValue } = form;

  const togglePosition = (enumName: string) => {
    if (!position) return;

    const permissionsOfForm: string[] = form.getValues("permissions") || [];

    if (permissionsOfForm?.includes(enumName)) {
      setValue(
        "permissions",
        [...permissionsOfForm?.filter((p) => p !== enumName)],
        { shouldValidate: true }
      );
    } else {
      setValue("permissions", [...permissionsOfForm, enumName], {
        shouldValidate: true,
      });
    }
  };

  useEffect(() => {
    if (position) {
      form.reset({
        name: position?.name,
        permissions: position?.permissions,
      });
    }
  }, [position, form]);

  return {
    position,
    update,
    togglePosition,
    form,
  };
};

export const EditPosition = ({ params }: Props) => {
  const positionId = params.get("positionId");
  const { position, form, togglePosition, update } =
    useEditPosition(positionId);
  const { getAllPermissions } = usePermissions();
  const { permissions } = getAllPermissions();
  const { register, watch, handleSubmit, formState } = form;
  const { isSubmitting } = formState;
  const router = useRouter();

  return (
    <motion.div
      transition={{ type: "keyframes", duration: 0.1 }}
      initial={{ opacity: 0, scale: 1 }}
      animate={{ opacity: 1, scale: 1 }}
      exit={{ opacity: 0, scale: 1 }}
      className="flex fixed top-0 left-0 flex-col w-full h-screen bg-gray-100 z-50 overflow-y-auto"
    >
      <header className="bg-white w-full">
        <CenterSection className="h-auto w-full justify-between flex-row items-center py-3 pb-3">
          <h1 className="text-md font-semibold">Editar cargo</h1>

          <div>
            <Link
              href="?"
              className="opacity-60 w-7 h-7 border-current text-gray-400 hover:opacity-100 grid place-items-center border rounded-md"
            >
              <IoClose />
            </Link>
          </div>
        </CenterSection>
      </header>

      <CenterSection className="mt-10 text-gray-500">
        <form
          onSubmit={handleSubmit((data) => {
            if (position?.id) {
              update(position.id, data);
              router.push("?");
            }
          })}
        >
          <label htmlFor="name" className="flex flex-col gap-0">
            <span className="font-semibold">Nome</span>
            <input
              type="text"
              id="name"
              {...register("name")}
              placeholder="Digite um nome..."
              className="w-full p-2 rounded-lg bg-gray-200 outline-none border border-transparent focus:ring-2 ring-indigo-500"
            />
          </label>

          <div className="flex flex-col gap-1 mt-5">
            <header className="w-full justify-between flex items-center">
              <div className={fontSaira}>
                <h1 className="font-semibold text-lg ">Permissões</h1>
              </div>

              <div className="text-sm rounded-md">
                {watch("permissions")?.length || 0} permissões selecionadas.
              </div>
            </header>

            <section className="rounded-md border bg-white flex flex-col divide-y">
              {permissions?.map((permission, index: number) => {
                const selected =
                  watch("permissions")?.some((permissions) =>
                    permissions.includes(permission.enumName)
                  ) ?? false;

                return (
                  <div
                    key={index}
                    className="w-full flex gap-5 items-center p-4"
                  >
                    <motion.button
                      onClick={() => togglePosition(permission.enumName)}
                      data-selected={!!selected}
                      type="button"
                      className="min-w-[3rem] overflow-hidden grid items-center relative
                      h-[1.5rem] shadow-inner rounded-full bg-gray-200 ring-gray-200
                      ring-4 opacity-90 data-[selected=true]:opacity-100
                      data-[selected=true]:ring-indigo-500 data-[selected=true]:bg-indigo-500"
                    >
                      <motion.div
                        layout
                        data-selected={!!selected}
                        animate={{ x: selected ? "100%" : "0" }}
                        className="h-[1.5rem] w-[1.5rem] bg-white rounded-full shadow-inner z-10
                        data-[selected=true]:text-indigo-500 opacity-90 text-gray-300 flex absolute items-center justify-center"
                      >
                        {selected && <BiCheck size={20} />}
                        {!selected && <IoClose size={20} />}
                      </motion.div>
                    </motion.button>

                    <div className="flex flex-col">
                      <span className={`${fontSaira} text-lg font-semibold`}>
                        {permission?.name}
                      </span>

                      <p className="text-sm opacity-60">
                        {permission?.description}
                      </p>
                    </div>
                  </div>
                );
              })}
            </section>

            <footer className="mt-5">
              <button className="p-2 px-4 bg-indigo-500 text-white hover:bg-indigo-400 rounded-md flex items-center justify-center gap-2">
                {isSubmitting && <DefaultLoader />}
                <span>Salvar</span>
              </button>
            </footer>
          </div>
        </form>
      </CenterSection>
    </motion.div>
  );
};
