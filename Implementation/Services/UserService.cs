using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.DTOs;
using ExamMSAppMVC.Models.DTOs.Auth;
using ExamMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ExamMSAppMVC.EMSDBcontext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;



namespace ExamMSAppMVC.Implementation.Services
{
    public class UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ILogger<UserService> logger,
        IHttpContextAccessor httpContext,
        EMSDbContext context) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly ILogger<UserService> _logger = logger;
        private readonly IHttpContextAccessor _httpContext = httpContext;
        private readonly EMSDbContext _context = context;

        public async Task<BaseResponse<LoginResponds>> LoginAsync(LoginRequest request)
        {

            var user = await _userRepository.GetUserByRegistrationNumberAsync(request.RegistrationNumber);
            if (user == null)
            {
                _logger.LogError("Invalid Registration Number: {RegNum}", request.RegistrationNumber);
                return new BaseResponse<LoginResponds>
                {
                    Message = "Invalid Registration Number, Please try again",
                    Status = false
                };
            }

            var isPasswordValid = request.Password == user.HashPassword;
            if (!isPasswordValid)
            {
                _logger.LogError("Invalid Password for user: {RegNum}", request.RegistrationNumber);
                return new BaseResponse<LoginResponds>
                {
                    Message = "Invalid Password, Please try again",
                    Status = false
                };
            }

            return new BaseResponse<LoginResponds>
            {
                Message = "Login successful",
                Status = true,
                Data = new LoginResponds
                {
                    Id = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    RoleName = user.Role?.Name ?? "Student"
                }
            };
        }

        public async Task<BaseResponse<bool>> RegisterMemberAsync(RegisterRequest request)
        {
                      var userExists = await _userRepository.GetByEmailAsync(request.Email);
            if (userExists != null)
            {
                return new BaseResponse<bool> { Status = false, Message = "User already exists" };
            }

            var role = await _roleRepository.GetByNameAsync("Student");
            if (role == null)
            {
                return new BaseResponse<bool>
                {
                    Status = false,
                    Message = "Registration failed: The 'Student' role does not exist."
                };
            }

            // Start a transaction to ensure both User and Student are created or neither is
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newUser = new Models.User
                {
                    Email = request.Email,
                    HashPassword = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    RoleId = role.Id,
                    RegistrationNumber = request.RegistrationNumber,
                    DateCreated = DateTime.UtcNow
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                // AUTOMATICALLY CREATE STUDENT PROFILE
                var studentProfile = new Student
                {
                    UserId = newUser.Id,
                    RegistrationNumber = request.RegistrationNumber,
                    DateCreated = DateTime.UtcNow,

                };

                await _context.Students.AddAsync(studentProfile);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new BaseResponse<bool> { Status = true, Message = "Registration successful! Student profile created." };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error during student registration");
                return new BaseResponse<bool> { Status = false, Message = "An error occurred during registration." };
            }
        }

        public async Task<BaseResponse> DeleteMember(Guid memberId)
        {
            var user = await _userRepository.GetByIdAsync(memberId);
            if (user == null) return new BaseResponse { Status = false, Message = "User not found" };

            await _userRepository.DeleteAsync(user);
            return new BaseResponse { Status = true, Message = "Student deleted successfully" };
        }

        public async Task<BaseResponse<Student>> GetStudentByUserIdAsync(Guid userId)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
            {
                return new BaseResponse<Student> { Status = false, Message = "No student profile found for this user." };
            }

            return new BaseResponse<Student> { Status = true, Data = student };
        }
    }
}