using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models.DTOs;

namespace ExamMSAppMVC.Interface.Service
{
    public interface ICourseService
    {
        Task<BaseResponse<bool>> CreateCourseAsync(CourseRequest request);
        Task<BaseResponse<IEnumerable<CourseResponse>>> GetAllCoursesAsync();
        Task<BaseResponse<CourseResponse>> GetCourseByIdAsync(Guid id);
        Task<BaseResponse<bool>> UpdateCourseAsync(Guid id, CourseRequest request);
        Task<BaseResponse<bool>> DeleteCourseAsync(Guid id);
    }

    public record CourseRequest(string Name, string CourseCode, string Description);
}