<script setup>

import Pencil from '@primeicons/vue/pencil';
import Times from '@primeicons/vue/times';


const props = defineProps({
	name: { type: String, required: true },
	activated: { type: Boolean, required: false }
})

const emit = defineEmits(["edit", "delete", "activation", "click"]);

function activation(event) {
	emit('activation', event);
}

</script>



<template>
	
	<div class="card rounded">
		<div>
			<input type="checkbox" :checked="activated" @change="activation"/>
			<label class="ref" @click="emit('click')">{{ name }}</label>
		</div>
		

		<div>
			<button class="btn_icon_empty" @click="emit('edit')"><Pencil/></button>
			<button class="btn_icon_empty "@click="emit('delete')"><Times/></button>
		</div>

		
	</div>
	
</template>


<style scoped>

.card {
	width: 100%;
	height: auto;

	display: flex;
	align-items: center;
	justify-content: space-between;

	margin-bottom: 5px;
	padding: 5px;
	box-sizing: border-box;

	border: 1px solid black;
}

</style>