import { fontInter, fontRoboto, fontSaira } from "@/fonts";
import { IPositionEmployee } from "@/interfaces/IEmployee";
import Link from "next/link";
import { BsThreeDots } from "react-icons/bs";
import { FaHashtag, FaPause } from "react-icons/fa";
import { GoDotFill } from "react-icons/go";
import { MdEmail } from "react-icons/md";
import { twMerge } from "tailwind-merge";

interface BaseProps {
  children: React.ReactNode;
}

interface ContainerProps extends BaseProps {
  employeeId: string;
  className?: string;
}

interface PhotoEmployeeCardProps {
  name: string;
  positions?: IPositionEmployee[];
}

interface InformationsEmployeeCardsProps {
  username: string;
  email?: string;
  phone?: string;
}

export const ActiveStatus = () => (
  <div className="bg-emerald-500 text-white p-1 px-3 flex gap-2 items-center text-sm rounded-full">
    <GoDotFill />
    <span>Ativo</span>
  </div>
);

export const PausedStatus = () => (
  <div className="bg-gray-200 text-gray-500 p-1 px-3 flex gap-2 items-center text-sm rounded-full">
    <FaPause size={10} />
    <span>Pausado</span>
  </div>
);

const HeaderEmployeeCard = ({ children }: BaseProps) => (
  <header className="flex justify-between w-full items-center">
    {children}
    <button className="p-1 px-2">
      <BsThreeDots size={15} />
    </button>
  </header>
);

const PhotoEmployeeCard = ({ name, positions }: PhotoEmployeeCardProps) => (
  <section className="flex flex-col gap-0 mt-4 items-center">
    <div className="w-14 h-14 bg-gray-200 rounded-full shadow-inner" />
    <div className="flex flex-col gap-[-1rem] items-center mt-3">
      <h1 className={`${fontSaira} text-gray-500 font-semibold text-lg`}>
        {name || "~"}
      </h1>
      <span
        className={`${fontRoboto} text-gray-500 opacity-70 text-sm flex divide-x-[0.1rem] divide-gray-200`}
      >
        {!positions?.length && "~"}

        {positions?.map((position) => {
          return (
            <div key={position.id} className="px-2">
              {position.name}
            </div>
          );
        })}
      </span>
    </div>
  </section>
);

const InformatinsEmployeeCard = (props: InformationsEmployeeCardsProps) => {
  const { username, email, phone } = props;
  return (
    <section className="w-full flex flex-1 rounded-md bg-gray-100 text-sm border p-2 mt-4 flex-col gap-2">
      <div className="flex gap-2 items-center text-sm">
        <FaHashtag size={14} />
        <span className={fontInter}>{username}</span>
      </div>
      <div className="flex gap-2 w-full relative">
        <MdEmail size={15} className="mt-1 min-w-6" />
        <div className="flex gap-1 flex-wrap w-[90%] relative">
          <span
            className={`${fontRoboto} overflow-hidden text-ellipsis whitespace-nowrap bg-white p-[0.1rem] border px-3 text-gray-400 rounded-full`}
          >
            {email || "~"}
          </span>
          <span
            className={`${fontRoboto} overflow-hidden text-ellipsis whitespace-nowrap p-[0.1rem] bg-white border px-3 text-gray-400 rounded-full`}
          >
            {phone || "~"}
          </span>
        </div>
      </div>
    </section>
  );
};

const EmployeeComponent = ({
  children,
  employeeId,
  ...props
}: ContainerProps) => {
  const classStyle = twMerge(
    "flex p-5 opacity-90 hover:opacity-100 hover:shadow-xl flex-1 w-full max-w-[18rem] min-w-[15rem] border-b-4 rounded-xl items-center flex-col border bg-white",
    props.className
  );

  return (
    <Link href={`/employee/${employeeId}`} className={classStyle}>
      {children}
      <footer className="text-sm mt-5 w-full flex opacity-70">
        Criado 24 de set, 2024
      </footer>
    </Link>
  );
};

const EmployeeCard = {
  Container: EmployeeComponent,
  Header: HeaderEmployeeCard,
  Photo: PhotoEmployeeCard,
  Informatins: InformatinsEmployeeCard,
};

export { EmployeeCard };
