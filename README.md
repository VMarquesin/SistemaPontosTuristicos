# 🗺️ Sistema de Mapeamento de Pontos Turísticos

Sistema Full-Stack desenvolvido para o gerenciamento e visualização de pontos turísticos, com foco em design moderno, arquitetura limpa e integração dinâmica de dados.

## Tecnologias Utilizadas

### Back-end
* **C# & .NET 10:** API RESTful robusta.
* **Entity Framework Core:** ORM principal (Code-First).
* **Dapper:** Utilizado estrategicamente para consultas de alta performance via Stored Procedures.
* **SQL Server:** Banco de dados relacional.
* **Arquitetura:** Clean Architecture (DDD), Padrão Repository e DTOs.

### Front-end
* **React + Vite:** Interface rápida e componentizada (JavaScript).
* **React Router Dom:** Gerenciamento das rotas (SPA).
* **Axios:** Cliente HTTP para comunicação com a API.
* **CSS Customizado:** Variáveis de design moderno e alinhamento responsivo.

---

## Estrutura do Projeto

### Backend (Clean Architecture)
```text
/Backend
├── PontosTuristicos.Api/             # Controllers, Program.cs e Injeção de Dependência
├── PontosTuristicos.Application/     # Services, DTOs e Casos de Uso
├── PontosTuristicos.Domain/          # Entidades (PontoTuristico, Cidade, Estado) e Interfaces
└── PontosTuristicos.Infrastructure/  # DbContext, Repositórios, Dapper e Migrations 
```

### Frontend (React + Vite)
```text
/frontend
├── src/
│   ├── components/  # Componentes reutilizáveis (PontoCard)
│   ├── pages/       # Telas principais (Home, Cadastro)
│   ├── services/    # Configuração do Axios e chamadas para API
│   └── index.css    # Estilização global e design system
```

### Funcionalidades e Diferenciais
CRUD Completo: Criação, leitura, atualização e exclusão de pontos turísticos.

Soft Delete: Exclusão lógica (Inativação) sem perda de histórico no banco.

Busca Híbrida: Filtros textuais otimizados via Stored Procedure.

Integração IBGE e Padrão Upsert (Diferencial): A tela de Cadastro consome a API Pública do IBGE. A API .NET recebe a localidade e aplica o padrão "Find or Create" (Upsert), inserindo novos Estados e Cidades no banco relacional sob demanda, garantindo integridade sem a necessidade de pré-popular milhares de registros.

### Passo a Passo para Inicialização
Pré-requisitos
* **.NET SDK**

* **Node.js (inclui o NPM)**

* **SQL Server (LocalDB ou instância rodando)**


1. Executando o Back-end (API)
Abra um terminal e navegue até a pasta raiz do Back-end:
```bash
cd Backend
```

Verifique se a string de conexão no arquivo `PontosTuristicos.Api/appsettings.json` aponta para a sua instância local do SQL Server. 

**Exemplo para SQL Server Padrão (LocalDB ou Instância Principal):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=MapeamentoPontosTuristicosDB;Trusted_Connection=True;TrustServerCertificate=True;"
} 
```

**Exemplo para SQL Server Express (Versão Gratuita):**
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=MapeamentoPontosTuristicosDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

Restaure as dependências:
```bash
dotnet restore
```

Inicie a aplicação:
```bash
dotnet run --project PontosTuristicos.Api
```
A API estará rodando em: http://localhost:5247

## Swagger
A documentação da API via Swagger estará disponivel em: http://localhost:5247/swagger/index.html

2. Executando o Front-end (React)
Abra outro terminal e navegue até a pasta do Front-end:
```bash
cd frontend
```
Instale as dependências:
```bash
npm install
```
Inicie o projeto:
```bash
npm run dev
```
A aplicação estará disponível em: http://localhost:5173.
