import { createContext, useContext, useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

export function AuthProvider({ children }) {
    const navigate = useNavigate();
    const [token, setToken] = useState(localStorage.getItem("token"));
    const [user, setUser] = useState(null);

    useEffect(() => {
        if (token) {
            try {
                const decoded = jwtDecode(token);

                console.log("[JWT Debug] Token:", token);
                console.log("[JWT Debug] Decoded:", decoded);

                const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
                const emailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

                const role = decoded[roleClaim] || decoded.role || "unknown";
                const email = decoded[emailClaim] || decoded.email || "unknown";

                setUser({
                    email,
                    role,
                    exp: decoded.exp,
                });
            } catch (err) {
                console.error("Невалиден токен:", err);
                logout();
            }
        } else {
            setUser(null);
        }
    }, [token]);

    const login = (newToken) => {
        localStorage.setItem("token", newToken);
        setToken(newToken);
    };

    const logout = () => {
        localStorage.removeItem("token");
        setToken(null);
        setUser(null);
        navigate("/login");
    };

    return (
        <AuthContext.Provider value={{ token, user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    return useContext(AuthContext);
}
