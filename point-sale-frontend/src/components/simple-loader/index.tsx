export function SimpleLoader() {
  return (
    <div className="flex gap-2 items-center">
      <span
        className={`animate-bounce delay-[0] w-[1.5rem] h-[1.5rem] rounded-full bg-indigo-500 shadow-xl shadow-gray-300`}
      />
      <span
        className={`animate-bounce delay-75 w-[1.5rem] h-[1.5rem] rounded-full bg-indigo-500 shadow-xl shadow-gray-300`}
      />
      <span
        className={`animate-bounce delay-100 w-[1.5rem] h-[1.5rem] rounded-full bg-indigo-500 shadow-xl shadow-gray-300`}
      />
    </div>
  );
}
