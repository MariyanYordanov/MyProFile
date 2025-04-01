import { useState } from "react";
import api from "@/services/api.js";


export default function ProjectUploadForm({ studentId, onUpload }) {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [screenshot, setScreenshot] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append("title", title);
        formData.append("description", description);
        formData.append("studentId", studentId);
        if (screenshot) formData.append("screenshot", screenshot);

        try {
            await api.post(`/projects`, formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            });
            onUpload?.();
            setTitle("");
            setDescription("");
            setScreenshot(null);
        } catch (err) {
            console.error("Project upload failed", err);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            <input
                type="text"
                placeholder="Заглавие"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
                className="w-full border p-2"
            />
            <textarea
                placeholder="Описание"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                className="w-full border p-2"
            />
            <input
                type="file"
                accept="image/*"
                onChange={(e) => setScreenshot(e.target.files[0])}
                className="w-full"
            />
            <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded">
                Качи проект
            </button>
        </form>
    );
}
