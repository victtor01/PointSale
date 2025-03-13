"use client";

import { fontSaira } from "@/fonts";
import { Bar, BarChart, ResponsiveContainer, Tooltip, TooltipProps, XAxis } from "recharts";

import { ChartConfig, ChartContainer } from "@/components/ui/chart";

const chartData = [
  { month: "janeiro", desktop: 30 },
  { month: "fevereiro", desktop: 10 },
  { month: "março", desktop: 3 },
  { month: "abril", desktop: 0 },
  { month: "maio", desktop: 0 },
  { month: "junho", desktop: 0 },
  { month: "julho", desktop: 0 },
  { month: "agosto", desktop: 0 },
  { month: "setembro", desktop: 0 },
  { month: "outubro", desktop: 0 },
  { month: "novembro", desktop: 0 },
  { month: "dezembro", desktop: 0 },
];

const chartConfig = {
  desktop: {
    label: "Ordens",
    color: "",
  },
} satisfies ChartConfig;

const CustomTooltip = ({ active, payload, label }: TooltipProps<number, string>) => {
  if (active && payload && payload.length) {
    return (
      <div className="bg-white p-2 border rounded-lg">
        <p
          className={`${fontSaira} text-gray-500 dark:text-gray-200 font-semibold`}
        >{`${label} : ${payload[0].value} ordens`}</p>
      </div>
    );
  }

  return null;
};

export function SimpleDashboard() {
  return (
    <section className="flex flex-col gap-2 mt-8">
      <header className="flex items-center px-1 justify-between flex-wrap gap-1">
        <div className="text-lg font-semibold text-gray-600">
          <h1 className={fontSaira}>Quantidade de ordens</h1>
        </div>
        <div className="flex items-center gap-2 rounded-md">
          <button className="bg-white p-1 px-2 shadow rounded-md text-gray-600 text-sm">
            <span className={fontSaira}>Todos os meses</span>
          </button>
          <button className="bg-gray-200 p-1 px-2 rounded-md text-gray-600 text-sm">
            <span className={fontSaira}>Essa semana</span>
          </button>
        </div>
      </header>
      <div className="h-[10rem] relative bg-white w-full p-1 border rounded-xl overflow-hidden">
        <ResponsiveContainer width="100%" height={"100%"} aspect={2}>
          <ChartContainer config={chartConfig}>
            <BarChart data={chartData}>
              <XAxis
                dataKey="month"
                tickLine={false}
                tickMargin={10}
                axisLine={false}
                tickFormatter={(value) => value.slice(0, 3)}
              />
              <Tooltip content={<CustomTooltip />} cursor={false} />
              <Bar
                dataKey="desktop"
                className="fill-gray-700"
                radius={8}
                barSize={40}
              />
            </BarChart>
          </ChartContainer>
        </ResponsiveContainer>
      </div>
    </section>
  );
}
