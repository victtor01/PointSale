import { CurrencyInputProps } from "react-currency-input-field";
import CurrencyInput from "react-currency-input-field";
import { twMerge } from "tailwind-merge";

interface CustomInputCurrencyProps extends CurrencyInputProps {
  value: string;
  onChangeValue: (value: string | undefined) => void;
}

export const CustomInputCurrency = ({
  value,
  onChangeValue,
  ...props
}: CustomInputCurrencyProps) => {
  const { className } = props;
  const merge = twMerge("p-2 outline-none rounded-md", className);

  return (
    <CurrencyInput
      {...props}
      value={value}
      onValueChange={onChangeValue}
      className={merge}
      placeholder="R$ 2,500.99"
      allowDecimals
      decimalsLimit={2}
      disableAbbreviations
    />
  );
};
