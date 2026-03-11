import axios from 'axios';

// Sua API
const api = axios.create({
  baseURL: 'http://localhost:5247/api' 
});

// Serviço para o IBGE 
const ibgeApi = axios.create({
  baseURL: 'https://servicodados.ibge.gov.br/api/v1/localidades'
});

export const pontoTuristicoService = {
  listar: (termo = '', pagina = 1) => 
    api.get(`/pontosturisticos?termoBusca=${termo}&pagina=${pagina}&tamanhoPagina=5`),
  
  obterPorId: (id) => api.get(`/pontosturisticos/${id}`),
  cadastrar: (dados) => api.post('/pontosturisticos', dados)
};

export const ibgeService = {
  buscarEstados: () => ibgeApi.get('/estados?orderBy=nome'),
  buscarCidades: (uf) => ibgeApi.get(`/estados/${uf}/municipios?orderBy=nome`)
};

