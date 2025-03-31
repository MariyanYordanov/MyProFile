import { Link } from "react-router-dom";
export default function Home() {
    return (
        <div className="text-center p-10">
            <h1 className="text-4xl font-bold">Добре дошли в MyProFile</h1>
            <p className="mt-4">Проследявайте развитието си, качвайте постижения и проекти</p>
            <Link to="/login" className="mt-6 inline-block text-blue-600 underline">
                Вход
            </Link>
        </div>
    );
}
