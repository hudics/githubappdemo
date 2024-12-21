using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GitHubApp.Core.DTO
{
    public class CommitDto
    {
        [JsonPropertyName("sha")]
        public string Sha { get; set; }

        [JsonPropertyName("commit")]
        public CommitDetails Commit { get; set; }       
    }

    public class CommitDetails
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("committer")]
        public Committer Committer { get; set; }
    }

    public class Committer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
