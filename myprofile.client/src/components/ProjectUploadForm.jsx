import { useState } from "react";

export default function ProjectUploadForm() {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [studentId, setStudentId] = useState("");
    const [screenshot, setScreenshot] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append("title", title);
        formData.append("description", description);
        formData.append("studentId", studentId);
        formData.append("screenshot", screenshot);

        try {
            const res = await fetch("/Projects/upload", {
                method: "POST",
                body: formData
            });

            const result = await res.json();
            alert("✅ Проект качен: " + result.screenshot);
        } catch (err) {
            console.error("Грешка:", err);
            alert("❌ Качването се провали.");
        }
    };

    return (
        <form onSubmit={handleSubmit} encType="multipart/form-data">
            <h2>📁 Качи проект</h2>

            <input
                type="text"
                placeholder="Заглавие"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />

            <textarea
                placeholder="Описание"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
            />

            <input
                type="number"
                placeholder="Student ID"
                value={studentId}
                onChange={(e) => setStudentId(e.target.value)}
                required
            />

            <input
                type="file"
                accept="image/*"
                onChange={(e) => setScreenshot(e.target.files[0])}
                required
            />

            <button type="submit">📤 Качи</button>
        </form>
    );
}
