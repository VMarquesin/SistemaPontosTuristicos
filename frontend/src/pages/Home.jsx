import { useState, useEffect } from 'react';
import { pontoTuristicoService } from '../services/api';
import PontoCard from '../components/PontoCard';

const Home = () => {
const [pontos, setPontos] = useState([]);
  
  const [busca, setBusca] = useState(''); 
  const [filtroAtivo, setFiltroAtivo] = useState(''); 
  const [pagina, setPagina] = useState(1);
  const [total, setTotal] = useState(0);
  const [carregando, setCarregando] = useState(false);

  const carregarPontos = async (termoAtual, paginaAtual) => {
    setCarregando(true);
    try {
      const response = await pontoTuristicoService.listar(termoAtual, paginaAtual);

      setPontos(response.data.dados || []);
      setTotal(response.data.totalRegistros || 0);
      
    } catch (error) {
      console.error("Erro ao buscar a lista da API:", error);

    } finally {
      setCarregando(false);
    }
  };

  useEffect(() => {
    carregarPontos(filtroAtivo, pagina);
  }, [filtroAtivo, pagina]);

  const handleBusca = (e) => {
    e.preventDefault();
    setFiltroAtivo(busca); 
    setPagina(1); 
  };

  return (
    <div className="home-container">
      
      {/* Barra de Pesquisa */}
      <form className="search-box" onSubmit={handleBusca}>

        <input 
          type="text" 
          placeholder="Buscar por nome, descrição ou localização..." 
          value={busca}
          onChange={(e) => setBusca(e.target.value)}
        />
        <button type="submit">Buscar</button>

      </form>

      {/* Listagem dos Cards */}
      <section className="lista-pontos">

        {carregando ? (
          <div className="loading-state">Carregando pontos turísticos...</div>
        ) : pontos.length > 0 ? (
          pontos.map(ponto => (
            <PontoCard key={ponto.id || ponto.idPontosTuristicos} ponto={ponto} />
          ))
        ) : (
          <div className="empty-state">
            <h3>Nenhum ponto turístico encontrado.</h3>
            <p>Tente buscar por outro termo ou cadastre um novo local!</p>
          </div>
        )}

      </section>

      {/* Controles de Paginação */}
      {!carregando && pontos.length > 0 && (
        <footer className="pagination">

          <button 
            disabled={pagina === 1} 
            onClick={() => setPagina(p => p - 1)}
          >
            &laquo; Voltar
          </button>
          
          <span className="page-indicator">Página {pagina}</span>
          
          <button 
            disabled={pontos.length < 5} 
            onClick={() => setPagina(p => p + 1)}
          >
            Avançar &raquo;
          </button>
          
        </footer>
      )}
    </div>
  );
};

export default Home;