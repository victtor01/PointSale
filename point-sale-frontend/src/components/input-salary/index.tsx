import { CurrencyInputProps } from "react-currency-input-field";
import CurrencyInput from "react-currency-input-field";

interface CustomInputCurrencyProps extends CurrencyInputProps {
  value: string;
  onChangeValue: (value: string | undefined) => void;
}

export const CustomInputCurrency = ({
  value,
  onChangeValue,
  ...props
}: CustomInputCurrencyProps) => {
  return (
    <CurrencyInput
      {...props}
      value={value}
      onValueChange={onChangeValue}
      prefix="R$ "
      className="p-2 outline-none rounded-md shadow"
      placeholder="R$ 2,500.99"
      allowDecimals
      decimalsLimit={2}
      disableAbbreviations
    />
  );
};
