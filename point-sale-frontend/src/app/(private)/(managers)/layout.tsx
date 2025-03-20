"use client";

import { BottomSidebar } from "@/components/bottom-sidebar";
import { MinSidebar } from "@/components/min-sidebar";
import { AnimatePresence, motion } from "framer-motion";
import { useSearchParams } from "next/navigation";
import { EditPosition } from "./edit-position-modal";

interface LayoutManagerProps {
  children: React.ReactNode;
}

export type ModalType = keyof typeof MODALS;

const MODALS = {
  "edit-position": EditPosition,
};

export default function LayoutManagers({ children }: LayoutManagerProps) {
  const params = useSearchParams();
  const modalType = params.get("modal") as ModalType;

  const ModalComponent =
    modalType && MODALS.hasOwnProperty(modalType) ? MODALS[modalType] : null;

  return (
    <AnimatePresence>
      {ModalComponent && <EditPosition params={params} key={modalType} />}

      {!ModalComponent && (
        <motion.main
          key="main"
          transition={{ type: "keyframes", duration: 0.2 }}
          initial={{ scale: 0.8, opacity: 0 }}
          animate={{ scale: 1, opacity: 1 }}
          exit={{ scale: 0.8, opacity: 0 }}
          className="flex flex-1 w-full overflow-hidden lg:py-2 lg:pr-3 relative"
        >
          <MinSidebar />
          <section
            className="flex w-full relative overflow-auto lg:rounded-xl lg:border 
            flex-col bg-gradient-radial from-gray-50 to-gray-50"
          >
            {children}
          </section>
          <BottomSidebar />
        </motion.main>
      )}
    </AnimatePresence>
  );
}
