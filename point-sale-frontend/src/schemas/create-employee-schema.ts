import { z } from "zod";

export const createEmployeeSchema = z.object({
  firstName: z
    .string({ message: "O primeiro nome deve ser texto" })
    .min(1, { message: "O primeiro nome é obrigatório" }),
  salary: z
    .string({ message: "O salário do funcionário não é válido" })
    .min(1, { message: "O Salário do funcionário é obrigatório" }),
  password: z.string().min(1, { message: "A senha é obrigatória" }),
  lastName: z.string().optional(),
  email: z.string().optional().optional(),
  phone: z.string().optional(),
  positions: z.array(z.string().uuid()).optional(),
});

type CreateEmployeeSchemaProps = z.infer<typeof createEmployeeSchema>;

export { type CreateEmployeeSchemaProps };
