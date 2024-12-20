import { twMerge } from "tailwind-merge";
import { callbackify } from "util";

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
  callback: ({ x, y }: Position) => any;
} & ContainerMainProps;

type CanvasContainerProps = {} & ContainerMainProps;

const useCanvasDiv = () => {
  const makeDraggable = (
    e: React.MouseEvent,
    callback: ({ x, y }: Position) => any
  ) => {
    const target = e.currentTarget as HTMLElement;
    let offsetX = e.clientX - target.offsetLeft;
    let offsetY = e.clientY - target.offsetTop;

    const onMouseMove = (moveEvent: MouseEvent) => {
      let newX = moveEvent.clientX - offsetX;
      let newY = moveEvent.clientY - offsetY;

      const canvas = window.document.getElementById("container-canvas");

      if (canvas) {
        newX = Math.max(0, Math.min(newX, canvas.offsetWidth - 40));
        newY = Math.max(0, Math.min(newY, canvas.offsetHeight - 40));
        callback({ x: newX, y: newY });
      }
    };

    const onMouseUp = () => {
      document.removeEventListener("mousemove", onMouseMove);
      document.removeEventListener("mouseup", onMouseUp);
    };

    document.addEventListener("mousemove", onMouseMove);
    document.addEventListener("mouseup", onMouseUp);
  };

  return { makeDraggable };
};

function CanvasDiv(props: CanvasDivProps) {
  const { makeDraggable } = useCanvasDiv();
  const { children, className, top = 1, left = 1, callback } = props;
  const mergeClass = twMerge("w-10 h-10 absolute bg-red-400", className);

  return (
    <div
      onMouseDown={(e) => makeDraggable(e, callback)}
      style={{ top, left }}
      className={mergeClass}
    >
      <div>{children}</div>
    </div>
  );
}

function CanvasContainer(props: CanvasContainerProps) {
  const { children, className } = props;
  const mergeClass = twMerge(
    "w-full relative p-2 h-auto min-h-[10rem] rounded bg-gray-100 border",
    className
  );
  return (
    <div className={mergeClass} id="container-canvas">
      <div>{children}</div>
    </div>
  );
}

const canvas = {
  Container: CanvasContainer,
  Div: CanvasDiv,
};

export { canvas };
