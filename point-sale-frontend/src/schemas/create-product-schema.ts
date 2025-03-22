import { z } from "zod";

export type ProductsPropsSchema = z.infer<typeof productSchema>;

const productSchema = z.object({
  name: z.string().min(1),
  price: z
    .string()
    .transform((val) => parseFloat(val))
    .refine((val) => val > 0, {
      message: "Price must be a positive number",
    }),
  description: z.string().optional(),
  quantity: z.number().int().nonnegative().optional(),
});

export { productSchema };
