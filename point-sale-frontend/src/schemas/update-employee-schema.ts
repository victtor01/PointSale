import { z } from "zod";

export const updateEmployeeSchema = z.object({
  firstName: z
    .string({ message: "O primeiro nome deve ser texto" })
    .min(1, { message: "O primeiro nome é obrigatório" }),
  salary: z
    .number({ message: "O salário do funcionário não é válido" })
    .min(1, { message: "O Salário do funcionário é obrigatório" })
    .refine((e) => Number(e)),
  username: z.number().optional(),
  lastName: z.string().optional(),
  email: z.string().optional().optional(),
  phone: z.string().optional(),
  password: z.string().optional(),
  positions: z.array(z.string().uuid()).optional(),
});

type SchemaUpdateEmployee = z.infer<typeof updateEmployeeSchema>;

export { type SchemaUpdateEmployee };
