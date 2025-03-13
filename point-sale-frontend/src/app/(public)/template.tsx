import { _COOKIE_ACCESS_TOKEN, _COOKIE_REFRESH_TOKEN } from "@/config/cookies";
import { fetchServer } from "@/utils/api-server";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";

interface PublicLayoutProps {
  children: Promise<React.ReactNode>;
}

interface FetchServerLoginProps {
  email?: string
}

export default async function PublicLayout({ children }: PublicLayoutProps) {
  const cookiesStore = await cookies();
  const accessToken = cookiesStore.get(_COOKIE_ACCESS_TOKEN)?.value;
  const refreshToken = cookiesStore.get(_COOKIE_REFRESH_TOKEN)?.value;

  if (!accessToken || !refreshToken) return await children;

  const res = await fetchServer<FetchServerLoginProps>({ url: "managers/i" });
  if (res?.email) return redirect("/select-store");
  
  return await children;
}
