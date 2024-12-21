using GitHubApp.Core.Interfaces;
using GitHubApp.Core.Models;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubApp.Infrastructure.Repositories
{
    public class SQLiteCommitRepository : ICommitRepository
    {
        private readonly string _connectionString;

        public SQLiteCommitRepository(string connectionString)
        {
            _connectionString = connectionString;
            SetupDatabase();
        }

        private void SetupDatabase()
        {
            Batteries.Init();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
               
                var command = connection.CreateCommand();
                command.CommandText = @"
        CREATE TABLE IF NOT EXISTS commits (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            user TEXT NOT NULL,
            repo TEXT NOT NULL,
            sha TEXT UNIQUE NOT NULL,
            message TEXT NOT NULL,
            committer TEXT NOT NULL
        );";
                command.ExecuteNonQuery();
            }
        }

        public void SaveCommits(string user, string repo, List<Commit> commits)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                INSERT OR IGNORE INTO commits (user, repo, sha, message, committer) 
                VALUES ($user, $repo, $sha, $message, $committer);";

                    foreach (var commit in commits)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("$user", user);
                        command.Parameters.AddWithValue("$repo", repo);
                        command.Parameters.AddWithValue("$sha", commit.Sha);
                        command.Parameters.AddWithValue("$message", commit.Message);
                        command.Parameters.AddWithValue("$committer", commit.Committer);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }


    }
}
