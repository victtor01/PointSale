import { fontOpenSans, fontSaira } from "@/fonts";
import Link from "next/link";
import { BsFillBoxFill } from "react-icons/bs";
import { FaUsers } from "react-icons/fa";

type DashboardEmployeeProps = {
  quantityOf?: number;
};

type DashboardProductsProps = {
  quantityOf?: number;
};

const DashboardEmployee = (props: DashboardEmployeeProps) => {
  const { quantityOf = 0 } = props;

  return (
    <Link
      href="/employee"
      className="bg-white flex  hover:bg-gray-50 items-center justify-between p-3 gap-1 border rounded-xl w-full max-w-[50%]"
    >
      <div className="flex flex-col">
        <header className="w-full flex items-center justify-between gap-2">
          <div className="flex items-center gap-2 text-gray-500">
            <FaUsers />
            <h1 className={`${fontSaira} font-semibold text-md`}>
              Funcionários
            </h1>
          </div>
        </header>

        <section className="flex justify-between items-center">
          <div className="flex items-center gap-2 font-semibold">
            <h1 className={`${fontSaira} font-semibold text-[2rem]`}>
              {quantityOf}
            </h1>
            <p className={`${fontOpenSans} mb-1`}>Ativos</p>
          </div>
        </section>
      </div>
      <div className="w-10 h-10 grid mx-3 place-items-center bg-gray-50 rounded-full text-gray-300 ">
        <FaUsers />
      </div>
    </Link>
  );
};

const DashboardProducts = (props: DashboardProductsProps) => {
  const { quantityOf = 0 } = props;
  return (
    <Link
      href="/products"
      className="bg-white flex items-center hover:bg-gray-50 justify-between p-3 gap-1 border rounded-xl w-full max-w-[50%]"
    >
      <div className="flex flex-col">
        <header className="w-full flex items-center justify-between gap-2">
          <div className="flex items-center gap-2 text-gray-500">
            <BsFillBoxFill />
            <h1 className={`${fontSaira} font-semibold text-md`}>Produtos</h1>
          </div>
        </header>

        <section className="flex justify-between items-center">
          <div className="flex items-center gap-2 font-semibold">
            <h1 className={`${fontSaira} font-semibold text-[2rem]`}>
              {quantityOf}
            </h1>
            <p className={`${fontOpenSans} mb-1`}>Produtos cadastrados</p>
          </div>
        </section>
      </div>
      <div className="w-10 h-10 mx-3 grid place-items-center bg-gray-50 rounded-full text-gray-300 ">
        <BsFillBoxFill />
      </div>
    </Link>
  );
};

const dashboards = {
  employees: DashboardEmployee,
  products: DashboardProducts,
};

export { dashboards };
