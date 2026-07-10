
<script setup>

import PlusCircle from '@primeicons/vue/plus-circle';

const props = defineProps({
	title: { type: String, required: true },
})

const emit = defineEmits(["click"]);

</script>

<template>
	<div class="btn_empty root rounded" @click="emit('click')">
		<PlusCircle class="plus"/>
		{{ title }}
	</div>
</template>

<style scoped>
.root {
	display: flex;
	align-items: center;

	padding: 5px;
	user-select: none;
}

.plus {
	margin-right: 5px;
}
</style>