
<script setup>
	import Times from '@primeicons/vue/times';

	const props = defineProps({
		title: { type: String, required: true },
	})

	const emit = defineEmits(["close"]);

</script>

<template>
	<div class="root">
		<label>{{ title }}</label>
		<button class="btn_icon_empty rounded" @click="emit('close')">
			<Times/>
		</button>
	</div>
</template>

<style scoped>

/* label {
	margin-top: 2px;
	margin-bottom: 2px;
} */

button {
	/* padding-right: 7px;
	padding-left: 7px; */
	padding: revert;
}

.root {
	display: flex;
	align-items: center;

	background-color: var(--btn-bg);
	border: 1px solid var(--btn-border);
	border-radius: 8px;

	padding-left: 15px;
}

</style>