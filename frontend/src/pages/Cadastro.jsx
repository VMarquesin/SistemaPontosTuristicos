import { useState, useEffect } from 'react';
import { useParams ,useNavigate } from 'react-router-dom';
import { pontoTuristicoService, ibgeService } from '../services/api';

const Cadastro = () => {
  const navigate = useNavigate();
  const [ufs, setUfs] = useState([]);
  const [cidades, setCidades] = useState([]);
  const [loadingCidades, setLoadingCidades] = useState(false);
  const [salvando, setSalvando] = useState(false);
  const { id } = useParams(); 
  const isEdicao = Boolean(id);
  
  const [formData, setFormData] = useState({
    nome: '',
    descricao: '',
    localizacao: '',
    cep: '',
    uf: '',
    cidade: ''
  });

  useEffect(() => {
    ibgeService.buscarEstados()
      .then(res => setUfs(res.data))
      .catch(err => console.error("Erro ao carregar UFs do IBGE", err));
  }, []);

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

      setCidades([]); 
    }

  }, [formData.uf]);

  useEffect(() => {
    if (isEdicao) {
      pontoTuristicoService.obterPorId(id).then(res => {
        const dados = res.data.dados || res.data;
        setFormData({
          nome: dados.nome,
          descricao: dados.descricao,
          localizacao: dados.localizacao,
          cep: dados.cep || '',
          uf: dados.cidade?.estado?.sigla || '',
          cidade: dados.cidade?.nome || ''
        });
      }).catch(err => console.error("Erro ao buscar para edição", err));
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === 'descricao' && value.length > 100) return;
    if (name === 'cep' && value !== '' && !/^\d*$/.test(value)) return;

    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSalvando(true);
    try {
      if (isEdicao) {
        await pontoTuristicoService.atualizar(id, formData);

      } else {
        await pontoTuristicoService.cadastrar(formData);

      }

      navigate('/');
    } catch (error) {

      console.error("Erro ao salvar:", error);
      alert("Erro ao salvar o ponto turístico.");

    } finally {
      setSalvando(false);
    }

  };

  return (
    <div className="cadastro-container">
      <h2>{isEdicao ? 'Editar Ponto Turístico' : 'Novo Ponto Turístico'}</h2>
      
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

        <div className="field" style={{ flex: 1 }}>
            <label>CEP:</label>
            <input 
              type="text" 
              name="cep" 
              value={formData.cep} 
              onChange={handleChange} 
              placeholder="Ex: 17600000"
              maxLength="8"
              inputMode="numeric"
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

          <small style={{ textAlign: 'right', color: 'var(--text-muted)' }}>
            {formData.descricao.length}/100
          </small>
        </div>

        <div className="detalhes-actions" style={{ justifyContent: 'space-between' }}>

          <button type="button" className="btn-voltar-center" onClick={() => navigate(-1)}>
            Cancelar
          </button>
          
          <button type="submit" className="btn-main-action" disabled={salvando}>
            {salvando ? 'Salvando...' : isEdicao ? 'Salvar Alterações' : 'Cadastrar Ponto'}
          </button>

        </div>
      </form>
    </div>
  );
};

export default Cadastro;