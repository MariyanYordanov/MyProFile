import { useState } from "react";

export default function GoalForm() {
    const [area, setArea] = useState("");
    const [description, setDescription] = useState("");
    const [studentId, setStudentId] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            area,
            description,
            studentId: Number(studentId)
        };

        try {
            const response = await fetch("/api/Goals", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(payload)
            });

            if (response.ok) {
                alert("✅ Целта е създадена успешно!");
                setArea("");
                setDescription("");
                setStudentId("");
            } else {
                alert("❌ Грешка при създаване на целта.");
            }
        } catch (error) {
            console.error("Грешка:", error);
            alert("⚠️ Възникна грешка при връзката с API.");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>🎯 Създаване на цел</h2>

            <input
                type="text"
                value={area}
                onChange={(e) => setArea(e.target.value)}
                placeholder="Област"
                required
            />

            <textarea
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                placeholder="Описание"
            />

            <input
                type="number"
                value={studentId}
                onChange={(e) => setStudentId(e.target.value)}
                placeholder="ID на ученик"
                required
            />

            <button type="submit">📌 Създай цел</button>
        </form>
    );
}
