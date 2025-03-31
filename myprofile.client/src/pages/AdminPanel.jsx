import { useEffect, useState } from "react";
import axios from "axios";

export default function AdminPanel() {
    const [students, setStudents] = useState([]);

    useEffect(() => {
        axios.get("/students").then(res => setStudents(res.data));
    }, []);

    return (
        <div className="p-6">
            <h1 className="text-2xl font-semibold">Административен панел</h1>
            <ul className="mt-4 list-disc pl-6">
                {students.map((s) => (
                    <li key={s.id}>{s.fullName}</li>
                ))}
            </ul>
        </div>
    );
}