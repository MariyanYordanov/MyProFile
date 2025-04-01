import { useState } from "react";
import api from "@/services/api.js";

export default function CreditUploadForm({ studentId, onUpload }) {
    const [type, setType] = useState("");
    const [value, setValue] = useState(0);
    const [validatedBy, setValidatedBy] = useState("");
    const [proofFile, setProofFile] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append("type", type);
        formData.append("value", value);
        formData.append("validatedBy", validatedBy);
        formData.append("proofFile", proofFile);

        try {
            await api.post(`/students/${studentId}/credits`, formData, {
                headers: { "Content-Type": "multipart/form-data" },
            });
            onUpload?.();
            setType("");
            setValue(0);
            setValidatedBy("");
            setProofFile(null);
        } catch (error) {
            console.error("Upload failed", error);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="p-4 border rounded shadow-md space-y-4">
            <input
                type="text"
                placeholder="Тип кредит"
                value={type}
                onChange={(e) => setType(e.target.value)}
                required
                className="w-full p-2 border"
            />
            <input
                type="number"
                placeholder="Стойност"
                value={value}
                onChange={(e) => setValue(Number(e.target.value))}
                required
                className="w-full p-2 border"
            />
            <input
                type="text"
                placeholder="Валидиран от"
                value={validatedBy}
                onChange={(e) => setValidatedBy(e.target.value)}
                required
                className="w-full p-2 border"
            />
            <input
                type="file"
                accept="image/*,.pdf"
                onChange={(e) => setProofFile(e.target.files[0])}
                className="w-full p-2 border"
            />
            <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded">
                Качи кредит
            </button>
        </form>
    );
}
