"use client";

import {
  ActiveStatus,
  EmployeeCard,
  PausedStatus,
} from "@/components/employee-card";
import { fontSaira } from "@/fonts";
import { useEmployee } from "@/hooks/use-employee";
import { IEmployee } from "@/interfaces/IEmployee";
import { motion } from "framer-motion";
import { BiPlus } from "react-icons/bi";

export const Employees = () => {
  const { useGetAllEmployees: getAllEmployees } = useEmployee();
  const { employees } = getAllEmployees();

  return (
    <div className="flex w-full mt-2 flex-col gap-2 relative">
      <header className="font-semibold flex justify-between items-center bg-white border p-4 rounded-lg border-b-4 z-20">
        <div className="flex gap-2 items-center">
          <h1 className={fontSaira}>Funcionários</h1>
        </div>
        <div className="flex items-center gap-5">
          <button className="px-3 py-2 bg-indigo-500/20 rounded-lg opacity-90 hover:opacity-100 shadow-lg shadow-indigo-500/30 flex gap-2 items-center justify-center">
            <BiPlus size={20}/>
            <span>Funcionário</span>
          </button>
        </div>
      </header>

      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        className="flex flex-wrap gap-2 justify-center lg:justify-start bg-white p-4 rounded-xl border-2 border-b-4 border-gray-400/20 z-20 "
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
