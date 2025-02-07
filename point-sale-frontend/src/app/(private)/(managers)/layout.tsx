import { BottomSidebar } from "@/components/bottom-sidebar";
import { FloatingSidebar } from "@/components/floating-sidebar";
import { MinSidebar } from "@/components/min-sidebar";

interface LayoutManagerProps {
  children: React.ReactNode;
}

export default function LayoutManagers({ children }: LayoutManagerProps) {
  return (
    <main className="flex h-screen w-full overflow-auto">
      <MinSidebar />
      <BottomSidebar/>

      <section className="flex flex-1 bg-gradient-radial from-violet-50 to-white overflow-auto">
        {children}
      </section>
    </main>
  );
}
