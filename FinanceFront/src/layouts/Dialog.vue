<script setup>
import { ref, watch } from 'vue'



const emit = defineEmits(["ok", "close"]);

const isOpen = defineModel('isOpen', { default: false })

const dialogRef = ref(null)

watch(() => isOpen.value, (newValue) => {
	if (!dialogRef.value) return

	if (newValue) {
		dialogRef.value.showModal()
	} else {
		dialogRef.value.close()
	}
})

const closeDialog = () => {
	isOpen.value = false;
	emit('close');
}

const okDialog = () => {
	emit('ok', isOpen);
}

</script>

<template>
	<dialog ref="dialogRef" class="custom-modal">
		<div class="modal-content" v-if="isOpen">
			<!-- Header Slot -->
			<header class="modal-header">
				<slot name="header">
				</slot>
				<button class="close-btn" @click="closeDialog">×</button>
			</header>

			<!-- Body Slot -->
			<main class="modal-body">
				<slot></slot>
			</main>

			<!-- Footer Slot -->
			<footer class="modal-footer">
				<slot name="footer">
					<div class="footer-buttons">
						<button class="btn" @click="okDialog">ОК</button>
						<button class="btn cancel" @click="closeDialog">Отмена</button>
					</div>
				</slot>
			</footer>
		</div>
	</dialog>
</template>



<style scoped>

.custom-modal {
	border: none;
	border-radius: 8px;
	padding: 0;
	max-width: 500px;
	width: 100%;
}

.custom-modal::backdrop {
	background-color: rgba(0, 0, 0, 0.5);
	backdrop-filter: blur(3px);
}

.modal-content {
	padding: 20px;	
}

.modal-header {
	display: flex;
	justify-content: space-between;
	align-items: center;
}

.modal-body {
	margin-bottom: 20px;
}

.close-btn {
	background: none;
	border: none;
	font-size: 24px;
	cursor: pointer;
}

.footer-buttons {
	display: flex;
	direction: rtl;
}

.cancel {
	margin-right: 10px;
}


</style>