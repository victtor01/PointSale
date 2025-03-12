import { HtmlHTMLAttributes } from "react";
import styles from "./default-loader.module.css";
import { twMerge } from "tailwind-merge";

interface DefaultLoaderProps extends HtmlHTMLAttributes<HTMLDivElement> {}

export function DefaultLoader(props: DefaultLoaderProps) {
  const { className } = props;
  const merge = twMerge(styles.loader, className);

  return <span className={merge} />;
}
