import React from 'react';
import { useNavigate } from 'react-router-dom';
import { pontoTuristicoService } from '../services/api';

const PontoCard = ({ ponto }) => {
  const navigate = useNavigate();

  const handleInativar = async () => {
    if(window.confirm(`Deseja realmente inativar o ponto '${ponto.nome}'?`)) {
      try {
        await pontoTuristicoService.inativar(ponto.idPontosTuristicos);
  
        alert("Ponto turístico inativado com sucesso.");
        window.location.reload(); 
      } catch (error) {

        alert("Falha ao inativar o ponto turístico. Verifique a conexão.");
      }
    }
  };

  return (
    <div className="ponto-card">
      <div className="ponto-info">
        <h3>{ponto.nome}</h3>
        
        <p>
          📍 <strong>Localização:</strong> {ponto.localizacao}
        </p>
        
        {ponto.cep && (
          <p>
            📮 <strong>CEP:</strong> {ponto.cep}
          </p>
        )}

        {ponto.cidade && (
          <p className="ponto-cidade">
            🌎 {ponto.cidade.nome} - {ponto.cidade.estado?.sigla}
          </p>
        )}
      </div>
      
      {/* Grupo de botões limpo e padronizado */}
      <div className="ponto-actions">
        <button className="btn-action btn-inativar" onClick={handleInativar}>
          Inativar
        </button>
        
        <button className="btn-action btn-editar" onClick={() => navigate(`/editar/${ponto.idPontosTuristicos}`)}>
          Editar
        </button>
        
        <button className="btn-action btn-detalhes" onClick={() => navigate(`/detalhes/${ponto.idPontosTuristicos}`)}>
          Ver Detalhes
        </button>
      </div>
    </div>
  );
};

export default PontoCard;