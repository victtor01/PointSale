import {
  Fira_Code,
  Inter,
  Open_Sans,
  Roboto,
  Saira,
  Varela_Round,
} from "next/font/google";

const roboto = Roboto({
  subsets: ["latin"],
  weight: ["400", "500", "900"],
});

const openSans = Open_Sans({
  subsets: ["latin"],
  weight: ["400", "600", "300", "500", "700", "800"],
});

const inter = Inter({
  subsets: ["latin"],
  weight: ["500", "700"],
});

const valela = Varela_Round({
  subsets: ["latin"],
  weight: ["400"],
});

const saira = Saira({
  subsets: ["latin"],
  weight: ["400", "500", "600", "700"],
});


const firaCode = Fira_Code({
  subsets: ["latin"],
  weight: ["300", "400", "500", "600", "700"],
});

export const fontSaira = saira.className;
export const fontRoboto = roboto.className;
export const fontOpenSans = openSans.className;
export const fontInter = inter.className;
export const fontValela = valela.className;
export const fontFiraCode = firaCode.className;
