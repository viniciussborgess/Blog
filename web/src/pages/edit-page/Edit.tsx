import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const EditPost = () => {
    const [id, setId] = useState<string | null>(null);
    const [title, setTitle] = useState("");
    const [text, setText] = useState("");
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [showPopup, setShowPopup] = useState(false);
    const [popupMessage, setPopupMessage] = useState("");
    const [popupType, setPopupType] = useState<"success" | "error">("success");
    const navigate = useNavigate();
    const token = localStorage.getItem("token");

    useEffect(() => {
        const storedId = localStorage.getItem("postId");
        if (storedId) {
            setId(storedId);
            fetchPost(storedId);
        } else {
            setError("Erro ao carregar postagem.");
            setLoading(false);
        }
    }, []);

    const fetchPost = async (postId: string) => {
        try {
            const response = await fetch(`http://localhost:5079/api/v1/post/${postId}`);
            if (!response.ok) throw new Error("Erro ao carregar postagem.");
            const data = await response.json();
            setTitle(data.titulo);
            setText(data.texto);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    const updatePost = async () => {
        if (!title.trim()) {
            setPopupMessage("O título não pode estar vazio.");
            setPopupType("error");
            setShowPopup(true);
            return;
        }

        try {
            const response = await fetch(`http://localhost:5079/api/v1/post/${id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
                body: JSON.stringify({ titulo: title, texto: text }),
            });

            if (!response.ok) throw new Error("Erro ao atualizar postagem.");

            setPopupMessage("Postagem atualizada com sucesso!");
            setPopupType("success");
            setShowPopup(true);
            setTimeout(() => navigate("/control"),500);
        } catch (err: any) {
            setPopupMessage(err.message);
            setPopupType("error");
            setShowPopup(true);
        }
    };

    return (
        <div className="w-auto h-screen bg-blue-400 flex flex-col overflow-hidden">
            <header className="flex justify-between items-center bg-blue-100 p-4 shadow-md fixed top-0 left-0 right-0 z-10 h-20">
                <div className="flex items-center gap-4">
                    <img src="/logoBaitz.png" alt="Logo" className="h-20 w-20" />
                    <h1 className="text-xl font-bold">Editar Postagem</h1>
                </div>
            </header>

            <div className="flex mt-20 justify-center">
                <div className="bg-white p-6 rounded-lg shadow-lg max-w-2xl w-full">
                    {loading ? (
                        <p className="text-center text-black font-bold text-2xl">Carregando...</p>
                    ) : error ? (
                        <p className="text-center text-red-500">{error}</p>
                    ) : (
                        <>
                            <label className="block mb-2 font-semibold">Título</label>
                            <input
                                type="text"
                                value={title}
                                onChange={(e) => setTitle(e.target.value)}
                                className="w-full p-2 border border-gray-300 rounded-lg mb-4"
                            />

                            <label className="block mb-2 font-semibold">Texto</label>
                            <textarea
                                value={text}
                                onChange={(e) => setText(e.target.value)}
                                className="w-full p-2 border border-gray-300 rounded-lg mb-4"
                                rows={6}
                            />

                            <div className="flex justify-end gap-2">
                                <button
                                    onClick={() => navigate("/control")}
                                    className="px-4 py-2 bg-red-500 text-white rounded-lg hover:bg-red-600"
                                >
                                    Cancelar
                                </button>
                                <button
                                    onClick={updatePost}
                                    className="px-4 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600"
                                >
                                    Confirmar
                                </button>
                            </div>
                        </>
                    )}
                </div>
            </div>

            {showPopup && (
                <div className="fixed inset-0 bg-blue-400 bg-opacity-50 flex justify-center items-center">
                    <div className={`bg-white p-6 rounded-lg shadow-lg max-w-150 w-full ${popupType === "success" ? "bg-green-100" : "bg-red-100"}`}>
                        <p className="text-center text-xl font-semibold">{popupMessage}</p>
                        <div className="flex justify-center mt-4">
                            <button
                                onClick={() => setShowPopup(false)}
                                className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
                            >
                                Fechar
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default EditPost;
