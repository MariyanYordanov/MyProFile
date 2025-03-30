// axiosInterceptor.js
import axios from "axios";

export const setupAxiosInterceptors = (auth) => {
    const instance = axios.create({
        baseURL: "/api",
        headers: { "Content-Type": "application/json" }
    });

    instance.interceptors.request.use((config) => {
        const token = localStorage.getItem("token");
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    });

    instance.interceptors.response.use(
        (response) => response,
        async (error) => {
            const originalRequest = error.config;
            if (error.response?.status === 401 && !originalRequest._retry) {
                originalRequest._retry = true;
                try {
                    const res = await axios.post("/auth/refresh", {
                        accessToken: localStorage.getItem("token"),
                        refreshToken: localStorage.getItem("refreshToken")
                    });
                    localStorage.setItem("token", res.data.token);
                    localStorage.setItem("refreshToken", res.data.refreshToken);
                    originalRequest.headers.Authorization = `Bearer ${res.data.token}`;
                    return axios(originalRequest);
                } catch (err) {
                    auth.logout?.(); // ако имаш logout функция
                }
            }
            return Promise.reject(error);
        }
    );

    // Задаваме глобалния axios да използва тази конфигурация
    axios.defaults.baseURL = "/api";
    axios.defaults.headers.common["Content-Type"] = "application/json";
    axios.defaults.headers.common["Authorization"] = `Bearer ${localStorage.getItem("token")}`;
};
