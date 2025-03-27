import { useState } from "react";

export default function ProfilePictureUploadForm({ studentId }) {
    const [file, setFile] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!file) {
            alert("⚠️ Моля, изберете файл.");
            return;
        }

        const formData = new FormData();
        formData.append("file", file);

        try {
            const response = await fetch(`/api/Students/${studentId}/upload-profile-picture`, {
                method: "POST",
                body: formData
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText);
            }

            const result = await response.json();
            alert(`✅ Успешно качване!\n📸 Път: ${result.profilePicturePath}`);
        } catch (err) {
            alert("❌ Грешка при качване: " + err.message);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4 p-4 border rounded shadow">
            <h2 className="text-lg font-bold">📸 Качване на профилна снимка</h2>

            <input
                type="file"
                accept="image/*"
                onChange={(e) => setFile(e.target.files[0])}
                className="block"
            />

            <button
                type="submit"
                className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
            >
                Качи
            </button>
        </form>
    );
}
