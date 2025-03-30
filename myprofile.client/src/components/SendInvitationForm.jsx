import { useState } from "react";
import axios from "axios";

export default function SendInvitationForm() {
    const [email, setEmail] = useState("");
    const [message, setMessage] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        setMessage("");
        try {
            await axios.post("/auth/invite", { email });
            setMessage("Поканата е изпратена успешно.");
            setEmail("");
        } catch (err) {
            setMessage("Грешка при изпращане на поканата.");
        }
    };

    return (
        <div className="max-w-md mx-auto p-4 border rounded shadow">
            <h2 className="text-xl font-bold mb-2">Изпрати покана</h2>
            <form onSubmit={handleSubmit} className="space-y-3">
                <input
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder="Имейл на поканения"
                    required
                    className="w-full border p-2"
                />
                <button type="submit" className="bg-green-600 text-white py-2 px-4 rounded">
                    Изпрати
                </button>
                {message && <p className="text-sm mt-2">{message}</p>}
            </form>
        </div>
    );
}
