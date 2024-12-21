using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApp.Core.Models
{
    public class Commit
    {
        public string Sha { get; set; }
        public string Message { get; set; }
        public string Committer { get; set; }
    }
}
