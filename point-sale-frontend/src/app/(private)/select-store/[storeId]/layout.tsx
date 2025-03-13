
interface TemplateProps {
  children: React.ReactNode
}

export default async function Layout(props: TemplateProps) {
  const children = props?.children;

  return (
    <div className="flex bg-blue-50 h-screen w-full p-6">
      {children}
    </div>
  )
}