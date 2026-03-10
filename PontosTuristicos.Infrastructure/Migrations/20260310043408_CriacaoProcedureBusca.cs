using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PontosTuristicos.Infrastructure.Migrations
{
    public partial class CriacaoProcedureBusca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var scriptProcedure = @"

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

            migrationBuilder.Sql(scriptProcedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE stp_BuscarPontosTuristicos");
        }
    }
}