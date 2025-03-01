"use client";

import { CenterSection } from "@/components/center-section";
import { fontSaira } from "@/fonts";
import Link from "next/link";
import { BsPlus } from "react-icons/bs";
import { IoMdAlert } from "react-icons/io";
import { TrendingUp } from "lucide-react";
import { Bar, BarChart, XAxis, ResponsiveContainer, Tooltip } from "recharts";

import { Card, CardContent, CardFooter } from "@/components/ui/card";

import {
  ChartConfig,
  ChartContainer,
  ChartTooltip,
  ChartTooltipContent,
} from "@/components/ui/chart";

interface LayoutOrdersProps {
  children: React.ReactNode;
}

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

const CustomTooltip = ({ active, payload, label }: any) => {
	if (active && payload && payload.length) {
			return (
					<div className="bg-white p-2 border rounded-lg">
							<p className={`${fontSaira} text-gray-500 dark:text-gray-200 font-semibold`}>{`${label} : ${payload[0].value} ordens`}</p>
					</div>
			);
	}
	return null;
};


export function SimpleDashboard() {
  return (
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
          <Bar dataKey="desktop" className="fill-gray-700" radius={8} />
        </BarChart>
      </ChartContainer>
    </ResponsiveContainer>
  );
}
