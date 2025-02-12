import { BottomSidebar } from "@/components/bottom-sidebar";
import { FloatingSidebar } from "@/components/floating-sidebar";
import { MinSidebar } from "@/components/min-sidebar";

interface LayoutManagerProps {
  children: React.ReactNode;
}

export default function LayoutManagers({ children }: LayoutManagerProps) {
  return (
    <main className="flex h-auto w-full">
      <MinSidebar />
      <BottomSidebar/>

      <section className="flex w-full mb-[4rem] h-auto bg-gradient-radial from-purple-50 to-white">
        {children}
      </section>
    </main>
  );
}
