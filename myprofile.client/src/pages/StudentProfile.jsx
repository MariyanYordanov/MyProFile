import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "axios";

export default function StudentProfile() {
    const { id } = useParams();
    const [student, setStudent] = useState(null);

    useEffect(() => {
        axios.get(`/api/students/${id}`).then(res => setStudent(res.data));
    }, [id]);

    if (!student) return <div>Зареждане...</div>;

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold">{student.fullName}</h1>
            <p>Клас: {student.class}</p>
            <p>Специалност: {student.speciality}</p>
            <p>Среден успех: {student.averageGrade}</p>
            <img src={`/profiles/${student.profilePicturePath}`} alt="Профилна снимка" className="mt-4 w-40 h-40 object-cover rounded-full" />
        </div>
    );
}
