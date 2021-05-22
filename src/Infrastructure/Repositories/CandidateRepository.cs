using System;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Connection;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : BaseRepository, ICandidateRepository
    {
        public CandidateRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            using var conn = ConnectionFactory.GetConnection();

            const string sql = "SELECT * FROM Candidate;";

            return await conn.QueryAsync<Candidate>(sql);
        }

        public async Task<int> CreateAsync(Candidate candidate)
        {
            using var conn = ConnectionFactory.GetConnection();

            candidate.UpdatedDate = candidate.CreatedDate = DateTimeOffset.Now;
            
            const string sql = @"INSERT INTO Candidate(ID, FirstName, Surname, DateOfBirth, Address1, Town, Country,
                      PostCode, PhoneHome, PhoneMobile, PhoneWork, CreatedDate, UpdatedDate) 
                      VALUES ((SELECT ISNULL(MAX(ID) + 1, 0) FROM Candidate), @FirstName, @Surname, @DateOfBirth, 
                              @Address1, @Town, @Country, @PostCode, @PhoneHome, @PhoneMobile, @PhoneWork, @CreatedDate,
                              @UpdatedDate); SELECT MAX(ID) FROM Candidate;";

            return await conn.ExecuteScalarAsync<int>(sql, candidate);
        }
    }
}