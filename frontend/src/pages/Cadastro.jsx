import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { pontoTuristicoService, ibgeService } from '../services/api';

const Cadastro = () => {
  const navigate = useNavigate();
  const [ufs, setUfs] = useState([]);
  const [cidades, setCidades] = useState([]);
  const [loadingCidades, setLoadingCidades] = useState(false);
  const [salvando, setSalvando] = useState(false);
  
  const [formData, setFormData] = useState({
    nome: '',
    descricao: '',
    localizacao: '',
    uf: '',
    cidade: ''
  });

  // 1. Carrega os Estados (UFs) assim que a tela abre
  useEffect(() => {
    ibgeService.buscarEstados()
      .then(res => setUfs(res.data))
      .catch(err => console.error("Erro ao carregar UFs do IBGE", err));
  }, []);

  // 2. O Efeito Cascata: Escuta as mudanças no campo 'uf'
  useEffect(() => {
    if (formData.uf) {
      setLoadingCidades(true);
      ibgeService.buscarCidades(formData.uf)
        .then(res => {
          setCidades(res.data);
          setLoadingCidades(false);
        })
        .catch(err => console.error("Erro ao carregar Cidades do IBGE", err));
    } else {
      setCidades([]); // Limpa a lista de cidades se o usuário desmarcar a UF
    }
  }, [formData.uf]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    
    // Regra de Negócio: Trava a digitação se passar de 100 caracteres
    if (name === 'descricao' && value.length > 100) return;

    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSalvando(true);
    try {
      // Dispara o POST para o nosso C#
      await pontoTuristicoService.cadastrar(formData);
      
      // Volta para a Home suavemente após o sucesso (sem alerts chatos)
      navigate('/');
    } catch (error) {
      console.error("Erro no cadastro:", error);
      alert("Erro ao cadastrar o ponto turístico. Verifique a conexão com a API.");
    } finally {
      setSalvando(false);
    }
  };

  return (
    <div className="cadastro-container">
      <h2>Novo Ponto Turístico</h2>
      
      <form onSubmit={handleSubmit} className="form-cadastro">
        <div className="field">
          <label>Nome do Ponto:</label>
          <input 
            type="text" 
            name="nome" 
            value={formData.nome} 
            onChange={handleChange} 
            placeholder="Ex: Cristo Redentor"
            required 
          />
        </div>

        <div className="field">
          <label>Localização (Endereço ou Referência):</label>
          <input 
            type="text" 
            name="localizacao" 
            value={formData.localizacao} 
            onChange={handleChange} 
            placeholder="Ex: Parque Nacional da Tijuca"
            required 
          />
        </div>

        <div className="row">
          <div className="field">
            <label>Estado (UF):</label>
            <select name="uf" value={formData.uf} onChange={handleChange} required>
              <option value="">Selecione o Estado</option>
              {ufs.map(uf => (
                <option key={uf.id} value={uf.sigla}>{uf.sigla} - {uf.nome}</option>
              ))}
            </select>
          </div>

          <div className="field">
            <label>Cidade:</label>
            <select 
              name="cidade" 
              value={formData.cidade} 
              onChange={handleChange} 
              disabled={!formData.uf || loadingCidades}
              required
            >
              <option value="">
                {loadingCidades ? 'Carregando cidades...' : 'Selecione a Cidade'}
              </option>
              {cidades.map(c => (
                <option key={c.id} value={c.nome}>{c.nome}</option>
              ))}
            </select>
          </div>
        </div>

        <div className="field">
          <label>Descritivo:</label>
          <textarea 
            name="descricao" 
            value={formData.descricao} 
            onChange={handleChange} 
            placeholder="Conte um pouco sobre este lugar..."
            required
          />
          {/* Contador de caracteres elegante */}
          <small style={{ textAlign: 'right', color: 'var(--text-muted)' }}>
            {formData.descricao.length}/100
          </small>
        </div>

        <div className="detalhes-actions" style={{ justifyContent: 'space-between' }}>
          <button type="button" className="btn-voltar-center" onClick={() => navigate(-1)}>
            Cancelar
          </button>
          
          <button type="submit" className="btn-main-action" disabled={salvando}>
            {salvando ? 'Salvando...' : 'Cadastrar Ponto'}
          </button>
        </div>
      </form>
    </div>
  );
};

export default Cadastro;