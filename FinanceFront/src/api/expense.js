import { rest } from './index.js';




export async function getExpenses(filter) {
	return await rest.get("/Expense", { params: filter });
}

export async function addExpense(expense) {
	return await rest.post("/Expense", expense);
}

export async function editExpense(expense) {
	return await rest.put("/Expense", expense);
}

export async function deleteExpense(id) {
	return await rest.delete(`/Expense/${id}`);
}