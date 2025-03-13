"use client";

import { CenterSection } from "@/components/center-section";
import { CustomInputCurrency } from "@/components/input-salary";
import { SimpleLoader } from "@/components/simple-loader";
import { fontRoboto, fontSaira, fontValela } from "@/fonts";
import { IUpdateEmployee, useEmployee } from "@/hooks/use-employee";
import { usePositions } from "@/hooks/use-positions";
import { IEmployee, IPositionEmployee } from "@/interfaces/IEmployee";
import { useParams } from "next/navigation";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { BiSolidPhone } from "react-icons/bi";
import { FaCheck, FaHashtag, FaLock, FaUser, FaUserTie } from "react-icons/fa";
import { MdEmail } from "react-icons/md";
import { RiEdit2Fill } from "react-icons/ri";
import {
  ActiveStatus,
  EmployeeCard,
  PausedStatus,
} from "@/components/employee-card";
import { zodResolver } from "@hookform/resolvers/zod";
import { UpdateEmployeeSchema } from "@/schemas/update-employee-schema";
import { DefaultLoader } from "@/components/default-loader";

interface ParamsOf extends Record<string, string> {
  id: string;
}

const useDetailsEmployee = (employee?: IEmployee) => {
  const form = useForm<IUpdateEmployee>({
    resolver: zodResolver(UpdateEmployeeSchema),
  });

  const { reset, watch, setValue } = form;

  const positions = watch("positions");

  const togglePosition = (id: string) => {
    if (positions?.includes(id)) {
      setValue(
        "positions",
        positions?.filter((pos) => pos !== id),
        { shouldValidate: true }
      );
    } else {
      setValue("positions", [...positions, id], { shouldValidate: true });
    }
  };

  useEffect(() => {
    console.log(employee);
    form.reset({
      salary: Number(employee?.salary) || 0,
      username: Number(employee?.username),
      firstName: employee?.firstName || "",
      email: employee?.email || "",
      phone: employee?.phone || "",
      positions: employee?.positions?.map((e) => e.id) ?? [],
    });
  }, [employee, reset, form]);

  return {
    form,
    togglePosition,
  };
};

export default function Details() {
  const { id } = useParams<ParamsOf>();
  const { useAllPositions: getAllPositions } = usePositions();
  const { positions, isLoading: loadingPositions } = getAllPositions();
  const { findById, update } = useEmployee();
  const { employee, isLoading: loadingEmployee } = findById(id);
  const { form, togglePosition } = useDetailsEmployee(employee);
  const { register, watch, setValue, handleSubmit, formState } = form;
  const { isSubmitting } = formState;

  const positionsOfEmployee: string[] =
    employee?.positions?.map((p) => p.id) || [];

  if (loadingEmployee || loadingPositions) {
    return (
      <div className="relative w-full h-full grid place-items-center">
        <div className="flex flex-col items-center">
          <SimpleLoader />
          <span
            className={`${fontValela} text-indigo-600 text-lg font-semibold`}
          >
            Carregando
          </span>
        </div>
      </div>
    );
  }

  if (!employee?.id) {
    throw new Error("Não foi encontrado nenhum funcionário");
  }

  return (
    <CenterSection className="mt-5">
      <div className="flex gap-2 justify-between items-center">
        <div className="flex items-center gap-2">
          <RiEdit2Fill />
          <h1 className={`${fontSaira} text-gray-600 rounded text-xl`}>
            Editar informações
          </h1>
        </div>
      </div>

      <form
        onSubmit={handleSubmit((data) => update(employee.id, data))}
        className="w-full flex flex-col mt-6 gap-4 p-5 z-30 border rounded-md bg-white"
      >
        <div className="text-md font-semibold flex gap-2 flex-col">
          <div className="flex gap-2 items-center">
            <FaLock />
            <span>Atividade</span>
          </div>

          <div className="flex gap-2 items-center">
            <button
              type="button"
              className="w-[4rem] overflow-hidden flex justify-end items-center border-emerald-500
              h-[2rem] shadow-inner relative rounded-md bg-green-600 ring-2 ring-emerald-500"
            >
              <div className="absolute w-[1rem] rounded-full h-[1rem] border-2 left-2"></div>
              <div className="h-[2rem] w-[2rem] bg-white rounded-md shadow-xl"></div>
            </button>

            <div className="p-1 px-4 bg-white shadow rounded-md">
              Usuário ativo
            </div>
          </div>
        </div>

        <label
          htmlFor="username"
          className="text-md font-semibold flex flex-col mt-4"
        >
          <div className="flex gap-2 items-center">
            <FaHashtag />
            <span>Usuário</span>
          </div>

          <input
            type="text"
            id="username"
            {...register("username")}
            className="p-2 bg-gray-200 rounded-md cursor-not-allowed shadow-inner shadow-gray-300"
            disabled
          />
        </label>

        <label
          htmlFor="firstname"
          className="text-md font-semibold flex flex-col"
        >
          <div className="flex gap-2 items-center px-1">
            <FaUser size={14} />
            <span>Primerio nome *</span>
          </div>

          <input
            id="firstname"
            type="text"
            {...register("firstName")}
            className="p-2 bg-white rounded-md border outline-none focus:ring-4 ring-indigo-500/40"
            placeholder="Jonh Doe"
          />
        </label>

        <label
          htmlFor="lastname"
          className="text-md font-semibold flex flex-col"
        >
          <div className="flex gap-2 items-center px-1">
            <FaUser size={14} />
            <span>Sobrenome</span>
          </div>

          <input
            id="lastname"
            type="text"
            {...register("lastName")}
            className="p-2 bg-white rounded-md border outline-none focus:ring-4 ring-indigo-500/40"
            placeholder="Jonh Doe"
          />
        </label>

        <div className="flex gap-4 w-full flex-wrap">
          <label
            htmlFor="email"
            className="text-md font-semibold flex flex-col flex-1"
          >
            <div className="flex gap-2 items-center">
              <MdEmail />
              <span>Email</span>
            </div>

            <input
              id="email"
              type="email"
              {...register("email")}
              className="p-2 bg-white rounded-md border outline-none focus:ring-4 ring-indigo-500/40"
              placeholder="JonhDoe@gmail.com"
            />
          </label>
          <label
            htmlFor="phone"
            className="text-md font-semibold flex flex-col flex-1"
          >
            <div className="flex gap-2 items-center">
              <BiSolidPhone />
              <span>Telefone</span>
            </div>

            <input
              type="text"
              id="phone"
              {...register("phone")}
              className="p-2 bg-white rounded-md border outline-none focus:ring-4 ring-indigo-500/40"
              placeholder="12345678901"
            />
          </label>
        </div>

        <label
          htmlFor="salary"
          className="text-md font-semibold flex flex-col flex-1"
        >
          <div className="flex gap-2 items-center">
            <span>R$ Salário</span>
          </div>

          <CustomInputCurrency
            id="salary"
            value={watch("salary")?.toString()}
            className="border"
            onChangeValue={(value) => setValue("salary", Number(value) || 0)}
          />
        </label>

        <section className="flex flex-col gap-2 mt-10 w-full">
          <header className="text-lg flex text-gray-600 w-full overflow-hidden font-semibold gap-4 justify-between">
            <div className="flex items-center gap-2">
              <FaUserTie />
              <h1 className={fontRoboto}>Cargos</h1>
            </div>

            <div className="flex items-center gap-3 flex-1 justify-end overflow-hidden">
              <div className="text-sm opacity-60 w-auto text-ellipsis whitespace-nowrap truncate overflow-hidden">
                {positionsOfEmployee?.length || 0} Cargos selecionados
              </div>
              <button
                type="button"
                className="p-1 px-3 font-semibold text-nowrap text-gray-500 bg-white border text-sm rounded-md"
              >
                Novo cargo
              </button>
            </div>
          </header>

          <div className="flex gap-2 flex-wrap">
            {positions?.map((position: IPositionEmployee, index: number) => {
              const includes: boolean = watch("positions")?.includes(
                position.id
              );
              return (
                <button
                  type="button"
                  key={index}
                  data-includes={includes}
                  onClick={() => togglePosition(position.id)}
                  className="items-center flex overflow-hidden bg-white rounded-md border
                  data-[includes=true]:ring-2 data-[includes=true]:ring-indigo-500"
                >
                  <div
                    data-includes={includes}
                    className="w-8 h-full border-gray-400/30 border-r bg-white grid place-items-center
                    data-[includes=true]:bg-indigo-600 text-white"
                  >
                    {includes && <FaCheck size={10} />}
                  </div>
                  <span
                    data-includes={includes}
                    className="text-md font-semibold px-2 text-gray-500 p-1
                    data-[includes=true]:bg-indigo-100"
                  >
                    {position.name}
                  </span>
                </button>
              );
            })}
          </div>
        </section>

        <section className="flex mt-6 rounded-md flex-col">
          <div className="text-lg flex text-gray-600 font-semibold justify-between">
            <div>
              <h1 className={fontRoboto}>Preview</h1>
            </div>
          </div>

          <EmployeeCard.Container
            employeeId={id}
            className="pointer-events-none select-none"
          >
            <EmployeeCard.Header>
              {true ? <ActiveStatus /> : <PausedStatus />}
            </EmployeeCard.Header>
            <EmployeeCard.Photo
              name={watch("firstName")}
              positions={positions?.filter((p) =>
                watch("positions")?.includes(p.id)
              )}
            />
            <EmployeeCard.Informatins
              username={watch("username")?.toString()}
              email={watch("email")}
              phone={watch("phone")}
            />
          </EmployeeCard.Container>

          <footer className="flex items-center justify-between mt-5 mb-6">
            <button
              disabled={isSubmitting}
              data-submitting={isSubmitting}
              className="px-3 py-2 text-md bg-indigo-600 rounded-md text-white
              hover:bg-indigo-500 shadow-lg flex items-center gap-2
              data-[submitting=true]:opacity-40
              "
            >
              {isSubmitting && <DefaultLoader />}
              <span className={fontSaira}>Salvar Alterações</span>
            </button>
          </footer>
        </section>
      </form>
    </CenterSection>
  );
}
