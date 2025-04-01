import { useState } from "react";
import api from "@/services/api.js";


export default function ProfilePictureUploadForm({ studentId, onUploadSuccess }) {
    const [file, setFile] = useState(null);
    const [loading, setLoading] = useState(false);

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!file) return alert("Моля, изберете файл.");

        const formData = new FormData();
        formData.append("profilePicture", file);

        try {
            setLoading(true);
            await api.post(`/students/${studentId}/profile-picture`, formData, {
                headers: { "Content-Type": "multipart/form-data" },
            });
            alert("✅ Профилната снимка е качена успешно!");
            onUploadSuccess?.();
            setFile(null);
        } catch (err) {
            console.error("❌ Грешка при качване на снимка:", err);
            alert("⚠️ Възникна грешка при качването.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>📷 Качи профилна снимка</h2>
            <input type="file" accept="image/*" onChange={handleFileChange} required />
            <button type="submit" disabled={loading}>
                {loading ? "Качване..." : "Качи"}
            </button>
        </form>
    );
}
