<script setup>
import { ref, watch, getCurrentInstance, inject } from 'vue'
import { useStore } from 'vuex'
import Dialog from '../layouts/Dialog.vue';




// const model = defineModel('isOpen');
const categoryName = defineModel('categoryName');
// const category = defineModel('category');

const { categoryRef, categoryDialogIsOpen } = inject('categoryDialog');

const { proxy } = getCurrentInstance();

const store = useStore();


function ok(isOpen) {
	if (categoryName.value == "" || !categoryName.value) {
		isOpen.value = true;
		return;
	}

	if (categoryRef.value == null) {
		// Добавление
		store.dispatch("addCategory", {
			name: categoryName.value,
			active: true
		});
	} else {
		// Изменение
		categoryRef.value.name = categoryName.value;

		store.dispatch("editCategory", categoryRef.value);
	}

	isOpen.value = false;
	close();
}

function close() {
	// Очистка полей
	categoryName.value = "";
	categoryRef.value = null;
}

watch(() => categoryDialogIsOpen.value, (newValue) => {
	if (newValue == true && categoryRef.value != null) { // Окно открыто на изменение
		categoryName.value = categoryRef.value.name;
	}
})


</script>


<template>
	<Dialog v-model:isOpen="categoryDialogIsOpen" @ok="ok" @close="close">
		<template #header>
			<h2 v-if="categoryRef == null">Добавить категорию</h2>
			<h2 v-else>Изменить категорию</h2>
		</template>

		<p class="section">Название</p>
		<input class="input_field" type="text" v-model="categoryName"/>


	</Dialog>

</template>

<style scoped>

.section {
	margin-left: 5px;
}

input {
	box-sizing: border-box;
	width: 50%;
	margin-top: 5px;
	margin-bottom: 20px;
}

</style>