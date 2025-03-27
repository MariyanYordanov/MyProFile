import { useState } from "react";

export default function ProfilePictureUploadForm() {
    const [file, setFile] = useState(null);
    const [studentId, setStudentId] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!studentId || !file) {
            alert("Моля, въведете ID на ученик и изберете файл.");
            return;
        }

        const formData = new FormData();
        formData.append("file", file);
        formData.append("studentId", studentId);

        try {
            const response = await fetch("/api/Students/upload-picture", {
                method: "POST",
                body: formData
            });

            if (!response.ok) throw new Error("Грешка при качването");

            const result = await response.json();
            alert(`✅ Снимката е качена успешно!\n${result.profilePicturePath}`);
        } catch (err) {
            console.error(err);
            alert("❌ Неуспешно качване на снимката.");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>📸 Качване на профилна снимка</h2>

            <input
                type="file"
                accept="image/*"
                onChange={(e) => setFile(e.target.files[0])}
                required
            />

            <input
                type="number"
                placeholder="ID на ученик"
                value={studentId}
                onChange={(e) => setStudentId(e.target.value)}
                required
            />

            <button type="submit">📤 Качи снимка</button>
        </form>
    );
}
