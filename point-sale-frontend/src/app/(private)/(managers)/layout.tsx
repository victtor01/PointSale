import { BottomSidebar } from "@/components/bottom-sidebar";
import { MinSidebar } from "@/components/min-sidebar";

interface LayoutManagerProps {
  children: React.ReactNode;
}

export default function LayoutManagers({ children }: LayoutManagerProps) {
  return (
    <main className="flex flex-1 w-full overflow-hidden">
      <MinSidebar />
      <section className="flex w-full relative overflow-auto flex-col bg-gradient-radial from-gray-100 to-white">
        {children}
      </section>
      <BottomSidebar />
    </main>
  );
}
