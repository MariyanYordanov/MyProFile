import { useState } from "react";

export default function InterestForm() {
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [studentId, setStudentId] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            name,
            description,
            studentId: Number(studentId)
        };

        try {
            const response = await fetch("/Interests", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(payload)
            });

            if (response.ok) {
                alert("✅ Интересът е добавен успешно!");
                setName("");
                setDescription("");
                setStudentId("");
            } else {
                alert("❌ Грешка при добавяне на интереса.");
            }
        } catch (error) {
            console.error("Грешка:", error);
            alert("⚠️ Възникна грешка при връзката с API.");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>💡 Добавяне на интерес</h2>

            <input
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Име на интерес"
                required
            />

            <textarea
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                placeholder="Описание (по избор)"
            />

            <input
                type="number"
                value={studentId}
                onChange={(e) => setStudentId(e.target.value)}
                placeholder="ID на ученик"
                required
            />

            <button type="submit">🌟 Добави интерес</button>
        </form>
    );
}
