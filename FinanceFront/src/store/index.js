import Vuex from "vuex"
import { getCurrentInstance } from 'vue'
import { getCategories, deleteCategory, addCategory, editCategory } from '../api/category.js'
import { getExpenses, deleteExpense, addExpense, editExpense } from '../api/expense.js'
import { getTransactions, deleteTransaction, addTransaction, editTransaction } from '../api/transaction.js'

//const { proxy } = getCurrentInstance();

const createStore = () => {
	return new Vuex.Store({
		state: {
			categories: [],
			expenses: [],
			transactions: [],
			categoryFilter: {},
			expenseFilter: {},
			transactionFilter: {}
		},
		getters: {
			categories: state => { return state.categories }
		},
		mutations: {
			setEntity: (state, { entity, entitiesName }) => {
				state[entitiesName][state[entitiesName].findIndex(e => e.id == entity.id)]
					= entity;
			},

			setCategories: (state, categories) => {
				state.categories = categories;
			},

			setExpenses: (state, expenses) => {
				state.expenses = expenses;
			},

			setTransactions: (state, transactions) => {
				const groups = transactions.reduce((acc, transaction) => {
					//const date = transaction.date.split('T')[0]; 
					const date = transaction.date;

					if (!acc[date]) {
						acc[date] = [];
					}
					acc[date].push(transaction);
						return acc;
				}, {});

				state.transactions = Object.keys(groups).map(date => ({
					date: date,
					items: groups[date],
					price: groups[date].reduce((accumulator, current) => accumulator + current.price, 0)
				}));
			},

			setCategoryFilter: (state, filter) => {
				state.categoryFilter = filter;
			},

			setExpenseFilter: (state, filter) => {
				state.expenseFilter = filter;
			},

			setTransactionFilter: (state, filter) => {
				state.transactionFilter = filter;
			},

			deleteEntity: (state, { entities, id }) => {
				state[entities] = state[entities].filter(e => e.id != id);
			},

			deleteTransaction: (state, id) => {
				state.transactions.forEach(element => {
					element.items = element.items.filter(e => e.id != id);
				});
				state.transactions = state.transactions.filter(e => e.items.length != 0);
			}
		},
		actions: {
			fetchCategories: async ({ commit, state }) => {
				const response = await getCategories(state.categoryFilter);
				commit('setCategories', response.data);
			},

			fetchExpenses: async ({ commit, state }) => {
				const response = await getExpenses(state.expenseFilter);
				commit('setExpenses', response.data);
			},

			fetchTransactions: async ({ commit, state }) => {
				const response = await getTransactions(state.transactionFilter);
				commit('setTransactions', response.data);
			},

			setTransactionFilter: ({ commit, dispatch }, filter) => {
				commit("setTransactionFilter", filter);
				dispatch("fetchTransactions");
			},

			deleteCategory: async ({ commit, dispatch }, id) => {
				const response = await deleteCategory(id);
				commit("deleteEntity", { entities: "categories", id: id});
				await dispatch("fetchExpenses");
				await dispatch("fetchTransactions");
			},

			deleteExpense: async ({ commit, dispatch }, id) => {
				const response = await deleteExpense(id);
				commit("deleteEntity", { entities: "expenses", id: id});
				await dispatch("fetchTransactions");
			},

			deleteTransaction: async ({ commit }, id) => {
				const response = await deleteTransaction(id);
				commit("deleteTransaction", id);
			},

			addCategory: async ({ dispatch }, category) => {
				const response = await addCategory(category);
				dispatch("fetchCategories");
			},

			addExpense: async ({ dispatch }, expense) => {
				const response = await addExpense(expense);
				dispatch("fetchExpenses");
			},

			addTransaction: async ({ dispatch }, transaction) => {
				const response = await addTransaction(transaction);
				dispatch("fetchTransactions");
				console.log(response);
			},

			editCategory: async ({ dispatch }, category) => {
				const response = await editCategory(category);
				await dispatch("fetchTransactions");
			},

			editExpense: async ({ dispatch }, expense) => {
				const response = await editExpense(expense);
				//commit("setEntity", { entity: expense, entitiesName: "expenses" });
				await dispatch("fetchTransactions");
			},

			editTransaction: async ({ dispatch }, transaction) => {
				const response = await editTransaction(transaction);
				dispatch("fetchTransactions");
			}
		}
	})
}


export default createStore