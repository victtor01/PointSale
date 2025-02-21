export function formatToBRL(value: number): string {
	const valueNumber = value || 0;
	return valueNumber.toLocaleString("pt-BR", {
			style: "currency",
			currency: "BRL",
	});
}
