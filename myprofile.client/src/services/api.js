import axios from "axios";

const getToken = () => localStorage.getItem("token");

const api = axios.create({
    baseURL: "/api",
    withCredentials: true,
    headers: {
        "Content-Type": "application/json"
    }
});

api.interceptors.request.use((config) => {
    const token = getToken();
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
        console.log("[axios] ✅ Token attached:", token.slice(0, 20) + "...");
    } else {
        console.warn("[axios] ❌ Невалиден или липсващ токен:", token);
    }
    return config;
});

export default api;
