using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontosTuristicos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterarProcedureParaDapper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var scriptProcedure = @"
            ALTER PROCEDURE stp_BuscarPontosTuristicos
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
                        @TermoBusca IS NULL OR @TermoBusca = '' OR
                        PTU.Nome LIKE '%' + @TermoBusca + '%' OR 
                        PTU.Descricao LIKE '%' + @TermoBusca + '%' OR 
                        PTU.Localizacao LIKE '%' + @TermoBusca + '%'
                       )
              ORDER BY PTU.DataInclusao DESC

                OFFSET @Offset ROWS
            FETCH NEXT @TamanhoPagina ROWS ONLY;

            END";

            migrationBuilder.Sql(scriptProcedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var scriptAntecessor = @"

            CREATE PROCEDURE stp_BuscarPontosTuristicos
                             @TermoBusca    VARCHAR(100),
                             @Pagina        INT,
                             @TamanhoPagina INT
        
            AS
            BEGIN
            SET NOCOUNT ON;

                DECLARE @Offset INT = (@Pagina - 1) * @TamanhoPagina;

                SELECT *
                  FROM PontosTuristicos
                 WHERE Ativo = 1
                   AND (
                        @TermoBusca IS NULL 
                        OR 
                        @TermoBusca = '' 
                        OR
                        Nome LIKE '%' + @TermoBusca + '%' 
                        OR 
                        Descricao LIKE '%' + @TermoBusca + '%' 
                        OR 
                        Localizacao LIKE '%' + @TermoBusca + '%'
                       )
                ORDER BY DataInclusao DESC

                OFFSET @Offset ROWS
                FETCH NEXT @TamanhoPagina ROWS ONLY;

            END";

            migrationBuilder.Sql(scriptAntecessor);
        }
    }
}
