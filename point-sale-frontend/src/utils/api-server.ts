import { _COOKIE_ACCESS_TOKEN, _COOKIE_REFRESH_TOKEN, _COOKIE_STORE_TOKEN } from "@/config/cookies";
import { SERVER_URL } from "@/config/server";
import { cookies } from "next/headers";

interface fetchServerProps {
  method?: "get" | "post" | "put" | null ;
  url: string;
}

export async function fetchServer<T>(props: fetchServerProps): Promise<T | undefined | null> {
  try {
    const { method, url } = props;
    const cookiesStore = await cookies();
    const accessToken = cookiesStore.get(_COOKIE_ACCESS_TOKEN)?.value;
    const refreshToken = cookiesStore.get(_COOKIE_REFRESH_TOKEN)?.value;
    const storeToken = cookiesStore.get(_COOKIE_STORE_TOKEN)?.value;

    const res = await fetch(`${SERVER_URL}/${url}`, {
      credentials: "include",
      method: method || "get",
      headers: {
        Cookie: `${_COOKIE_ACCESS_TOKEN}=${accessToken};${_COOKIE_REFRESH_TOKEN}=${refreshToken};${_COOKIE_STORE_TOKEN}=${storeToken}`,
      },
    });
  
    return await res.json();
  } catch (error) {
    console.log(error)
    return null;
  }
}