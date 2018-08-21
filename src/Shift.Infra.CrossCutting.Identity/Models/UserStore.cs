#region usings

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Models
{
    public class UserStore : 
                            IUserStore<ApplicationUser>, 
                            IUserEmailStore<ApplicationUser>, 
                            IUserPhoneNumberStore<ApplicationUser>,
                            IUserTwoFactorStore<ApplicationUser>,
                            IUserPasswordStore<ApplicationUser>,
                            IUserRoleStore<ApplicationUser>
    {

        private readonly string _connectionString;


        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                var query = $@"INSERT INTO [AspNetUsers]
                                                        (
                                                           [UserName],
                                                           [NormalizedUserName],
                                                           [Email],
                                                           [NormalizedEmail],
                                                           [EmailConfirmed],
                                                           [PasswordHash],
                                                           [SecurityStamp],
                                                           [ConcurrencyStamp],
                                                           [PhoneNumber],
                                                           [PhoneNumberConfirmed],
                                                           [TwoFactorEnabled],
                                                           [LockoutEnd],
                                                           [LockoutEnabled],
                                                           [AccessFailedCount],
                                                           [Matricula]
                                                        )
                                                       VALUES
                                                       (
                                                           @{nameof(ApplicationUser.UserName)},
                                                           @{nameof(ApplicationUser.NormalizedUserName)},
                                                           @{nameof(ApplicationUser.Email)},
                                                           @{nameof(ApplicationUser.NormalizedEmail)},
                                                           @{nameof(ApplicationUser.EmailConfirmed)},
                                                           @{nameof(ApplicationUser.PasswordHash)},
                                                           @{nameof(ApplicationUser.SecurityStamp)},
                                                           @{nameof(ApplicationUser.ConcurrencyStamp)},
                                                           @{nameof(ApplicationUser.PhoneNumber)},
                                                           @{nameof(ApplicationUser.PhoneNumberConfirmed)},
                                                           @{nameof(ApplicationUser.TwoFactorEnabled)},
                                                           @{nameof(ApplicationUser.LockoutEnd)},
                                                           @{nameof(ApplicationUser.LockoutEnabled)}
                                                           @{nameof(ApplicationUser.AccessFailedCount)},
                                                           @{nameof(ApplicationUser.Matricula)}); 
                                
                                                    SELECT CAST(SCOPE_IDENTITY() as int";

                user.Id = await connection.QuerySingleAsync<Guid>(query, user);
            }

            return IdentityResult.Success;

        }



        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                await connection.ExecuteAsync($"DELETE FROM [AspNetUsers] WHERE [Id] = @{nameof(ApplicationUser.Id)}", user);

            }

            return IdentityResult.Success;

        }



        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * [AspNetUsers] WHERE [Id] = @{nameof(userId)}", new { userId });
            }
        }



        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * [AspNetUsers] WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
        }



        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }


        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }



        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }



        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;

            return Task.FromResult(0);
        }



        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                await connection.ExecuteAsync($@"UPDATE [AspNetUsers] SET
                                                [UserName]              =   @{nameof(ApplicationUser.UserName)},
                                                [NormalizedUserName]    =   @{nameof(ApplicationUser.NormalizedUserName)},
                                                [Email]                 =   @{nameof(ApplicationUser.Email)},
                                                [NormalizedEmail]       =   @{nameof(ApplicationUser.NormalizedEmail)},
                                                [EmailConfirmed]        =   @{nameof(ApplicationUser.EmailConfirmed)},
                                                [PasswordHash]          =   @{nameof(ApplicationUser.PasswordHash)},
                                                [SecurityStamp]         =   @{nameof(ApplicationUser.SecurityStamp)},
                                                [ConcurrencyStamp]      =   @{nameof(ApplicationUser.ConcurrencyStamp)},
                                                [PhoneNumber]           =   @{nameof(ApplicationUser.PhoneNumber)},
                                                [PhoneNumberConfirmed]  =   @{nameof(ApplicationUser.PhoneNumberConfirmed)},
                                                [TwoFactorEnabled]      =   @{nameof(ApplicationUser.TwoFactorEnabled)},
                                                [LockoutEnd]            =   @{nameof(ApplicationUser.LockoutEnd)},
                                                [LockoutEnabled]        =   @{nameof(ApplicationUser.LockoutEnabled)},
                                                [AccessFailedCount]     =   @{nameof(ApplicationUser.AccessFailedCount)},
                                                [Matricula]             =   @{nameof(ApplicationUser.Matricula)}

                                                WHERE [Id] = @{nameof(ApplicationUser.Id)}", user);
            }

            return IdentityResult.Success;
        }



        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;

            return Task.FromResult(0);
        }



        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }



        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }




        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;

            return Task.FromResult(0);
        }




        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QuerySingleOrDefaultAsync<ApplicationUser>($@"SELECT * [AspNetUsers] WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }





        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }


        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;

            return Task.FromResult(0);
        }


        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }



        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }




        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;

            return Task.FromResult(0);
        }


        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;

            return Task.FromResult(0);
        }



        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }



        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }



        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }




        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }







        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    

      

        



    

        

 

    



     

     
        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }






        // IUserRoleStore
        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        // IUserRoleStore
        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        // IUserRoleStore
        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        // IUserRoleStore
        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        // IUserRoleStore
        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
