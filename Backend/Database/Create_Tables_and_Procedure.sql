/* =========================================================================
    Script de Referência - TurisMap
    Autor: MARQUESIN, Vinicius
   ========================================================================= */

-- Criação de Tabelas 

CREATE TABLE Estados (
    Sigla VARCHAR(2)   PRIMARY KEY,
    Nome  VARCHAR(50) NOT NULL
);

CREATE TABLE Cidades (
    Id       INT IDENTITY(1,1) PRIMARY KEY,
    Nome     VARCHAR(100)      NOT NULL,
    EstadoId VARCHAR(2)        NOT NULL,
    CONSTRAINT FK_Cidades_Estados FOREIGN KEY (EstadoId) REFERENCES Estados(Sigla)
);

CREATE TABLE PontosTuristicos (
    IdPontosTuristicos INT IDENTITY(1,1) PRIMARY KEY,
    Nome               VARCHAR(255)     NOT NULL,
    Descricao          VARCHAR(100)     NULL,
    Localizacao        VARCHAR(255)     NOT NULL,
    Cep                VARCHAR(9)       NULL,
    DataInclusao       DATETIME         NOT NULL DEFAULT GETDATE(),
    Ativo              BIT              NOT NULL DEFAULT TRUE,
    CidadeId           INT              NOT NULL,
    CONSTRAINT FK_PontosTuristicos_Cidades FOREIGN KEY (CidadeId) REFERENCES Cidades(Id)
);
    
GO

-- Criação da Stored Procedure
      
CREATE PROCEDURE stp_BuscarPontosTuristicos
                    @TermoBusca    VARCHAR(100),
                    @Pagina        INT,
                    @TamanhoPagina INT

AS
BEGIN

    SET NOCOUNT ON;
    
    DECLARE 
    @Offset INT = (@Pagina - 1) * @TamanhoPagina;

    SELECT PTU.*, 
           CID.Id, 
           CID.Nome, 
           CID.EstadoSigla, 
           EST.Sigla, 
           EST.Nome
      FROM PontosTuristicos AS PTU
INNER JOIN Cidades CID      ON PTU.IdCidade    = CID.Id
INNER JOIN Estados EST      ON CID.EstadoSigla = EST.Sigla
     WHERE PTU.Ativo = 1
       AND (
            @TermoBusca     IS NULL OR @TermoBusca = ''  OR
            PTU.Nome        LIKE '%' + @TermoBusca + '%' OR 
            PTU.Descricao   LIKE '%' + @TermoBusca + '%' OR 
            PTU.Localizacao LIKE '%' + @TermoBusca + '%'
           )
  ORDER BY PTU.DataInclusao DESC

    OFFSET @Offset ROWS
FETCH NEXT @TamanhoPagina ROWS ONLY;

END
    
GO