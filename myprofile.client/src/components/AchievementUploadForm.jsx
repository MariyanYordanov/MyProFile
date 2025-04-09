import { useState } from "react";
import api from "@/services/api";

export default function AchievementUploadForm({ onUpload }) {
    const [description, setDescription] = useState("");
    const [file, setFile] = useState(null);
    const [error, setError] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!file) return setError("Моля, прикачете файл.");

        const formData = new FormData();
        formData.append("description", description);
        formData.append("file", file);

        try {
            const res = await api.post("/achievements", formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            });

            setDescription("");
            setFile(null);
            setError(null);
            onUpload?.(res.data);
        } catch (err) {
            console.error(err);
            setError("Грешка при качването на постижението.");
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            <div>
                <label className="block mb-1">Описание на постижението</label>
                <input
                    type="text"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    className="w-full border px-3 py-2 rounded"
                    required
                />
            </div>
            <div>
                <label className="block mb-1">Файл</label>
                <input
                    type="file"
                    onChange={(e) => setFile(e.target.files[0])}
                    className="w-full"
                    required
                />
            </div>
            {error && <p className="text-red-500 text-sm">{error}</p>}
            <button
                type="submit"
                className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
            >
                Качи постижение
            </button>
        </form>
    );
}
