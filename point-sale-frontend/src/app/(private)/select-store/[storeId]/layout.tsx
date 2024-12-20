import { fetchServer } from "@/utils/api-server";

interface TemplateProps {
  params: Promise<{ storeId: string }>
  children: Promise<React.ReactNode>
}

async function TemplateSelectStoreWithId(props: TemplateProps) {
  const children = await props?.children;

  return (
    <div className="flex bg-blue-50 h-screen w-full p-6">
      {children}
    </div>
  )
}

export default TemplateSelectStoreWithId;