type Position = {
  x: number;
  y: number;
};

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
      if (!canvas) return;

      const isOverlapping = checkOverlapWithOtherDivs(target, canvas);
      if (isOverlapping) {};

      newX = Math.max(0, Math.min(newX, canvas.offsetWidth - 40));
      newY = Math.max(0, Math.min(newY, canvas.offsetHeight - 40));
      callback({ x: newX, y: newY });
    };

    const onMouseUp = (moveEvent: MouseEvent) => {
      let newX = moveEvent.clientX - offsetX;
      console.log(newX);

      document.removeEventListener("mousemove", onMouseMove);
      document.removeEventListener("mouseup", onMouseUp);
    };

    const checkOverlapWithOtherDivs = (
      target: HTMLElement,
      container: HTMLElement
    ) => {
      const otherDivs = Array.from(container.children).filter(
        (child) => child !== target
      ) as HTMLElement[];

      for (const otherDiv of otherDivs) {
        const isOverlapping = checkOverlap(target, otherDiv);
        if (isOverlapping) {
          return true;
        }
      }

      return false;
    };

    const checkOverlap = (div1: HTMLElement, div2: HTMLElement) => {
      const rect1 = div1.getBoundingClientRect();
      const rect2 = div2.getBoundingClientRect();

      const isOverlapping =
        rect1.left < rect2.right &&
        rect1.right > rect2.left &&
        rect1.top < rect2.bottom &&
        rect1.bottom > rect2.top;

      return isOverlapping;
    };

    document.addEventListener("mousemove", onMouseMove);
    document.addEventListener("mouseup", onMouseUp);
  };

  return { makeDraggable };
};

export { useCanvasDiv };

