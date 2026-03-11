import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { pontoTuristicoService } from '../services/api';

const Detalhes = () => {
  const { id } = useParams(); 
  const navigate = useNavigate();
  
  const [ponto, setPonto] = useState(null);
  const [carregando, setCarregando] = useState(true);

  useEffect(() => {

    const buscarDetalhes = async () => {

      try {
        const response = await pontoTuristicoService.obterPorId(id);
        setPonto(response.data.dados || response.data);

      } catch (error) {
        console.error("Erro ao buscar detalhes do ponto:", error);

      } finally {
        setCarregando(false);

      }
    };

    buscarDetalhes();
  }, [id]);

  if (carregando) {
    return <div className="loading-state">Buscando detalhes...</div>;
  }

  if (!ponto) {
    return (
      <div className="empty-state">

        <h3>Ponto turístico não encontrado.</h3>
        <button className="btn-voltar" onClick={() => navigate(-1)}>Voltar</button>

      </div>
    );
  }

  return (
    <div className="detalhes-container">
      <h2>{ponto.nome}</h2>
      
      <div className="detalhes-card">
        <p><strong>📍 Localização:</strong> {ponto.localizacao}</p>
        
        {ponto.cidade && (
          <p><strong>🌎 Cidade/UF:</strong> {ponto.cidade.nome} - {ponto.cidade.estado?.sigla}</p>
        )}
        
        <div className="descricao-box">

          <strong>📝 Descrição:</strong>
          <p>{ponto.descricao}</p>

        </div>
        <div className="detalhes-actions">

          <button className="btn-voltar-center" onClick={() => navigate(-1)}>
            &laquo; Voltar para a lista
          </button>
          
        </div>
      </div>
    </div>
  );
};

export default Detalhes;