import { formatToBRL } from "@/utils/formatBRL";
import { motion } from "framer-motion";
import { BiCheck } from "react-icons/bi";
import { FaPen } from "react-icons/fa";
import { IoClose } from "react-icons/io5";

interface PropsProduct {
  data: {
    id: string;
    name: string;
    description?: string | null;
    categories: string[];
    price: number;
  };
}

export const Product = (props: PropsProduct) => {
  const { data } = props;

  const { id, name, description, categories, price } = data;

  return (
    <tr className=" border-b dark:border-gray-700 border-gray-200">
      <td className="px-2 py-2">{name}</td>
      <td
        className="px-2 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white 
        text-ellipsis max-w-[4rem] overflow-hidden"
      >
        {description}
      </td>
      <td className="px-2 py-4">{formatToBRL(price)}</td>
      <td className="px-2 py-4">
        {categories.length ? categories?.map((cat) => cat) : "-"}
      </td>
      <td className="px-2 py-4">
        <motion.button
          onClick={() => null}
          data-selected={false}
          type="button"
          className="min-w-[3rem] overflow-hidden grid items-center relative
          h-[1.5rem] rounded-full bg-gray-200 ring-gray-200
          ring-4 opacity-90 data-[selected=true]:opacity-100
          data-[selected=true]:ring-indigo-500 data-[selected=true]:bg-indigo-500"
        >
          <motion.div
            layout
            data-selected={false}
            animate={{ x: false ? "100%" : "0" }}
            className="h-[1.5rem] w-[1.5rem] bg-white rounded-full z-10 data-[selected=true]:text-indigo-500 opacity-90 text-gray-300 flex absolute items-center justify-center"
          >
            {false && <BiCheck size={20} />}
            {true && <IoClose size={20} />}
          </motion.div>
        </motion.button>
      </td>
      <td className="px-2 py-0 text-center">
        <button className="w-8 h-8 bg-indigo-50/50 rounded-md grid place-items-center text-indigo-700/40">
          <FaPen />
        </button>
      </td>
    </tr>
  );
};
