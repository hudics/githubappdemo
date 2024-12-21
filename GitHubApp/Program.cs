using GitHubApp.Core.Interfaces;
using GitHubApp.Displayer;
using GitHubApp.Infrastructure.Fetchers;
using GitHubApp.Infrastructure.Repositories;

namespace GitHubApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Użycie: dotnet run <nazwa_użytkownika> <nazwa_repozytorium>");
                return;
            }

            string user = args[0];
            string repo = args[1];

            ICommitFetcher fetcher = new GitHubCommitFetcher();
            ICommitDisplayer displayer = new ConsoleCommitDisplayer();
            ICommitRepository repository = new SQLiteCommitRepository("Data Source=commits.db");

            var app = new GitHubApp(fetcher, displayer, repository);
            await app.RunAsync(user, repo);
            Console.ReadKey();
        }
    }
}
