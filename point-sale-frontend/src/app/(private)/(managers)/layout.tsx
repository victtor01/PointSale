import { BottomSidebar } from "@/components/bottom-sidebar";
import { MinSidebar } from "@/components/min-sidebar";

interface LayoutManagerProps {
  children: React.ReactNode;
}

export default function LayoutManagers({ children }: LayoutManagerProps) {
  return (
    <main className="flex h-[100vh] w-full overflow-hidden">
      <MinSidebar />
      <BottomSidebar />
      <section className="flex w-full relative h-[100vh] overflow-auto flex-col pb-[5rem] bg-gradient-radial from-gray-100 to-white">
        {children}
      </section>
    </main>
  );
}
