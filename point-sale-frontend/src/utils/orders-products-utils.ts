import { OrderProductStatusColors } from "@/interfaces/IOrderProducts";

const getColorByStatus = (status: string) => {
	return OrderProductStatusColors[status]
};

export { getColorByStatus };
