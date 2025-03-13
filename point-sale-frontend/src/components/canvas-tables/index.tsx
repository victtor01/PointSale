import { useCanvasDiv } from "@/hooks/canvas/useCanvas.ts";
import { motion } from "framer-motion";
import { twMerge } from "tailwind-merge";

type ContainerMainProps = {
  children?: React.ReactNode;
} & React.HtmlHTMLAttributes<HTMLDivElement>;

type Position = {
  x: number;
  y: number;
};

type CanvasDivProps = {
  top?: number;
  left?: number;
  callback: ({ x, y }: Position) => void;
} & ContainerMainProps;

type CanvasContainerProps = {} & ContainerMainProps;

function CanvasDiv(props: CanvasDivProps) {
  const { makeDraggable } = useCanvasDiv();
  const { children, className, top = 1, left = 1, callback } = props;
  const mergeClass = twMerge(
    "w-10 h-10 absolute  bg-white/50 grid place-items-center rounded shadow",
    className
  );

  return (
    <motion.div
      whileTap={{ scale: 1.2, boxShadow: "10px 10px 20px rgba(0,0,0,0.1)" }}
      style={{ top, left }}
      onMouseDown={(e) => makeDraggable(e, callback)}
      className={mergeClass}
    >
      <div>{children}</div>
    </motion.div>
  );
}

function CanvasContainer(props: CanvasContainerProps) {
  const { children, className } = props;
  const mergeClass = twMerge(
    "w-full relative p-2 h-auto min-h-[30rem] rounded bg-gray-100 border",
    className
  );

  return (
    <div className={mergeClass} id="container-canvas">
      {children}
    </div>
  );
}

const canvas = {
  Container: CanvasContainer,
  Div: CanvasDiv,
};

export { canvas };
