
interface LayoutTableProps {
  children: React.ReactNode
}

export default function LayoutTables ({ children }: LayoutTableProps) {
  return (
    <main className="w-full h-screen overflow-hidden relative bg-gradient-to-b from-indigo-50/30 to-transparent">
      {children}
    </main>
  )
}