import { useState } from "react";

export default function ProfilePictureUploadForm({ studentId, onUpload }) {
    const [file, setFile] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!file) {
            alert("Моля, изберете файл.");
            return;
        }

        const formData = new FormData();
        formData.append("file", file);

        const response = await fetch(`/api/students/${studentId}/profile-picture`, {
            method: "POST",
            body: formData
        });

        if (response.ok) {
            const data = await response.json();
            alert(`✅ Успешно качване!\nПът: ${data.profilePicturePath}`);

            // ☑️ Извикай reloadStudent ако е подаден
            if (onUpload) onUpload();

            // 🧼 Нулирай избрания файл
            setFile(null);
        } else {
            alert("❌ Възникна грешка при качването.");
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-2">
            <label className="block">
                📸 Избери нова профилна снимка:
                <input type="file" onChange={(e) => setFile(e.target.files[0])} />
            </label>
            <button type="submit" className="bg-blue-500 text-white px-4 py-1 rounded">
                Качи
            </button>
        </form>
    );
}
