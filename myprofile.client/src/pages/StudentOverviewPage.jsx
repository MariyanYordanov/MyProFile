import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import ProfilePictureUploadForm from "../components/ProfilePictureUploadForm";

export default function StudentOverviewPage() {
    const { id } = useParams();
    const [overview, setOverview] = useState(null);

    const reloadStudent = () => {
        fetch(`/api/students/${id}/overview`)
            .then(res => res.json())
            .then(data => setOverview(data));
    };

    useEffect(() => {
        reloadStudent();
    }, [id]);

    if (!overview) return <p>Зареждане...</p>;

    return (
        <div className="p-4 space-y-6">
            <h1 className="text-2xl font-bold">👤 Профил на {overview.student.fullName}</h1>
            
            {overview.student.profilePicturePath && (
                <img
                    src={`${overview.student.profilePicturePath}?t=${Date.now()}`}
                    alt="Профилна снимка"
                    className="w-48 h-48 object-cover rounded-full border mt-4"
                />
            )}

            <ProfilePictureUploadForm studentId={id} onUpload={reloadStudent} />

            <div className="bg-gray-100 rounded-xl p-4 shadow">
                {overview.stats && (
                    <div className="bg-green-100 rounded-xl p-4 shadow mt-6">
                        <h2 className="text-xl font-semibold mb-2">📊 Статистика</h2>
                        <ul className="list-disc list-inside">
                            <li><strong>Общо кредити:</strong> {overview.stats.totalCredits}</li>
                            <li><strong>Брой постижения:</strong> {overview.stats.achievementsCount}</li>
                            <li><strong>Брой събития:</strong> {overview.stats.eventsCount}</li>
                            <li><strong>Брой цели:</strong> {overview.stats.goalsCount}</li>
                            <li><strong>Брой санкции:</strong> {overview.stats.sanctionsCount}</li>
                            <li><strong>Брой интереси:</strong> {overview.stats.interestsCount}</li>
                        </ul>
                    </div>
                )}

                <p><strong>Клас:</strong> {overview.student.class}</p>
                <p><strong>Специалност:</strong> {overview.student.speciality}</p>
                <p><strong>Среден успех:</strong> {overview.student.averageGrade.toFixed(2)}</p>
                <p><strong>Рейтинг:</strong> {overview.student.rating}</p>
                <p><strong>Ментор:</strong> {overview.student.mentorName || "Няма"}</p>
            </div>
        </div>
    );
}
