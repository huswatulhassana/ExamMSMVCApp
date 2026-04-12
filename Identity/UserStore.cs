// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using ExamMSAppMVC.Models.Entities;
// using ExamMSAppMVC.EMSDBcontext; // Ensure this matches your actual namespace

// namespace ExamMSAppMVC.Identity
// {
//     public class UserStore : IUserStore<User>
//     {
//         private readonly EMSDBcontext _context;

//         public UserStore(EMSDBcontext context)
//         {
//             _context = context;
//         }

//         // Required by IUserStore: Get User ID
//         public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
//         {
//             return Task.FromResult(user.Id.ToString());
//         }

//         // Required by IUserStore: Get User Name
//         public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
//         {
//             return Task.FromResult(user.UserName);
//         }

//         public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
//         {
//             user.UserName = userName;
//             return Task.CompletedTask;
//         }

//         // Required by IUserStore: Normalized Name (Used for searching)
//         public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
//         {
//             return Task.FromResult(user.NormalizedUserName);
//         }

//         public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
//         {
//             user.NormalizedUserName = normalizedName;
//             return Task.CompletedTask;
//         }

//         // Create User
//         public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
//         {
//             cancellationToken.ThrowIfCancellationRequested();
//             _context.Add(user);
//             await _context.SaveChangesAsync(cancellationToken);
//             return IdentityResult.Success;
//         }

//         // Update User
//         public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
//         {
//             _context.Update(user);
//             await _context.SaveChangesAsync(cancellationToken);
//             return IdentityResult.Success;
//         }

//         // Delete User
//         public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
//         {
//             _context.Remove(user);
//             await _context.SaveChangesAsync(cancellationToken);
//             return IdentityResult.Success;
//         }

//         // Find by ID
//         public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
//         {
//             return await _context.Set<User>().FindAsync(new object[] { userId }, cancellationToken);
//         }

//         // Find by Name
//         public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
//         {
//             return await _context.Set<User>()
//                 .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
//         }

//         public void Dispose()
//         {
//             // Nothing to dispose manually as EF handles the context lifetime
//         }
//     }








// }