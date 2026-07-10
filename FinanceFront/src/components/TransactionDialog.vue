<script setup>
import { ref, watch, inject } from 'vue'
import { useStore } from 'vuex'
import Dialog from '../layouts/Dialog.vue';
import { getExpenses } from '../api/expense.js'
import { getTransactions } from '../api/transaction.js';


//const model = defineModel('isOpen');
const expenseModel = defineModel('expense');
const price = defineModel('price');
const date = defineModel('date');
const description = defineModel('description');


const { transactionRef, transactionDialogIsOpen } = inject('transactionDialog');

const activeExpenses = ref([]);
const expenseActive = ref(true);

const store = useStore();


async function ok(isOpen) {
	// Валидация
	if (price.value == "" || !price.value || !date.value || !expenseModel.value) {
		isOpen.value = true;
		return;
	}

	if (transactionRef.value == null) {
		// Добавление
		store.dispatch("addTransaction", {
			price: price.value,
			date: date.value,
			description: description.value,
			expenseId: expenseModel.value
		});
	} else {
		// Изменение
		transactionRef.value.price = price.value;
		transactionRef.value.date = date.value;
		transactionRef.value.description = description.value;
		//transactionRef.value.expense = expenseModel.value;
		//transactionRef.value.expense = activeExpenses.value[activeExpenses.value.findIndex(e => e.id == expenseModel.value)];
		transactionRef.value.expenseId = expenseModel.value;

		store.dispatch("editTransaction", transactionRef.value);
	}

	isOpen.value = false;
	close();
}

function close() {
	// Очистка полей
	price.value = "";
	date.value = "";
	description.value = "";
	transactionRef.value = null;
	expenseActive.value = true;
}

async function fetchExpense() {
	const expenses = (await getExpenses({ active: true })).data;
	expenses.forEach(element => {
		element.category = null;
	});
	activeExpenses.value = expenses;
}

watch(() => transactionDialogIsOpen.value, (newValue) => {
	if (newValue == true) { // Окно открыто
		fetchExpense();

		if (transactionRef.value != null) { // На изменение
			price.value = transactionRef.value.price;
			//date.value = transactionRef.value.date.split('T')[0];
			date.value = transactionRef.value.date;
			description.value = transactionRef.value.description;
			expenseModel.value = transactionRef.value.expenseId;
			expenseActive.value = transactionRef.value.expense.active;
		}
	}
})

</script>


<template>
	<Dialog v-model:isOpen="transactionDialogIsOpen" @ok="ok" @close="close">
		<template #header>
			<h2 v-if="transactionRef == null">Добавить транзакцию</h2>
			<h2 v-else>Изменить транзакцию</h2>
		</template>

		<p class="section">Статья</p>
		
		<select class="input_field" v-model="expenseModel" :disabled="!expenseActive">
			<option 
				v-if="!expenseActive"
				:key="transactionRef.expenseId" 
				:value="transactionRef.expenseId"
				>{{ transactionRef.expense.name }}</option>
			<option 
				v-for="expense in activeExpenses"
				:key="expense.id" 
				:value="expense.id"
				>{{ expense.name }}</option>
		</select>

		<p class="section">Цена</p>
		<input class="input_field" type="number" v-model="price"/>

		<p class="section">Дата</p>
		<input class="input_field" type="date" v-model="date"/>

		<p class="section">Комментарий</p>
		<textarea class="input_field" v-model="description"></textarea>

	</Dialog>

</template>

<style scoped>

.section {
	margin-left: 5px;
}

input, select, textarea {
	box-sizing: border-box;
	margin-top: 5px;
	margin-bottom: 20px;
}

input {
	width: 50%;
}

select {
	width: 50%;
}

textarea {
	width: -webkit-fill-available;
	min-height: 100px;
	resize: none;
}

</style>