import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

export default function StudentOverviewPage() {
    const { id } = useParams();
    const [overview, setOverview] = useState(null);

    useEffect(() => {
        fetch(`/api/Students/${id}/overview`)
            .then(res => res.json())
            .then(data => setOverview(data));
    }, [id]);

    if (!overview) return <p>Зареждане...</p>;

    return (
        <div className="p-4 space-y-6">
            <h1 className="text-2xl font-bold">👤 Профил на {overview.student.fullName}</h1>

            <div className="bg-gray-100 rounded-xl p-4 shadow">
                <p><strong>Клас:</strong> {overview.student.class}</p>
                <p><strong>Специалност:</strong> {overview.student.speciality}</p>
                <p><strong>Среден успех:</strong> {overview.student.averageGrade.toFixed(2)}</p>
                <p><strong>Рейтинг:</strong> {overview.student.rating}</p>
                <p><strong>Ментор:</strong> {overview.student.mentorName || "Няма"}</p>
            </div>

            <Section title="🏆 Постижения" items={overview.achievements} render={a => (
                <li key={a.id}>{a.title} – {a.date.split('T')[0]}</li>
            )} />

            <Section title="🎯 Цели" items={overview.goals} render={g => (
                <li key={g.id}>{g.area} – {g.description}</li>
            )} />

            <Section title="📅 Събития" items={overview.events} render={e => (
                <li key={e.id}>{e.title} – {e.date.split('T')[0]}</li>
            )} />

            <Section title="🎓 Кредити" items={overview.credits} render={c => (
                <li key={c.id}>{c.type}: {c.value} ({c.validatedBy})</li>
            )} />

            <Section title="⚠️ Санкции" items={overview.sanctions} render={s => (
                <li key={s.id}>{s.reason} – {s.date.split('T')[0]}</li>
            )} />

            <Section title="💡 Интереси" items={overview.interests} render={i => (
                <li key={i.id}>{i.name}: {i.description}</li>
            )} />
        </div>
    );
}

function Section({ title, items, render }) {
    if (!items.length) return null;

    return (
        <div>
            <h2 className="text-xl font-semibold mt-4">{title}</h2>
            <ul className="list-disc list-inside">{items.map(render)}</ul>
        </div>
    );
}
