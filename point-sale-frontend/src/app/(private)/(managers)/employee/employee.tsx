"use client";

import {
  ActiveStatus,
  EmployeeCard,
  PausedStatus,
} from "@/components/employee-card";
import { fontOpenSans } from "@/fonts";
import { useEmployee } from "@/hooks/use-employee";
import { IEmployee } from "@/interfaces/IEmployee";
import { motion } from "framer-motion";
import Link from "next/link";
import { BiPlus } from "react-icons/bi";

export const Employees = () => {
  const { useGetAllEmployees: getAllEmployees } = useEmployee();
  const { employees } = getAllEmployees();

  return (
    <div className="flex w-full mt-2 flex-col gap-4">
      <header className="font-semibold flex justify-between items-end text-gray-500 rounded-lg z-20">
        <div className="flex gap-2 text-xl">
          <h1 className={fontOpenSans}>Funcionários</h1>
        </div>
        <div className="flex items-center gap-5">
          <Link
            href={"/employee/create"}
            className="px-3 py-2 bg-indigo-500/20 text-sm rounded-lg opacity-90 hover:opacity-100 shadow-md hover:shadow-lg hover:shadow-indigo-500/30 shadow-indigo-500/30 flex gap-2 items-center justify-center"
          >
            <BiPlus size={20} />
            <span>Funcionário</span>
          </Link>
        </div>
      </header>

      <div className="bg-white/50 absolute z-10 top-0 left-0 w-full h-[10rem] ">
      </div>

      <div className="bg-gradient-to-b z-0 from-indigo-200/10 to-transparent absolute top-0 left-0 w-full h-[50rem] ">
      </div>

      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        className="flex flex-wrap gap-2 justify-center lg:justify-start bg-white p-4 rounded-xl border z-20 "
      >
        {employees?.map((employee: IEmployee, index: number) => {
          const { id, username, email, phone, firstName, positions } = employee;

          const active = Math.floor(Math.random() * 2) % 2 === 0;
          return (
            <EmployeeCard.Container employeeId={id} key={index}>
              <EmployeeCard.Header>
                {active ? <ActiveStatus /> : <PausedStatus />}
              </EmployeeCard.Header>
              <EmployeeCard.Photo name={firstName} positions={positions} />
              <EmployeeCard.Informatins
                username={username}
                email={email}
                phone={phone}
              />
            </EmployeeCard.Container>
          );
        })}
      </motion.div>
    </div>
  );
};
