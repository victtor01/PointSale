import { fetchServer } from "@/utils/api-server";

interface TemplateProps {
  params: Promise<{ storeId: string }>
  children: Promise<React.ReactNode>
}

async function TemplateSelectStoreWithId(props: TemplateProps) {
  const params = await props?.params;
  const children = await props?.children;

  return (
    <div className="flex bg-blue-50 h-screen w-full ">
      {children}
    </div>
  )
}

export default TemplateSelectStoreWithId;