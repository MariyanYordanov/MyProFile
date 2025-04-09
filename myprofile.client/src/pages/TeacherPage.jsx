import { useAuth } from "@/context/AuthProvider";

export default function TeacherPage() {
    const { user } = useAuth();

    return (
        <div className="max-w-3xl mx-auto mt-10">
            <h1 className="text-2xl font-bold mb-4">👨‍🏫 Добре дошъл, {user.email}</h1>
            <p className="text-gray-600">Това е началната страница за учители.</p>
            {/* Тук можем да добавим: списък с ученици, събития, заявки за верификация и др. */}
        </div>
    );
}
