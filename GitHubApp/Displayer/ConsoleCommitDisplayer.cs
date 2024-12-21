using GitHubApp.Core.Interfaces;
using GitHubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApp.Displayer
{
    public class ConsoleCommitDisplayer : ICommitDisplayer
    {
        public void DisplayCommits(string repo, List<Commit> commits)
        {
            foreach (var commit in commits)
            {
                Console.WriteLine($"{repo}/{commit.Sha}: {commit.Message} [{commit.Committer}]");
            }
        }
    }
}
