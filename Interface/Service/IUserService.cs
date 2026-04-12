using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamMSAppMVC.Models.DTOs;
using ExamMSAppMVC.Models.DTOs.Auth;
using ExamMSAppMVC.Models.Entities;

namespace ExamMSAppMVC.Interface.Service
{
    public interface IUserService
    {
        public Task<BaseResponse<bool>> RegisterMemberAsync(RegisterRequest request);
        public Task<BaseResponse> DeleteMember(Guid memberId);
        public Task<BaseResponse<LoginResponds>> LoginAsync(LoginRequest request);
        Task<BaseResponse<Student>> GetStudentByUserIdAsync(Guid userId);

    }
}