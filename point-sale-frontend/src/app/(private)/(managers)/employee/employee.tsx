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
import { useState } from "react";
import { FaAngleDown } from "react-icons/fa";

export const Employees = () => {
  const { getAllEmployees } = useEmployee();
  const { employees } = getAllEmployees();

  const [open, setOpen] = useState<boolean>(true);

  return (
    <div className="flex w-full mt-2 flex-col gap-2 relative">
      <header className="font-semibold flex justify-between items-center bg-white border p-2 rounded-lg border-b-4 z-20">
        <h1 className={fontSaira}>Funcionários</h1>
        <div className="flex items-center gap-5">
          <div className="p-1 px-3 bg-gray-100 text-sm rounded-md ">21/20</div>

          <button onClick={() => setOpen((prev) => !prev)} className="w-6 h-6 ">
            <FaAngleDown />
          </button>
        </div>
      </header>

      {open && (
        <>
          <motion.div
            initial={{ height: 0 }}
            animate={{ height: "10rem" }}
            className="absolute w-[0.5rem] border-l-[0.4rem] border-dotted left-10 top-10"
          />
          <motion.div
            initial={{ height: 0 }}
            animate={{ height: "10rem" }}
            transition={{ type: "tween" }}
            className="absolute w-[0.5rem] border-r-[0.4rem] border-dotted right-10 top-10"
          />

          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            className="flex flex-wrap gap-2 justify-center lg:justify-start items-center bg-white p-4 rounded-xl border z-20 mt-[2rem]"
          >
            {employees?.map((employee: IEmployee, index: number) => {
              const { id, username, email, phone, firstName, positions } =
                employee;
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
        </>
      )}
    </div>
  );
};
