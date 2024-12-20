"use client";

import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import { FaPlay } from "react-icons/fa";
import { canvas } from "@/components/canvas-tables";
import { useState } from "react";

function Tables() {
  const [divs, setDivs] = useState([
    { positionX: 0, positionY: 0, number: 1 },
    { positionX: 60, positionY: 60, number: 2 },
  ]);

  console.log(divs);

  return (
    <CenterSection>
      <header className="flex w-full justify-between text-gray-600 dark:text-gray-200">
        <div className="font-semibold text-lg">
          <div className="flex gap-2 items-center">
            <FaPlay size={12} />
            <h1 className={`text-lg font-semibold ${fontSaira}`}>Minha loja</h1>
          </div>
        </div>
      </header>

      <section className="flex w-full relative">
        <canvas.Container>
          {divs?.map((div, index) => {
            return (
              <canvas.Div
                key={index}
                top={div.positionY}
                left={div.positionX}
                callback={({ x, y }) => {
                  console.log(index, x, y);

                  setDivs((prev) => {
                    // Cria uma cópia do array anterior de divs
                    const updatedDivs = [...prev];

                    // Atualiza a div no índice correto
                    updatedDivs[index] = {
                      ...updatedDivs[index], // Mantém os valores antigos
                      positionX: x, // Atualiza a posição X
                      positionY: y, // Atualiza a posição Y
                    };

                    // Retorna o novo array com a div atualizada
                    return updatedDivs;
                  });
                }}
              />
            );
          })}
        </canvas.Container>
      </section>
    </CenterSection>
  );
}

export default Tables;
