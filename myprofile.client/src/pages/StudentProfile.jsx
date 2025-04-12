import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import api from "@/services/api";

export default function StudentProfile() {
    const { id } = useParams();
    const [student, setStudent] = useState(null);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchStudent = async () => {
            try {
                const res = await api.get(`/students/${id}`);
                setStudent(res.data);
            } catch (err) {
                console.error("Грешка при зареждане на профила:", err);
                setError("Неуспешно зареждане на профил.");
            }
        };

        fetchStudent();
    }, [id]);

    if (error) return <div className="text-red-500">{error}</div>;
    if (!student) return <div>Зареждане...</div>;

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold">{student.fullName}</h1>
            <p>Клас: {student.class}</p>
            <p>Специалност: {student.speciality}</p>
            <p>Среден успех: {student.averageGrade}</p>
            <img
                src={`/profiles/${student.profilePicturePath}`}
                alt="Профилна снимка"
                className="mt-4 w-40 h-40 object-cover rounded-full"
            />
        </div>
    );
}
