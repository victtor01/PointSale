import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { IStore } from "@/interfaces/IStore";
import { fetchServer } from "@/utils/api-server";
import { GoPencil } from "react-icons/go";

type BannerProps = {
  id: string;
  title: string;
};

export function Banner(props: BannerProps) {
  const { id, title } = props;
  return (
    <div className="flex w-full justify-center relative mt-2">
      <CenterSection className="px-[1rem] py-0 w-full">
        <div className="p-3 bg-white border rounded-md justify-between flex items-center">
          <div className="flex gap-2 items-center">
            <div className="flex w-7 h-7 bg-white rounded-md border"></div>
            <span
              className={`${fontSaira} font-semibold text-gray-600 dark:text-gray-100 text-lg`}
            >
              {title}
            </span>
          </div>

          <button className="bg-gray-100 w-7 h-7 rounded grid place-items-center">
            <GoPencil />
          </button>
        </div>
      </CenterSection>
    </div>
  );
}
