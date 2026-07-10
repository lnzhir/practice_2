import { rest } from './index.js';





export async function getCategories(filter) {
	return await rest.get("/Category", { params: filter });
}

export async function addCategory(category) {
	return await rest.post("/Category", category);
}

export async function editCategory(category) {
	return await rest.put("/Category", category);
}

export async function deleteCategory(id) {
	return await rest.delete(`/Category/${id}`);
}