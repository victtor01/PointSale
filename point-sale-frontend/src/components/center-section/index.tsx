import { HTMLAttributes } from "react";
import { twMerge } from "tailwind-merge";

type CenterSectionProps = {
  children: React.ReactNode;
} & HTMLAttributes<HTMLDivElement>;

function CenterSection({ children, ...props }: CenterSectionProps) {
  const className = twMerge(
    "flex mx-auto flex-col h-auto w-full max-w-[55rem] p-2 px-4 pb-[5rem] z-10",
    props.className
  );
  
  return <section className={className}>{children}</section>;
}

export { CenterSection };
