import { z } from "zod";

export const UpdateEmployeeSchema = z.object({
	firstName: z.string(),
	username: z.number(),
	lastName: z.string().min(0),
	email: z.string().email(),
	phone: z.string(),
	salary: z.number(),
	positions: z.array(z.string().uuid())
})

// string FirstName,
// string LastName,
// string Email,
// string Phone,
// decimal Salary,
// List<Guid> Positions