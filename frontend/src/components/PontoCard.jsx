import React from 'react';
import { useNavigate } from 'react-router-dom';

const PontoCard = ({ ponto }) => {
  const navigate = useNavigate();

  return (
    <div className="ponto-card">
      <div className="ponto-info">

        <h3>{ponto.nome}</h3>
        <p><strong>📍 Localização:</strong> {ponto.localizacao}</p>
        {ponto.cep && (
          <p><strong>📮 CEP:</strong> {ponto.cep}</p>
        )}

        {ponto.cidade && (
          <p className="ponto-cidade">
            🌎 {ponto.cidade.nome} - {ponto.cidade.estado?.sigla}
          </p>
        )}

      </div>
      
      <div className="ponto-actions">

        <button 
          className="btn-detalhes" 
          onClick={() => navigate(`/detalhes/${ponto.idPontosTuristicos}`)}
        >
          Ver Detalhes
        </button>
        
      </div>
    </div>
  );
};

export default PontoCard;