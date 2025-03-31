import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

type Post = {
    id: number;
    titulo: string;
    dataPostagem: string;
};

const ControlPost = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [showCreateModal, setShowCreateModal] = useState(false); // Modal de criação
    const [showDeleteModal, setShowDeleteModal] = useState(false); // Modal de exclusão
    const [title, setTitle] = useState("");
    const [text, setText] = useState("");
    const [showPopup, setShowPopup] = useState(false);
    const [popupMessage, setPopupMessage] = useState("");
    const [popupType, setPopupType] = useState<"success" | "error">("success");
    const [postIdToDelete, setPostIdToDelete] = useState<number | null>(null); 
    const navigate = useNavigate();
    const token = localStorage.getItem("token");

    const fetchPosts = async () => {
        try {
            const response = await fetch("http://localhost:5079/api/v1/post");
            if (!response.ok) throw new Error("Erro ao buscar posts.");
    
            const text = await response.text();
    
            if (!text.trim()) {
                setPosts([]);
                return;
            }
    
            const data: Post[] = JSON.parse(text);

            const sortByDate = data.sort((a, b) => {
                const dateA = new Date(a.dataPostagem);
                const dateB = new Date(b.dataPostagem);
                return dateB.getTime() - dateA.getTime();
            });
    
            setPosts(sortByDate);
        } catch (err: any) {
            setError("Erro ao carregar posts. Tente novamente mais tarde.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);

    const deletePost = async () => {
        if (postIdToDelete === null) return;

        try {
            const response = await fetch(`http://localhost:5079/api/v1/post/${postIdToDelete}`, {
                method: "DELETE",
                headers: { "Authorization": `Bearer ${token}` }
            });

            if (!response.ok) throw new Error("Erro ao excluir postagem.");

            setPosts(posts.filter(post => post.id !== postIdToDelete));
            setShowDeleteModal(false); // Fechar o modal após confirmação
            setPopupMessage("Postagem excluída com sucesso!");
            setPopupType("success");
            setShowPopup(true);
        } catch (err: any) {
            setPopupMessage(err.message);
            setPopupType("error");
            setShowPopup(true);
        }
    };

    const createPost = async () => {
        if (!title.trim()) {
            setPopupMessage("O título não pode estar vazio.");
            setPopupType("error");
            setShowPopup(true);
            return;
        }

        if (title.length > 255) {
            setPopupMessage("O título pode ter no máximo 255 caracteres.");
            setPopupType("error");
            setShowPopup(true);
            return;
        }

        try {
            const response = await fetch("http://localhost:5079/api/v1/post", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
                body: JSON.stringify({ titulo: title, texto: text }),
            });

            if (!response.ok) throw new Error("Erro ao criar postagem.");

            setPopupMessage("Postagem criada com sucesso!");
            setPopupType("success");
            setShowPopup(true);
            fetchPosts();

            setShowCreateModal(false);
            setTitle("");
            setText("");
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
                    <h1 className="text-xl font-bold">Gerenciar Postagens</h1>
                </div>

                <div className="flex items-center gap-4">
                    <button
                        onClick={() => navigate("/")}
                        className="cursor-pointer bg-blue-500 px-4 py-2 rounded-lg hover:bg-blue-600 transition text-white font-bold"
                    >
                        Voltar
                    </button>
                </div>
            </header>

            <div className="flex mt-16">
                <div className="p-6 w-full mx-auto max-w-3xl">
                    <div className="mb-4">
                        <h2 className="text-2xl font-bold flex flex-row justify-center">Lista de Postagens</h2>
                        <hr className="border-t-2 mt-1" />
                    </div>

                    <button
                        onClick={() => setShowCreateModal(true)}
                        className="flex flex-row justify-end mb-4 px-4 py-2 text-white bg-green-500 font-bold rounded-lg hover:bg-green-600"
                    >
                        Criar
                    </button>

                    {loading ? (
                        <p className="text-center text-black font-bold text-2xl">Carregando...</p>
                    ) : error ? (
                        <p className="text-center text-red-500">{error}</p>
                    ) : posts.length === 0 ? (
                        <p className="text-center text-black">Nenhuma postagem encontrada.</p>
                    ) : (
                        <ul className="w-full bg-white p-4 rounded-lg shadow-md overflow-y-auto max-h-[400px]">
                            {posts.map((post) => (
                                <li key={post.id} className="flex justify-between items-center border-b p-3 last:border-none">
                                    <span className="font-semibold text-lg break-words overflow-hidden">{post.titulo}</span>
                                    <div className="flex gap-2">
                                        <button
                                            onClick={() => {
                                                localStorage.setItem("postId", post.id.toString());
                                                navigate(`/edit`);
                                            }}
                                            className="px-3 py-1 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
                                        >
                                            Editar
                                        </button>
                                        <button
                                            onClick={() => { setPostIdToDelete(post.id); setShowDeleteModal(true); }}
                                            className="px-3 py-1 bg-red-500 text-white rounded-lg hover:bg-red-600"
                                        >
                                            Excluir
                                        </button>
                                    </div>
                                </li>
                            ))}
                        </ul>
                    )}
                </div>
            </div>

            {showPopup && (
                <div className="fixed inset-0 bg-blue-400 bg-opacity-50 flex justify-center items-center">
                    <div
                        className={`bg-white p-6 rounded-lg shadow-lg max-w-150 w-full ${popupType === "success" ? "bg-green-100" : "bg-red-100"}`}
                    >
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

            {showCreateModal && (
                <div className="fixed inset-0 bg-blue-400 bg-opacity-50 flex justify-center items-center">
                    <div className="bg-white p-6 rounded-lg shadow-lg max-w-300 w-full">
                        <h2 className="text-xl font-bold mb-4">Criar Nova Postagem</h2>

                        <label className="block mb-2 font-semibold">Título (máx. 255 caracteres)</label>
                        <input
                            type="text"
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                            maxLength={255}
                            className="w-2/4 p-2 border border-gray-300 rounded-lg mb-4"
                            placeholder="Insira o Título"
                        />

                        <label className="block mb-2 font-semibold">Texto</label>
                        <textarea
                            value={text}
                            onChange={(e) => setText(e.target.value)}
                            className="w-full p-2 border border-gray-300 rounded-lg mb-4"
                            rows={4}
                        />

                        <div className="flex justify-end gap-2">
                            <button
                                onClick={() => setShowCreateModal(false)}
                                className="px-4 py-2 bg-red-500 text-white rounded-lg hover:bg-red-600"
                            >
                                Cancelar
                            </button>
                            <button
                                onClick={createPost}
                                className="px-4 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600"
                            >
                                Criar
                            </button>
                        </div>
                    </div>
                </div>
            )}

            {showDeleteModal && (
                <div className="fixed inset-0 bg-blue-400 bg-opacity-50 flex justify-center items-center">
                    <div className="bg-white p-6 rounded-lg shadow-lg max-w-150 w-full flex flex-col items-center">
                        <h2 className="text-xl font-bold mb-4">Tem certeza que deseja excluir esta postagem?</h2>

                        <div className="flex justify-end gap-2">
                            <button
                                onClick={() => setShowDeleteModal(false)} 
                                className="px-4 py-2 bg-red-500 text-white rounded-lg hover:bg-red-600"
                            >
                                Cancelar
                            </button>
                            <button
                                onClick={deletePost}
                                className="px-4 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600"
                            >
                                Confirmar
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default ControlPost;
