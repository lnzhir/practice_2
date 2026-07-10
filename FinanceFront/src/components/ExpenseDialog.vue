<script setup>
import { ref, watch, inject } from 'vue'
import { useStore } from 'vuex'
import Dialog from '../layouts/Dialog.vue';
import { getCategories } from '../api/category.js';

// const model = defineModel('isOpen');


const expenseName = defineModel('expenseName');
const categoryModel = defineModel('category');

const store = useStore();

const { expenseRef, expenseDialogIsOpen } = inject('expenseDialog');

const activeCategories = ref([]);


function ok(isOpen) {
	if (expenseName.value == "" || !expenseName.value || categoryModel.value == null) {
		isOpen.value = true;
		return;
	}

	if (expenseRef.value == null) {
		// Добавление
		store.dispatch("addExpense", {
			name: expenseName.value,
			categoryId: categoryModel.value.id,
			active: true
		});
	} else {
		// Изменение
		expenseRef.value.name = expenseName.value;
		expenseRef.value.category = categoryModel.value;
		expenseRef.value.categoryId = categoryModel.value.id;

		store.dispatch("editExpense", expenseRef.value);
	}

	isOpen.value = false;
	close();
}

function close() {
	// Очистка полей
	expenseName.value = "";
	expenseRef.value = null;
}

async function fetchCategories() {
	activeCategories.value = (await getCategories({ active: true})).data;
}

watch(() => expenseDialogIsOpen.value, (newValue) => {
	if (newValue == true) { // Окно открыто
		fetchCategories();

		if (expenseRef.value != null) {
			expenseName.value = expenseRef.value.name;
			categoryModel.value = expenseRef.value.category;
		}
	}
})


</script>


<template>
	<Dialog v-model:isOpen="expenseDialogIsOpen" @ok="ok" @close="close">
		<template #header>
			<h2 v-if="expenseRef == null">Добавить статью</h2>
			<h2 v-else>Изменить статью</h2>
		</template>

		<p class="section">Категория</p>
		<select class="input_field" v-model="categoryModel">
			<option 
				v-for="category in activeCategories" 
				:key="category.id"
				:value="category">{{ category.name }}</option>
		</select>

		<p class="section">Название</p>
		<input class="input_field" type="text" v-model="expenseName">


	</Dialog>

</template>

<style scoped>

.section {
	margin-left: 5px;
}

input, select {
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
</style>