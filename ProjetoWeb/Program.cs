using System.Security.Cryptography;

namespace ProjetoWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.UseStaticFiles();

        app.MapGet("/", () => "Hello World!");

        app.MapGet("/cliente", () => "Cliente !!!!!");

        app.MapGet("/produtos", (HttpContext contexto) =>
            {
                contexto.Response.Redirect("produtos.html", false);
            }
        );

        Pessoa pessoa1 = new Pessoa() { id = 1, nome = "Ana" };

        /*
        app.MapGet("/fornecedores", () =>
        $"O fornecedor é: {pessoa1.id} - {pessoa1.nome}"
        );*/

        app.MapGet("/fornecedores", (HttpContext contexto) =>
            {
                string pagina = "<h1> Fornecedores </h1>";
                pagina = pagina + $" <h2> ID:{pessoa1.id} - Nome {pessoa1.nome} </h2>";
                contexto.Response.WriteAsync(pagina);
            });

        app.MapGet("/fornecedoresEnviarDados", (int id, string nome) =>
        {
            pessoa1.id = id;
            pessoa1.nome = nome;
            return "Dados inseridos com sucesso";
        });

        app.MapGet("/clientes", (string nome, string email) =>
        $"O nome do cliente escolhido é : {nome} \n O email é: {email}");

        app.MapGet("/api", (Func<object>)(() =>
            {
                return new
                {
                    id = pessoa1.id,
                    nome = pessoa1.nome
                };
            }
        ));


        app.Run();
    }
}
