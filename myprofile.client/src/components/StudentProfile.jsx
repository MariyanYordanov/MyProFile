import { useEffect, useState } from "react";

export default function StudentProfile({ studentId }) {
    const [student, setStudent] = useState(null);
    const [goals, setGoals] = useState([]);
    const [achievements, setAchievements] = useState([]);
    const [credits, setCredits] = useState([]);
    const [events, setEvents] = useState([]);
    const [sanctions, setSanctions] = useState([]);
    const [interests, setInterests] = useState([]);

    useEffect(() => {
        fetchData();
    }, [studentId]);

    const fetchData = async () => {
        const [studentData, g, a, c, e, s, i] = await Promise.all([
            fetch(`/api/Students/${studentId}`).then(r => r.json()),
            fetch(`/api/Goals?studentId=${studentId}`).then(r => r.json()),
            fetch(`/api/Achievements?studentId=${studentId}`).then(r => r.json()),
            fetch(`/api/Credits?studentId=${studentId}`).then(r => r.json()),
            fetch(`/api/Events?studentId=${studentId}`).then(r => r.json()),
            fetch(`/api/Sanctions?studentId=${studentId}`).then(r => r.json()),
            fetch(`/api/Interests?studentId=${studentId}`).then(r => r.json())
        ]);
        setStudent(studentData);
        setGoals(g);
        setAchievements(a);
        setCredits(c);
        setEvents(e);
        setSanctions(s);
        setInterests(i);
    };

    if (!student) return <p>Зареждане на профила...</p>;

    return (
        <div>
            <h1>{student.fullName}</h1>
            <img src={student.profilePicturePath} alt="Profile" width="150" />
            <p>Клас: {student.class}</p>
            <p>Специалност: {student.speciality}</p>

            <hr />
            <h2>🎯 Цели</h2>
            <ul>{goals.map(g => <li key={g.id}>{g.area}: {g.description}</li>)}</ul>

            <h2>🏆 Постижения</h2>
            <ul>{achievements.map(a => <li key={a.id}>{a.title} – {a.date}</li>)}</ul>

            <h2>🧾 Кредити</h2>
            <ul>{credits.map(c => <li key={c.id}>{c.type} – {c.value} (валидирани от {c.validatedBy})</li>)}</ul>

            <h2>📅 Събития</h2>
            <ul>{events.map(e => <li key={e.id}>{e.title} – {e.date}</li>)}</ul>

            <h2>🚫 Санкции</h2>
            <ul>{sanctions.map(s => <li key={s.id}>{s.reason} – {s.date}</li>)}</ul>

            <h2>❤️ Интереси</h2>
            <ul>{interests.map(i => <li key={i.id}>{i.name}</li>)}</ul>
        </div>
    );
}
