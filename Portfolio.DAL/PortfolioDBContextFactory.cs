using Audit.EntityFramework;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Portfolio.DAL
{
    public class PortfolioDBContextFactory
    {
        private readonly IOptions<PortfolioDBConnectionSetting> _connetionOptions;

        public PortfolioDBContextFactory(IOptions<PortfolioDBConnectionSetting> connetionOptions)
        {
            _connetionOptions = connetionOptions;
        }

        public PortfolioDB DbContext => new PortfolioDB(GetDataContext().Options);

        public DbContextOptionsBuilder<PortfolioDB> GetDataContext() { 
            ValidateDefaultConnection();
            var sqlConnectionBuilder = new SqlConnectionStringBuilder(_connetionOptions.Value.DefaultConnection);
            var contextOptionsBuilder = new DbContextOptionsBuilder<PortfolioDB>();
            contextOptionsBuilder.UseSqlServer(sqlConnectionBuilder.ConnectionString);
            return contextOptionsBuilder;
        }

        public void ValidateDefaultConnection()
        {
            if(string.IsNullOrEmpty(_connetionOptions.Value.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(_connetionOptions.Value.DefaultConnection));
            }
        }

    }
}
