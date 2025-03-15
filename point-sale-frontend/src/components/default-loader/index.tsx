import { HtmlHTMLAttributes } from "react";
import styles from "./default-loader.module.css";
import { twMerge } from "tailwind-merge";

export function DefaultLoader(props: HtmlHTMLAttributes<HTMLDivElement>) {
  const { className } = props;
  const merge = twMerge(styles.loader, className);

  return <span className={merge} />;
}
