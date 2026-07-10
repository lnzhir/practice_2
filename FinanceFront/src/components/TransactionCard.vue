<script setup>
import { ref, watch, inject } from 'vue'
import { useStore } from 'vuex'
import Pencil from '@primeicons/vue/pencil';
import Times from '@primeicons/vue/times';



const props = defineProps({
	transaction: { type: Object, required: true },
})

const store = useStore();

const { transactionRef, transactionDialogIsOpen } = inject('transactionDialog');



function edit() {
	transactionRef.value = props.transaction;
	transactionDialogIsOpen.value = true;
}

function _delete() {
	store.dispatch('deleteTransaction', props.transaction.id)
}

const active = ref(false);

</script>



<template>
	
	<div class="card rounded">
		<div class="header">
			<label class="large">{{ transaction.expense.name }}</label>

			<div class="header_right">
				<label class="large">{{ transaction.price.toFixed(2) }} руб.</label>
				<button class="btn_icon_empty" @click="edit" ><Pencil/></button>
				<button class="btn_icon_empty" @click="_delete"><Times/></button>
			</div>
		</div>

		<small class="category">
			Категория: {{ transaction.expense.category.name }}
		</small>

		<small>
			{{ transaction.description }}
		</small>
	</div>
	
</template>


<style scoped>

.card {
	width: auto;

	display: flex;
	flex-direction: column;
	justify-content: space-between;

	margin-bottom: 10px;
	padding: 20px;
	box-sizing: border-box;

	border: 1px solid var(--border);
}

.header {
	display: flex;
	align-items: center;
	justify-content: space-between;
}

.header_right {
	display: flex;
	align-items: center;
}

.large {
	font-size: larger;
}

.category {
	margin-bottom: 20px;
}

</style>