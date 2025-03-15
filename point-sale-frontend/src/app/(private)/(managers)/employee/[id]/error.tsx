"use client";

import { fontSaira } from "@/fonts";

interface ErrorProps {
  error: Error & { digest?: string };
  reset: () => void;
}

export default function Error({ error, reset }: ErrorProps) {
  return (
    <section className="w-full h-auto grid place-items-center font-semibold py-10">
      <h1 className={`${fontSaira} text-gray-600 text-xl`}>{error.message}</h1>

      <button
        type="button"
        onClick={() => reset()}
        className="py-2 text-md px-4 bg-gray-800 rounded-md text-white mt-4"
      >
        Tentar novamente!
      </button>
    </section>
  );
}
