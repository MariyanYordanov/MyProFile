import axios from "axios";

// Взимаме токена от localStorage (може и от context в бъдеще)
const getToken = () => localStorage.getItem("token");

const api = axios.create({
    baseURL: "/api",
    headers: {
        "Content-Type": "application/json",
    },
});

// Автоматично добавяме Bearer token към всяка заявка
api.interceptors.request.use((config) => {
    const token = getToken();
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;
