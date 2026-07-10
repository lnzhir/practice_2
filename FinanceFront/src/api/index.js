import axios from "axios";


export const rest = axios.create({
	baseURL: import.meta.env.VITE_BACK_HOST,
	headers: {
		'Access-Control-Allow-Origin' : '*',
		'Access-Control-Allow-Methods':'GET,PUT,POST,DELETE,PATCH,OPTIONS',
		'Content-Type': 'application/json'
	}
});