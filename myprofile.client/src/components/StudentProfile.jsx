import { useParams } from "react-router-dom";
import StudentOverviewPage from "../pages/StudentOverviewPage";

export default function StudentProfile() {
    const { id } = useParams();

    return (
        <div className="p-6">
            <h2 className="text-2xl font-semibold mb-4">Профил на ученик #{id}</h2>
            <StudentOverviewPage studentId={id} />
        </div>
    );
}
