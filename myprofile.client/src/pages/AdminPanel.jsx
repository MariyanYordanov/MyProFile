import CreditUploadForm from "../components/CreditUploadForm";
import CreditList from "../components/CreditList";
import ProjectUploadForm from "../components/ProjectUploadForm";
import EventUploadForm from "../components/EventUploadForm";
import GoalForm from "../components/GoalForm";
import SanctionForm from "../components/SanctionForm";
import InterestForm from "../components/InterestForm";

export default function AdminPanel() {
    return (
        <div className="space-y-6">
            <h1 className="text-2xl font-bold">🛠️ Админ панел</h1>

            <section>
                <h2 className="font-semibold">📤 Качване на кредит</h2>
                <CreditUploadForm />
                <CreditList />
            </section>

            <section>
                <h2 className="font-semibold">📁 Качване на проект</h2>
                <ProjectUploadForm />
            </section>

            <section>
                <h2 className="font-semibold">📅 Качване на събитие</h2>
                <EventUploadForm />
            </section>

            <section>
                <h2 className="font-semibold">🎯 Цел</h2>
                <GoalForm />
            </section>

            <section>
                <h2 className="font-semibold">🛑 Санкция</h2>
                <SanctionForm />
            </section>

            <section>
                <h2 className="font-semibold">💡 Интерес</h2>
                <InterestForm />
            </section>
        </div>
    );
}
