"use client";

import { CustomInputCurrency } from "@/components/input-salary";
import { fontOpenSans } from "@/fonts";
import { CreateEmployeeSchemaProps } from "@/schemas/create-employee-schema";
import { useFormContext } from "react-hook-form";
import { FaCheck } from "react-icons/fa6";
import { IoMdAlert } from "react-icons/io";

const Error = (props: { message?: string }) => {
  if (props?.message) {
    return (
      <div className="flex gap-2 items-center text-red-200">
        <IoMdAlert />
        <span className="font-semibold text-rose-500">{props.message}</span>
      </div>
    );
  }
};

const PasswordContext = () => {
  const {
    register,
    formState: { errors },
  } = useFormContext<CreateEmployeeSchemaProps>();

  return (
    <div className="p-5 bg-white  rounded-xl border gap-4 flex flex-col">
      <label htmlFor="" className="flex flex-col flex-1">
        <span className={`${fontOpenSans} text-md font-semibold`}>
          Defina uma senha
        </span>

        <div className="flex gap-4 items-center w-full">
          <input
            type="text"
            className="w-full p-2 rounded-md flex-1 border outline-none"
            placeholder="•••••••••••••"
            {...register("password")}
          />
        </div>

        <Error message={errors?.password?.message} />
      </label>
    </div>
  );
};

const SalaryContext = () => {
  const { watch, setValue, formState, register } =
    useFormContext<CreateEmployeeSchemaProps>();
  const { errors } = formState;

  return (
    <div className="p-5 bg-white rounded-xl border gap-4 flex flex-col">
      <label htmlFor="salary" className="flex flex-col">
        <span className={`${fontOpenSans} text-md font-semibold`}>Salário</span>

        <CustomInputCurrency
          value={watch("salary")?.toString()}
          onChangeValue={(value) =>
            setValue("salary", value?.toString() || "0")
          }
          {...register("salary")}
          className="border"
          autoComplete="off"
          id="salary"
        />

        <Error message={errors?.salary?.message} />
      </label>

      <div className="flex flex-col gap-2">
        <span className={`${fontOpenSans} text-md font-semibold`}>Cargos</span>
        <div className="flex flex-wrap">
          <button
            type="button"
            data-includes={true}
            onClick={() => null}
            className="items-center flex overflow-hidden bg-white rounded-md border
            data-[includes=true]:ring-2 data-[includes=true]:ring-indigo-500"
          >
            <div
              data-includes={true}
              className="w-8 h-full border-gray-400/30 border-r bg-white grid place-items-center
              data-[includes=true]:bg-indigo-600 text-white"
            >
              {true && <FaCheck size={10} />}
            </div>
            <span
              data-includes={true}
              className="text-md font-semibold px-2 text-gray-500 p-1
              data-[includes=true]:bg-indigo-100"
            >
              Manager
            </span>
          </button>

          <Error message={errors?.positions?.message} />
        </div>
      </div>
    </div>
  );
};

const PersonalInformationContext = () => {
  const { register, formState } = useFormContext<CreateEmployeeSchemaProps>();
  const { errors } = formState;

  return (
    <div className="p-5 bg-white  rounded-xl border gap-4 flex flex-col">
      <label htmlFor="firstname" className="flex flex-col">
        <span className={`${fontOpenSans} text-md font-semibold`}>
          Primeiro nome *
        </span>

        <input
          type="text"
          id="firstname"
          autoComplete="off"
          {...register("firstName")}
          className="w-full p-2 bg-white rounded-md border outline-none"
          placeholder="Jonh "
        />
        <Error message={errors?.firstName?.message} />
      </label>

      <label htmlFor="lastname" className="flex flex-col">
        <span className={`${fontOpenSans} text-md font-semibold`}>
          Sobrenome
        </span>

        <input
          type="text"
          id="lastname"
          autoComplete="off"
          {...register("lastName")}
          className="w-full p-2 bg-white rounded-md border outline-none"
          placeholder="Doe"
        />
        <Error message={errors?.lastName?.message} />
      </label>

      <div className="flex gap-2 items-center">
        <label htmlFor="email" className="flex flex-col flex-1">
          <span className={`${fontOpenSans} text-md font-semibold`}>Email</span>

          <input
            type="text"
            id="email"
            autoComplete="off"
            {...register("email")}
            className="w-full flex-1 p-2 bg-white rounded-md border outline-none"
            placeholder="JonhDoe@example.com"
          />
          <Error message={errors?.email?.message} />
        </label>
        <label htmlFor="phone" className="flex flex-col flex-1">
          <span className={`${fontOpenSans} text-md font-semibold`}>
            Telefone
          </span>

          <input
            type="text"
            id="phone"
            {...register("phone")}
            autoComplete="off"
            className="w-full p-2 bg-white rounded-md border outline-none"
            placeholder="(83) 98803-2789"
          />
          <Error message={errors?.phone?.message} />
        </label>
      </div>
    </div>
  );
};

const updateEmployeeComponents = {
  personalInformation: PersonalInformationContext,
  salaryAndPositions: SalaryContext,
  password: PasswordContext,
};

export { updateEmployeeComponents };
