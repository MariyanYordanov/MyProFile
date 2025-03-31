import { useState } from "react";

export default function AchievementUploadForm() {
    const [file, setFile] = useState(null);
    const [title, setTitle] = useState("");
    const [details, setDetails] = useState("");
    const [date, setDate] = useState("");
    const [studentId, setStudentId] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append("file", file);
        formData.append("title", title);
        formData.append("details", details);
        formData.append("date", date);
        formData.append("studentId", studentId);

        try {
            const response = await fetch("/Achievements/upload", {
                method: "POST",
                body: formData
            });

            if (!response.ok) {
                const err = await response.text();
                console.error("Server error:", err);
                alert("⚠️ Грешка: " + response.status);
                return;
            }

            const result = await response.json();
            alert("✅ Постижението е качено!\n" + result.proofPath);
        } catch (error) {
            console.error("Грешка при качване:", error);
            alert("❌ Възникна грешка при качването.");
        }
    };

    return (
        <form onSubmit={handleSubmit} encType="multipart/form-data">
            <h2>Качване на постижение</h2>

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
                value={details}
                onChange={(e) => setDetails(e.target.value)}
                placeholder="Допълнителна информация"
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

            <button type="submit">📤 Качи</button>
        </form>
    );
}
