import { useState } from "react";

export default function CreditUploadForm() {
    const [file, setFile] = useState(null);
    const [type, setType] = useState("Професия");
    const [value, setValue] = useState(1);
    const [validatedBy, setValidatedBy] = useState("");
    const [studentId, setStudentId] = useState("");

    const validatorOptions = [
        "Класен ръководител",
        "Ментор",
        "Преподавател",
        "Родител",
        "Екипна оценка",
        "Самооценка"
    ];

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!studentId) {
            alert("Моля, въведете ID на ученик.");
            return;
        }

        const formData = new FormData();
        formData.append("file", file);
        formData.append("type", type);
        formData.append("value", value);
        formData.append("validatedBy", validatedBy);
        formData.append("studentId", studentId);

        try {
            const response = await fetch("/api/Credits/upload", {
                method: "POST",
                body: formData
            });

            // 👇 Проверяваме дали отговорът е валиден JSON
            if (!response.ok) {
                const text = await response.text(); // за дебъгване
                console.error("❌ Грешка от сървъра:", text);
                alert("⚠️ Грешка: " + response.status + "\n" + text);
                return;
            }

            const result = await response.json();
            alert("✅ Кредитът е качен!\n" + result.proofPath);
        } catch (error) {
            console.error("Грешка при качване:", error);
            alert("❌ Възникна грешка при качването.");
        }
    };

    return (
        <form onSubmit={handleSubmit} encType="multipart/form-data">
            <h2>Качване на кредит</h2>

            <input
                type="file"
                accept="image/*,.pdf"
                onChange={(e) => setFile(e.target.files[0])}
                required
            />

            <select value={type} onChange={(e) => setType(e.target.value)}>
                <option value="Професия">Професия</option>
                <option value="Мислене">Мислене</option>
                <option value="Аз и другите">Аз и другите</option>
            </select>

            <input
                type="number"
                value={value}
                min={1}
                onChange={(e) => setValue(Number(e.target.value))}
                placeholder="Стойност"
                required
            />

            <select
                value={validatedBy}
                onChange={(e) => setValidatedBy(e.target.value)}
                required
            >
                <option value="">Избери валидатор</option>
                {validatorOptions.map((v, index) => (
                    <option key={index} value={v}>
                        {v}
                    </option>
                ))}
            </select>

            <input
                type="number"
                value={studentId}
                min={1}
                onChange={(e) => setStudentId(e.target.value)}
                placeholder="ID на ученик"
                required
            />

            <button type="submit">📤 Качи</button>
        </form>
    );
}
