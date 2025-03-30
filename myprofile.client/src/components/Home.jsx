import { Link } from "react-router-dom";

export default function Home() {
    return (
        <div className="text-center p-10">
            <h1 className="text-3xl font-bold mb-4">Добре дошли в MyProFile</h1>
            <p className="mb-6">Платформа за проследяване на постижения, интереси и напредък на учениците.</p>
            <div className="space-x-4">
                <Link to="/login" className="bg-blue-600 text-white py-2 px-4 rounded">Вход</Link>
                <Link to="/register" className="bg-gray-300 py-2 px-4 rounded">Регистрация</Link>
            </div>
        </div>
    );
}
