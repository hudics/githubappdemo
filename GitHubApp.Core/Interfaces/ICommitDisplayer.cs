using GitHubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApp.Core.Interfaces
{
    public interface ICommitDisplayer
    {
        void DisplayCommits(string repo, List<Commit> commits);
    }
}
