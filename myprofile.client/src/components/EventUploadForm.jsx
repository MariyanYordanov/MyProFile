import { useState } from "react";

export default function EventUploadForm() {
    const [file, setFile] = useState(null);
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [date, setDate] = useState("");
    const [studentId, setStudentId] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!file || !studentId || !title || !date) {
            alert("Моля, попълни всички задължителни полета.");
            return;
        }

        const formData = new FormData();
        formData.append("file", file);
        formData.append("title", title);
        formData.append("description", description);
        formData.append("date", date);
        formData.append("studentId", studentId);

        try {
            const response = await fetch("/Events/upload", {
                method: "POST",
                body: formData,
            });

            if (response.ok) {
                const result = await response.json();
                alert("✅ Събитието е добавено!\n" + result.filePath);
            } else {
                alert("❌ Грешка при качване на събитието.");
            }
        } catch (err) {
            console.error("Грешка:", err);
            alert("❌ Възникна грешка.");
        }
    };

    return (
        <form onSubmit={handleSubmit} encType="multipart/form-data">
            <h2>📅 Качване на събитие</h2>

            <input
                type="file"
                accept="image/*,.pdf"
                onChange={(e) => setFile(e.target.files[0])}
                required
            />

            <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="Заглавие"
                required
            />

            <textarea
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                placeholder="Описание"
            />

            <input
                type="date"
                value={date}
                onChange={(e) => setDate(e.target.value)}
                required
            />

            <input
                type="number"
                value={studentId}
                onChange={(e) => setStudentId(e.target.value)}
                placeholder="ID на ученик"
                required
            />

            <button type="submit">📤 Качи събитие</button>
        </form>
    );
}
