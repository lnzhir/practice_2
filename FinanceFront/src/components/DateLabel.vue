
<script setup>
import { ref, watch } from 'vue'
import CircleFill from '@primeicons/vue/circle-fill';

const props = defineProps({
	date: { type: String, required: true },
	price: { type: Number, required: true },
})

const markers = [
	{ color: "green", to: 500},
	{ color: "yellow", to: 2000},
	{ color: "red", to: 10000000}
]

// менее 500 рублей, отображать зеленый стикер
// от 500 до 2000 рублей, отображать желтый стикер
// более 2000 рублей, отображать красный стикер

const weekdays = ["Воскресенье", "Понедельник", "Вторник", 
	"Среда", "Четверг", "Пятница", "Суббота"];
const days = ["Сегодня", "Вчера"];


const curDate = new Date();
const nativeDate = new Date(props.date);
curDate.setHours(0, 0, 0, 0);
nativeDate.setHours(0, 0, 0, 0);
const differenceDays = (curDate - nativeDate)/(86400000);
const weekday = differenceDays >= 0 && differenceDays < days.length ?
	days[differenceDays] : weekdays[nativeDate.getDay()];
const dateString = new Intl.DateTimeFormat('en-GB')
							.format(new Date(nativeDate))
							.replace(/\//g, '-');

const markerColor = props.price < markers[0].to ? 
	markers[0].color : (props.price < markers[1].to ? markers[1].color : markers[2].color)


</script>


<template>
	<div class="root">
		<label>{{ weekday }} - {{ dateString }}</label>
		<div class="right">
			<label>{{ price.toFixed(2) }} руб.</label>
			<CircleFill class="marker" :color="markerColor"/>
		</div>
		
	</div>
</template>

<style scoped>
.root {
	display: flex;
	flex-direction: row;
	align-items: center;
	justify-content: space-between;

	margin-bottom: 20px;
}

.marker {
	margin-left: 10px;
}

.right {
	display: flex;
	flex-direction: row;
	align-items: center;
}
</style>