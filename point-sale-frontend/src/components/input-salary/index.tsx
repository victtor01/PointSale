import { FieldValues, Path, UseFormRegisterReturn } from "react-hook-form";
import CurrencyInput, { CurrencyInputProps } from "react-currency-input-field";

interface CustomInputCurrencyProps<T extends FieldValues> extends CurrencyInputProps {
  register: UseFormRegisterReturn<Path<T>>;
}

export const CustomInputCurrency = <T extends FieldValues>({
  register,
  ...props
}: CustomInputCurrencyProps<T>) => {
  return (
    <CurrencyInput
      {...props}
      {...register} 
      prefix="R$ "
						className="p-2 outline-none rounded-md shadow"
						placeholder="R$ 2,500.99"
      allowDecimals
      decimalsLimit={2}
      disableAbbreviations
    />
  );
};
