import { Link } from "react-router-dom";

export default function AdminPanel() {
    return (
        <div className="p-6">
            <h2 className="text-2xl font-bold mb-4">Админ панел</h2>
            <div className="space-y-3">
                <Link to="/admin/send-invite" className="block text-blue-600 underline">Изпращане на покана</Link>
                <Link to="/students/1/overview" className="block text-blue-600 underline">Преглед на ученик</Link>
                {/* Добави още линкове при нужда */}
            </div>
        </div>
    );
}