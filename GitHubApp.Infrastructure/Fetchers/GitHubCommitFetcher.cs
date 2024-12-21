using GitHubApp.Core.DTO;
using GitHubApp.Core.Interfaces;
using GitHubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubApp.Infrastructure.Fetchers
{
    public class GitHubCommitFetcher : ICommitFetcher
    {
        private readonly HttpClient _httpClient;

        public GitHubCommitFetcher()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Commit>> FetchCommitsAsync(string user, string repo)
        {
            var url = $"https://api.github.com/repos/{user}/{repo}/commits";
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("C# App");

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Bad HTTP response: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var dtos = JsonSerializer.Deserialize<List<CommitDto>>(content);
                return dtos.Select(dto => new Commit
                {
                    Sha = dto.Sha,
                    Message = dto.Commit.Message,
                    Committer = dto.Commit.Committer.Name
                }).ToList();
            }
            catch (HttpRequestException ex)
            {               
                Console.WriteLine($"HTTP communication error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {                
                Console.WriteLine($"JSON data deserialization error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }
    }
}
