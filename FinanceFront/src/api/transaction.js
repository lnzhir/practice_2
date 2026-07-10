import { rest } from './index.js';


export async function getTransactions(filter) {
	return await rest.get("/Transaction", { params: filter });
}

export async function addTransaction(transaction) {
	return await rest.post("/Transaction", transaction);
}

export async function editTransaction(transaction) {
	return await rest.put("/Transaction", transaction);
}

export async function deleteTransaction(id) {
	return await rest.delete(`/Transaction/${id}`);
}