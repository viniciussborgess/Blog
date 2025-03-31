import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

type Post = {
    id: number;
    titulo: string;
    texto: string;
    dataPostagem: string;
};

function Home() {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [token, setToken] = useState<string | null>(localStorage.getItem("token"));
    const [showDashboard, setShowDashboard] = useState(false);
    const navigate = useNavigate();

    const formatarData = (dataP: string) => {
        const data = new Date(dataP);
        return data.toLocaleDateString("pt-BR", { day: "2-digit", month: "2-digit", year: "numeric" });
    };

    useEffect(() => {
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
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchPosts();
    }, []);

    const Logout = () => {
        localStorage.removeItem("token");
        setToken(null);
        navigate("/login");
    };

    return (
        <div className="w-auto h-screen bg-blue-400 flex flex-col overflow-hidden">
            <header className="flex justify-between items-center bg-blue-100 p-4 shadow-md fixed top-0 left-0 right-0 z-10 h-20">
                <div className="flex items-center gap-4">
                    <img src="/logoBaitz.png" alt="Logo" className="h-20 w-20" />
                    <h1 className="text-xl font-bold">Blog Baitz</h1>
                </div>

                <div className="flex items-center gap-4">
                    {token && (
                        <button
                            onClick={() => setShowDashboard(!showDashboard)}
                            className="cursor-pointer bg-blue-500 px-4 py-2 rounded-lg hover:bg-blue-600 transition text-white font-bold"
                        >
                            {showDashboard ? "Fechar Menu" : "Menu"}
                        </button>
                    )}

                    {token ? (
                        <button
                            onClick={Logout}
                            className="cursor-pointer bg-red-500 px-4 py-2 rounded-lg hover:bg-red-600 transition text-white font-bold"
                        >
                            Logout
                        </button>
                    ) : (
                        <button
                            onClick={() => navigate("/login")}
                            className="cursor-pointer bg-blue-600 px-4 py-2 rounded-lg hover:bg-blue-800 transition font-bold text-white"
                        >
                            Login
                        </button>
                    )}
                </div>
            </header>

            <div className="flex mt-16">
                <div className={`bg-blue-950 text-white p-4 h-screen fixed left-0 top-16 shadow-lg transition-all duration-300 ${showDashboard ? "w-60" : "w-0"
                    } overflow-hidden`}>
                    {token && showDashboard && (
                        <div className="flex flex-col gap-10 mt-10">
                            <button
                                onClick={() => navigate("/control")}
                                className="cursor-pointer bg-blue-500 px-4 py-2 rounded-lg hover:bg-blue-600 transition shadow-md"
                            >
                                Gerenciar Postagens
                            </button>
                        </div>
                    )}
                </div>

                <div
                    className={`p-6 overflow-y-auto max-h-screen transition-all duration-300 ${showDashboard ? "ml-60 w-[calc(100%-60px)]" : "ml-0 w-full"
                        }`}
                >
                    {loading ? (
                        <p className="text-center text-black font-bold text-md mt-4">Carregando posts...</p>
                    ) : error ? (
                        <p className="text-center text-red-500 mt-4">{error}</p>
                    ) : posts.length === 0 ? (
                        <p className="text-center text-black mt-4">Nenhum post encontrado.</p>
                    ) : (
                        <div className="mt-6 flex flex-col items-center">
                            {posts.map((post) => (
                                <div key={post.id} className="w-3/4 bg-gray-100 p-4 rounded-lg shadow-md mb-4">
                                    <h2 className="text-xl font-semibold">{post.titulo}</h2>
                                    <div className="bg-gray-200 rounded-md min-h-10">
                                        <div className="ml-2">
                                            <p className="mt-2 whitespace-pre-line">{post.texto}</p>
                                        </div>
                                    </div>
                                    <p className="mt-2">{formatarData(post.dataPostagem)}</p>
                                </div>
                            ))}
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default Home;
