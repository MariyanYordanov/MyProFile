import axios from "axios";

// Взимаме токена от localStorage (или по-късно от context)
const getToken = () => localStorage.getItem("token");

// Създаваме Axios инстанция с базов URL към backend API (през proxy)
const api = axios.create({
    baseURL: "/api",
    withCredentials: true, // за да пращаме cookies ако има
    headers: {
        "Content-Type": "application/json",
    },
});

// Добавяме Authorization header автоматично към всички заявки
api.interceptors.request.use((config) => {
    const token = getToken();
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;
