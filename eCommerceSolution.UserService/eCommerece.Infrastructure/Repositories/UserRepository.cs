using Dapper;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerece.Infrastructure.DbContext;

namespace eCommerece.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly DapperDbContext _dbContext;
    public UserRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        user.User_Id = Guid.NewGuid();

        //string query = "INSERT INTO public.\"Users\" (\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\")" +
        //               "VALUES (@UserID, @Email, @PersonName, @Gender, @Password)";

        //string query = @"
        //    INSERT INTO public.""Users"" 
        //    (""UserID"", ""Email"", ""PersonName"", ""Gender"", ""Password"") 
        //    VALUES 
        //    (@UserID, @Email, @PersonName, @Gender, @Password)";

        string query = @"
            insert into tbl_users
            (user_id, email, person_name, gender, password) 
            values 
            (@user_id, @email, @person_name, @gender, @password)";

        int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);

        if (rowCountAffected > 0)
        {
            return user;
        }
        else
        {
            return null;
        }
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        //return new ApplicationUser()
        //{
        //    UserId = Guid.NewGuid(),
        //    Email = Email,
        //    Password = Password,
        //    PersonName = "SHARAD",
        //    Gender = GenderOptions.Male.ToString()
        //};

        string query = @"select * from tbl_users where email = @email and password = @password";

        var parameters = new { email = email, password = password };

        ApplicationUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

        return user == null ? null : user;
    }
}
