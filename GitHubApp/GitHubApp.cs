using GitHubApp.Core.Interfaces;

namespace GitHubApp
{
    public class GitHubApp
    {
        private readonly ICommitFetcher _fetcher;
        private readonly ICommitDisplayer _displayer;
        private readonly ICommitRepository _repository;

        public GitHubApp(ICommitFetcher fetcher, ICommitDisplayer displayer, ICommitRepository repository)
        {
            _fetcher = fetcher;
            _displayer = displayer;
            _repository = repository;
        }

        public async Task RunAsync(string user, string repo)
        {
            var commits = await _fetcher.FetchCommitsAsync(user, repo);
            if (commits == null) return;

            _displayer.DisplayCommits(repo, commits);
            _repository.SaveCommits(user, repo, commits);
        }
    }
}
