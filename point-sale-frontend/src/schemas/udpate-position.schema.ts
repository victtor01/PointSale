import { z } from "zod";

export type UpdatePositionData = z.infer<typeof UpdatePositionSchema>;

export const UpdatePositionSchema = z.object({
  name: z.string().min(1),
  permissions: z.array(z.string()),
});
