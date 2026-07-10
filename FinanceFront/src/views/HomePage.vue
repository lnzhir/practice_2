<script setup>
import { ref, onMounted, provide } from 'vue'
import { useStore } from 'vuex'
import { editCategory } from '../api/category.js'
import { editExpense } from '../api/expense.js'
import viteLogo from '../assets/vite.svg'
import heroImg from '../assets/hero.png'
import vueLogo from '../assets/vue.svg'
import Card from '../components/Card.vue'
import TransactionCard from '../components/TransactionCard.vue'
import FilterButton from '../components/FilterButton.vue'
import DateLabel from '../components/DateLabel.vue'
import AddButton from '../components/AddButton.vue'
import Dialog from '../layouts/Dialog.vue'
import CategoryDialog from '../components/CategoryDialog.vue'
import ExpenseDialog from '../components/ExpenseDialog.vue'
import TransactionDialog from '../components/TransactionDialog.vue'


function showTransactionDialog() {
	transactionDialogIsOpen.value = true;
}

function showCategoryDialog() {
	categoryDialogIsOpen.value = true;
}

function showExpenseDialog() {
	expenseDialogIsOpen.value = true;
}

function addCategoryFilter(category) {
	delete store.state.transactionFilter.expenseId;
	delete store.state.transactionFilter.expense;
	store.state.transactionFilter.categoryId = category.id;
	store.state.transactionFilter.category = category;
	store.dispatch("setTransactionFilter", store.state.transactionFilter);
}

function addExpenseFilter(expense) {
	delete store.state.transactionFilter.categoryId;
	delete store.state.transactionFilter.category;
	store.state.transactionFilter.expenseId = expense.id;
	store.state.transactionFilter.expense = expense;
	store.dispatch("setTransactionFilter", store.state.transactionFilter);
}

function removeIdFilter() {
	store.dispatch("setTransactionFilter", {});
}

function changeDateType(event) {
	dateType.value = Number(event.target.value);

	switch (dateType.value) {
		case 0:
			delete store.state.transactionFilter.dateFrom;
			delete store.state.transactionFilter.dateTo;
			break;
		case 1:
			store.state.transactionFilter.dateFrom = new Date();
			store.state.transactionFilter.dateFrom.setHours(0, 0, 0, 0);
			store.state.transactionFilter.dateFrom.setTime(
				store.state.transactionFilter.dateFrom.getTime() - 30*86400000
			);

			store.state.transactionFilter.dateTo = new Date();
			store.state.transactionFilter.dateTo.setHours(0, 0, 0, 0);
			
			break;
	}

	store.dispatch("setTransactionFilter", store.state.transactionFilter);
}

function changeDateFrom(event) {
	store.state.transactionFilter.dateFrom = event.target.value;
	store.dispatch("setTransactionFilter", store.state.transactionFilter);
}

function changeDateTo(event) {
	store.state.transactionFilter.dateTo = event.target.value;
	store.dispatch("setTransactionFilter", store.state.transactionFilter);
}

function changeDate(event) {
	store.state.transactionFilter.dateFrom = event.target.value;
	store.state.transactionFilter.dateTo = event.target.value;
	store.dispatch("setTransactionFilter", store.state.transactionFilter);
}

function changeCategoryActive(event, category) {
	category.active = event.target.checked == true ? true : false;
	editCategory(category);
}

function changeExpenseActive(event, expense) {
	expense.active = event.target.checked == true ? true : false;
	//editExpense(expense);
	store.dispatch("editExpense", expense);
}


const store = useStore();

const tab = ref(0);
const dateType = ref(0);
const dateTypeModel = defineModel('dateType', { default: 0 });
const dateFrom = defineModel('dateFrom');
const dateTo = defineModel('dateTo');
const categoryDialogIsOpen = defineModel('categoryDialogIsOpen', { default: false });
const expenseDialogIsOpen = defineModel('expenseDialogIsOpen', { default: false });
const transactionDialogIsOpen = defineModel('transactionDialogIsOpen', { default: false });

const categoryRef = ref(null);
const expenseRef = ref(null);
const transactionRef = ref(null);

provide('categoryDialog', { categoryRef, categoryDialogIsOpen });
provide('expenseDialog', { expenseRef, expenseDialogIsOpen });
provide('transactionDialog', { transactionRef, transactionDialogIsOpen });


onMounted(() => {
	store.dispatch("fetchCategories");
	store.dispatch("fetchExpenses");
	store.dispatch("fetchTransactions");
});





</script>


<template>
	<div class="content">

		<!-- Левая часть -->
		<div class="left">
			<div class="tabs">
				<template v-if="tab == 0">
					<label class="h2">Категории</label>
					<label class="ref" @click="tab = 1">Статьи</label>
				</template>

				<template v-else>
					<label class="ref" @click="tab = 0">Категории</label>
					<label class="h2">Статьи</label>
				</template>
			</div>
			
			<!-- Категории -->
			<div v-if="tab == 0" class="tab">

				<AddButton 
					class="addcategory" 
					title="Категория" 
					@click="showCategoryDialog"/>

				<Card 
					v-for="category in store.state.categories" 
					:name="category.name"
					:activated="category.active"
					@activation="(event) => changeCategoryActive(event, category)"
					@edit="categoryRef = category; showCategoryDialog()"
					@click="addCategoryFilter(category)"
					@delete="store.dispatch('deleteCategory', category.id)"
				/>
			</div>

			<!-- Статьи -->
			<div v-else class="tab">

				<AddButton 
					class="addcategory" 
					title="Статья" 
					@click="showExpenseDialog"/>

				<Card 
					v-for="expense in store.state.expenses" 
					:name="expense.name"
					:activated="expense.active"
					@activation="(event) => changeExpenseActive(event, expense)"
					@edit="expenseRef = expense; showExpenseDialog()"
					@click="addExpenseFilter(expense)"
					@delete="store.dispatch('deleteExpense', expense.id)"
				/>
			</div>
			
			
		</div>

		<!-- Правая часть -->
		<div class="right">
			<!-- Верхняя часть -->
			<div class="right_header">
				<div class="filters h5">
					<select class="input_field filterelem" @change="changeDateType">
						<option :value="0">Всё время</option>
						<option :value="1">За 30 дней</option>
						<option :value="2">День</option>
						<option :value="3">Промежуток</option>
					</select>

					<input 
						v-if="dateType == 2" 
						class="input_field filterelem" 
						type="date" 
						@change="changeDate"/>

					<div v-else-if="dateType == 3">
						С
						<input class="input_field filterelem" type="date" @change="changeDateFrom"/>
						До
						<input class="input_field filterelem" type="date" @change="changeDateTo"/>
					</div>

					<FilterButton 
						class="filterelem"
						v-if="store.state.transactionFilter.hasOwnProperty('category')"
						:title="store.state.transactionFilter.category.name" 
						@close="removeIdFilter"/>

					<FilterButton 
						class="filterelem"
						v-if="store.state.transactionFilter.hasOwnProperty('expense')"
						:title="store.state.transactionFilter.expense.name" 
						@close="removeIdFilter"/>
				</div>

				<AddButton title="Транзакция" @click="showTransactionDialog"/>
			</div>

			<hr/>

			<!-- Список транзакций и дат -->

			<div v-for="transactionDate in store.state.transactions">
				<DateLabel 
					:key="transactionDate.date" 
					:date="transactionDate.date" 
					:price="transactionDate.price"/>

				<TransactionCard 
					v-for="transaction in transactionDate.items" 
					:key="transaction.id"
					:transaction="transaction"/>
			</div>
			
		</div>

		<!-- Диалоговые окна -->
		<CategoryDialog/>
		<ExpenseDialog/>
		<TransactionDialog/>


	</div>
	
</template>


<style scoped>

hr {
	margin: 0;
	margin-bottom: 20px;
}


.left {
	box-sizing: border-box;
	height: 100vh;
	width: 30%;
	
	padding: 10px;
	padding-top: 50px;

	border-right: 1px solid var(--border);

	display: flex;
	flex-direction: column;

	position: fixed;
	z-index: 1;
	top: 0;
	left: 0;
}

.right {
	width: 80%;

	padding: 20px;
	padding-top: 50px;
	margin-left: 30%;
}

.tabs {
	display: flex;
	flex-direction: row;
	justify-content: space-evenly;

	margin-bottom: 20px;
}

.tab {
	overflow: auto;
	scrollbar-width: thin;
	
	box-sizing: border-box;
	height: 100%;
}

.content {
	width: 100%;

	display: flex;
	flex-direction: row;
}

.right_header {
	display: flex;
	flex-direction: row;
	align-items: flex-start;
	justify-content: space-between;

	margin-bottom: 10px;
}

.filters {
	display: flex;
	flex-direction: row;
	flex-wrap: wrap;
	align-items: center;
}

.filterelem {
	margin-right: 10px;
	margin-bottom: 10px;
	font-size: 16px;
}

.addcategory {
	margin-bottom: 5px;
}



</style>