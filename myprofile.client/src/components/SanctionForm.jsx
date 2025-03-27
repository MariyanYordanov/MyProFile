import { useState } from "react";

export default function SanctionForm() {
    const [reason, setReason] = useState("");
    const [notes, setNotes] = useState("");
    const [date, setDate] = useState("");
    const [studentId, setStudentId] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            reason,
            notes,
            date,
            studentId: Number(studentId)
        };

        try {
            const response = await fetch("/api/Sanctions", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(payload)
            });

            if (response.ok) {
                alert("✅ Санкцията е добавена успешно!");
                setReason("");
                setNotes("");
                setDate("");
                setStudentId("");
            } else {
                alert("❌ Грешка при добавяне на санкцията.");
            }
        } catch (error) {
            console.error("Грешка:", error);
            alert("⚠️ Възникна грешка при връзката с API.");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>🛑 Добавяне на санкция</h2>

            <input
                type="text"
                value={reason}
                onChange={(e) => setReason(e.target.value)}
                placeholder="Причина"
                required
            />

            <textarea
                value={notes}
                onChange={(e) => setNotes(e.target.value)}
                placeholder="Бележки (по избор)"
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

            <button type="submit">🚫 Добави санкция</button>
        </form>
    );
}
