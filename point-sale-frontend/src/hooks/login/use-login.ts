import { api } from "@/utils/api";

const useLogin = () => {
  const submitLogin = async (): Promise<void> => {
    const body = { email: "example@gmail.com", password: "example" };
    const res = await api.post("/auth", body);

    console.log(res);
  };

  return {
    submitLogin,
  };
};

export { useLogin };

