"use client";

import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { usePermissions } from "@/hooks/use-permissions";
import { usePositions } from "@/hooks/use-positions";

export default function Positions() {
  const { useAllPositions: getAllPositions } = usePositions();
  const { positions } = getAllPositions();

  const { getAllPermissions } = usePermissions();
  const { permissions } = getAllPermissions();

  return (
    <CenterSection className="flex flex-col gap-2">
      {positions?.map((position) => {
        return (
          <div key={position.id} className="flex flex-col">
            <div className="bg-white p-2 rounded-md border z-10 justify-between">
              <div className={`${fontSaira} text-gray-500 font-semibold`}>
                {position.name}
              </div>

              <div></div>
            </div>

            <div className="p-2 bg-white rounded-b-md border -mt-3 py-[1.5rem] flex flex-col gap-2">
              {permissions?.map((permission, index: number) => {
                const selected =
                  positions?.some((positions) =>
                    positions.permissions.includes(permission.enumName)
                  ) ?? false;

                return (
                  <div key={index} className="w-full flex gap-2 items-center">
                    <button
                      data-selected={!!selected}
                      type="button"
                      className="w-[3rem] overflow-hidden flex justify-start items-center border
                      h-[1.5rem] shadow-inner relative rounded-md bg-gray-200 ring-2 ring-gray-200
                      data-[selected=true]:justify-end 	data-[selected=true]:border-emerald-500 
                      data-[selected=true]:ring-2 data-[selected=true]:ring-emerald-500 
                      data-[selected=true]:bg-green-600"
                    >
                      <div className="absolute w-[0.5rem] rounded-full h-[0.5rem] border left-2"></div>
                      <div className="h-[1.5rem] w-[1.5rem] bg-white rounded-md shadow-xl z-10"></div>
                    </button>

                    <span className={`${fontSaira} text-lg`}>
                      {permission?.name}
                    </span>
                  </div>
                );
              })}
            </div>
          </div>
        );
      })}
    </CenterSection>
  );
}
