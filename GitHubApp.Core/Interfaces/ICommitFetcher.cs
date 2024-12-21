using GitHubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApp.Core.Interfaces
{
    public interface ICommitFetcher
    {
        Task<List<Commit>> FetchCommitsAsync(string user, string repo);
    }
}
