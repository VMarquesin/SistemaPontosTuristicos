# SistemaPontosTuristicos
# 🗺️ TurisMap - Gestão de Pontos Turísticos

Este é um sistema completo (Full-Stack) desenvolvido para o gerenciamento e visualização de pontos turísticos. O projeto permite a listagem, busca textual, visualização de detalhes e o cadastro de novos pontos com integração dinâmica de localidades.

## 🚀 Tecnologias Utilizadas

### Back-end
* **C# & .NET:** API RESTful robusta.
* **Entity Framework Core:** ORM para mapeamento das entidades e persistência.
* **Dapper (Micro ORM):** Utilizado para consultas complexas e de alta performance.
* **SQL Server:** Banco de dados relacional.
* **Padrões e Arquitetura:** Injeção de Dependência, Padrão Repository, DTOs (Data Transfer Objects).

### Front-end
* **React + Vite:** Interface rápida e componentizada.
* **React Router Dom:** Gerenciamento das rotas da aplicação (SPA).
* **Axios:** Cliente HTTP para comunicação com a API e serviços externos.

---

## 🧠 Destaque Arquitetural: Integração IBGE e Padrão Upsert

### 1. CQRS Simplificado (EF Core + Dapper)
Para otimizar a performance da aplicação, adotamos uma abordagem híbrida de acesso a dados:
* **Escritas (Commands):** Utilizam o Entity Framework Core para garantir o rastreamento de estado e validações ricas de domínio.
* **Leituras (Queries):** A busca principal de pontos turísticos (`ObterTodosAsync`) consome uma **Stored Procedure** (`stp_BuscarPontosTuristicos`) executada via **Dapper**. A procedure foi embutida diretamente nas Migrations do EF, garantindo que o banco de dados seja versionado por completo.

### 2. Integração IBGE e Padrão Upsert (Find or Create)
Para garantir a integridade dos dados e uma excelente experiência de usuário, a tela de Cadastro consome a **API Pública do IBGE**.
No Back-end, a API recebe os textos da localidade e, através do Entity Framework, verifica se a Cidade/Estado já existem no banco. Se não existirem, o sistema cria a entidade sob demanda e a vincula ao novo Ponto Turístico.

---

## ⚙️ Pré-requisitos

Para rodar o projeto localmente, você precisará ter instalado:
* [.NET SDK](https://dotnet.microsoft.com/download)
* [Node.js](https://nodejs.org/) (inclui o NPM)
* [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (LocalDB ou instância rodando)

---

## 🛠️ Passo a Passo para Inicialização

### 1. Configurando o Back-end (API)

Abra um terminal e navegue até a pasta do Back-end:
```bash
cd backend

Verifique se a string de conexão no arquivo appsettings.json aponta para o seu SQL Server local. Exemplo:

JSON
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MapeamentoPontosTuristicosDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

Rode as Migrations para criar o banco de dados e as tabelas (o banco iniciará vazio):

Bash
dotnet ef database update
Inicie a API:

Bash
dotnet run
A API estará rodando geralmente na porta http://localhost:5247.

2. Configurando o Front-end (React)
Abra outro terminal e navegue até a pasta do Front-end:

Bash
cd frontend
Instale as dependências do projeto:

Bash
npm install
Inicie o servidor de desenvolvimento:

Bash
npm run dev
O Front-end estará rodando em http://localhost:5173.

🧪 Como testar a aplicação?
Acesse o Front-end e clique em "Cadastrar um ponto turístico".

Preencha os dados e utilize o dropdown em cascata para selecionar Estado e Cidade (integrado ao IBGE).

Após salvar, retorne à Home para ver o Ponto Turístico listado.

Utilize a barra de Busca para testar os filtros textuais.

Clique em "Ver Detalhes" para checar a rota dinâmica.