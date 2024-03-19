// Define o namespace onde a classe Program est� localizada
namespace MeuSiteEmMVC {
    // Define a classe Program
    public class Program {
        // M�todo Main, ponto de entrada da aplica��o
        public static void Main(string[] args) {
            // Cria um WebApplicationBuilder usando os argumentos da linha de comando
            // � usado para configurar e inicializar uma aplica��o web ASP.NET Core.
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona servi�os ao cont�iner de inje��o de depend�ncia para suportar controllers e views
            // � usado para registrar os servi�os necess�rios para suportar o padr�o de design Model-View-Controller (MVC) na aplica��o ASP.NET Core.
            // Essa linha de c�digo � essencial para habilitar o suporte a controllers e views na aplica��o we
            builder.Services.AddControllersWithViews();

            // Constr�i a aplica��o
            var app = builder.Build();

            // Configura o pipeline de requisi��o HTTP
            if (!app.Environment.IsDevelopment()) {
                // Se n�o estiver em ambiente de desenvolvimento, configura o tratamento de exce��es
                app.UseExceptionHandler("/Home/Error");
                // Define o tempo de HSTS (HTTP Strict Transport Security) para aumentar a seguran�a
                // Link de refer�ncia fornecido para mais informa��es sobre HSTS
                // (https://aka.ms/aspnetcore-hsts)
                app.UseHsts();
            }


            //CONFIGURA��ES PADR�ES DO PIPELINE DE REQUISI��O HTTP

            // Redireciona todas as requisi��es HTTP para HTTPS
            app.UseHttpsRedirection();

            // Habilita o uso de arquivos est�ticos (CSS, JavaScript, imagens, etc.)
            app.UseStaticFiles();

            // Habilita o roteamento de requisi��es
            app.UseRouting();

            // Habilita a autoriza��o para controle de acesso
            app.UseAuthorization();


            //POR PADR�O O PROJETO STARTA NA CONTROLLER HOMER E NA ACTION INDEX, COM UM PADR�O QUE PODE SER NULL OU VIR PREENCHIDO 
            // Mapeia uma rota padr�o para os controladores
            //O m�todo app.MapControllerRoute() � usado p configurar uma rota padr�o para os controladores. Especifica que por padr�o,
            //quando a aplica��o recebe a requisi��o sem um controlador ou a��o especifica na URL, ele redirecionar� para a Ation Index do controlador Home
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            /*{controller=Home}: Define o controlador padr�o como Home.
             *{action=Index}: Define a action padr�o como Index.
             *{id?}: Indica que o par�metro id � opcional na rota.*/

            /* uma "Action" � um m�todo dentro de um controlador que executa uma determinada funcionalidade em resposta a uma requisi��o HTTP
             * As actions s�o respons�veis por processar as requisi��es do cliente e retornar uma resposta adequada, que pode ser uma p�gina HTML, um JSON
             * No exemplo fornecido, a action Index seria respons�vel por mostrar a p�gina inicial do site.
             * No exemplo fornecido, a action Index seria respons�vel por mostrar a p�gina inicial do site.*/

            // Inicia a execu��o da aplica��o
            app.Run();
        }
    }

}
