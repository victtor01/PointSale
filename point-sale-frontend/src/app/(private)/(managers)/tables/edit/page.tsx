"use client";
import { canvas } from "@/components/canvas-tables";
import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { useState } from "react";

function EditTables() {
  const [divs, setDivs] = useState([
    { positionX: 0, positionY: 0, number: 34 },
    { positionX: 60, positionY: 60, number: 12 },
  ]);

  return (
    <CenterSection>
      <section className="flex w-full relative">
        <canvas.Container>
          {divs?.map((div, index) => {
            return (
              <canvas.Div
                key={index}
                top={div.positionY}
                left={div.positionX}
                className="cursor-move"
                callback={({ x, y }) => {
                  setDivs((prev) => {
                    const updatedDivs = [...prev];

                    updatedDivs[index] = {
                      ...updatedDivs[index],
                      positionX: x,
                      positionY: y,
                    };

                    return updatedDivs;
                  });
                }}
              >
                <span
                  className={`${fontSaira} pointer-events-none select-none font-semibold`}
                >
                  {div.number}
                </span>
              </canvas.Div>
            );
          })}
        </canvas.Container>
      </section>
    </CenterSection>
  );
}

export default EditTables;
