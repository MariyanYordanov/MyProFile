import { useEffect, useState } from "react";
import axios from "axios";

export default function StudentOverviewPage({ studentId }) {
    const [overview, setOverview] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchOverview = async () => {
            try {
                const response = await axios.get(`/students/${studentId}/overview`);
                setOverview(response.data);
            } catch (err) {
                console.error("Грешка при зареждане на обобщението:", err);
                setError("Възникна грешка при зареждането на профила.");
            }
        };

        if (studentId) {
            fetchOverview();
        }
    }, [studentId]);

    if (error) {
        return <div className="text-red-500">{error}</div>;
    }

    if (!overview) {
        return <div>Зареждане на профил...</div>;
    }

    const { student, credits, achievements, goals, events, sanctions, interests, stats } = overview;

    return (
        <div className="space-y-4">
            <h3 className="text-xl font-bold mb-2">Обобщена информация за {student.fullName}</h3>
            <p><strong>Клас:</strong> {student.class}</p>
            <p><strong>Специалност:</strong> {student.speciality}</p>
            <p><strong>Среден успех:</strong> {student.averageGrade}</p>
            <p><strong>Рейтинг:</strong> {student.rating}</p>
            {student.mentorName && <p><strong>Ментор:</strong> {student.mentorName}</p>}

            <div>
                <h4 className="font-semibold mt-4">📚 Кредити ({credits.length})</h4>
                <ul className="list-disc list-inside">
                    {credits.map(c => (
                        <li key={c.id}>{c.type}: {c.value} - {c.validatedBy}</li>
                    ))}
                </ul>
            </div>

            <div>
                <h4 className="font-semibold mt-4">🏆 Постижения ({achievements.length})</h4>
                <ul className="list-disc list-inside">
                    {achievements.map(a => (
                        <li key={a.id}>{a.title} – {a.date}</li>
                    ))}
                </ul>
            </div>

            <div>
                <h4 className="font-semibold mt-4">🎯 Цели ({goals.length})</h4>
                <ul className="list-disc list-inside">
                    {goals.map(g => (
                        <li key={g.id}>{g.area}: {g.description}</li>
                    ))}
                </ul>
            </div>

            <div>
                <h4 className="font-semibold mt-4">📅 Събития ({events.length})</h4>
                <ul className="list-disc list-inside">
                    {events.map(e => (
                        <li key={e.id}>{e.title} – {e.date}</li>
                    ))}
                </ul>
            </div>

            <div>
                <h4 className="font-semibold mt-4">⚠️ Санкции ({sanctions.length})</h4>
                <ul className="list-disc list-inside">
                    {sanctions.map(s => (
                        <li key={s.id}>{s.reason} – {s.date}</li>
                    ))}
                </ul>
            </div>

            <div>
                <h4 className="font-semibold mt-4">💡 Интереси ({interests.length})</h4>
                <ul className="list-disc list-inside">
                    {interests.map(i => (
                        <li key={i.id}>{i.name}: {i.description}</li>
                    ))}
                </ul>
            </div>

            <div>
                <h4 className="font-semibold mt-4">📊 Статистика</h4>
                <ul className="list-disc list-inside">
                    <li>Общо кредити: {stats.totalCredits}</li>
                    <li>Брой постижения: {stats.achievementsCount}</li>
                    <li>Брой събития: {stats.eventsCount}</li>
                    <li>Брой цели: {stats.goalsCount}</li>
                    <li>Брой санкции: {stats.sanctionsCount}</li>
                    <li>Брой интереси: {stats.interestsCount}</li>
                </ul>
            </div>
        </div>
    );
}
