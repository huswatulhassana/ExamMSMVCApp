using ExamMSAppMVC.Contracts;
using ExamMSAppMVC.Interface.Repositories;
using ExamMSAppMVC.Interface.Service;
using ExamMSAppMVC.Models.Entities;
using ExamMSAppMVC.Models.DTOs;

namespace ExamMSAppMVC.Implementation.Services
{
    public class CourseService(ICourseRepository courseRepository) : ICourseService
    {
        private readonly ICourseRepository _courseRepository = courseRepository;

        public async Task<BaseResponse<bool>> CreateCourseAsync(CourseRequest request)
        {
            // 1. Check if course code already exists
            var existingCourse = await _courseRepository.GetByCodeAsync(request.CourseCode);
            if (existingCourse != null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Course with code {request.CourseCode} already exists.",
                    Status = false
                };
            }

            // 2. Map request to Entity
            var course = new Course
            {
                Name = request.Name,
                CourseCode = request.CourseCode,
            };

            // 3. Save to database
            await _courseRepository.AddAsync(course);

            return new BaseResponse<bool>
            {
                Message = "Course created successfully!",
                Status = true,
                Data = true
            };
        }

        public async Task<BaseResponse<IEnumerable<CourseResponse>>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();

            var data = courses.Select(c => new CourseResponse
            {
                Id = c.Id,
                Name = c.Name,
                CourseCode = c.CourseCode
            });

            return new BaseResponse<IEnumerable<CourseResponse>>
            {
                Status = true,
                Message = "Courses retrieved",
                Data = data
            };
        }
        public async Task<BaseResponse<bool>> UpdateCourseAsync(Guid id, CourseRequest request)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return new BaseResponse<bool> { Status = false, Message = "Course not found" };

            course.Name = request.Name;
            course.CourseCode = request.CourseCode;
            // Update other fields as necessary

            await _courseRepository.UpdateAsync(course);
            return new BaseResponse<bool> { Status = true, Message = "Course updated successfully" };
        }

        public async Task<BaseResponse<bool>> DeleteCourseAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return new BaseResponse<bool> { Status = false, Message = "Course not found" };

            await _courseRepository.DeleteAsync(course);

            return new BaseResponse<bool> { Status = true, Message = "Course deleted successfully" };
        }

        public async Task<BaseResponse<CourseResponse>> GetCourseByIdAsync(Guid id)
        {
            // 1. Fetch from repository
            var course = await _courseRepository.GetByIdAsync(id);

            // 2. Check if it exists
            if (course == null)
            {
                return new BaseResponse<CourseResponse>
                {
                    Status = false,
                    Message = "Course not found."
                };
            }

            // 3. Map Entity to DTO (CourseResponse)
            var courseDto = new CourseResponse
            {
                Id = course.Id,
                Name = course.Name,
                CourseCode = course.CourseCode,
            };

            return new BaseResponse<CourseResponse>
            {
                Status = true,
                Message = "Course retrieved successfully.",
                Data = courseDto
            };
        }
    }
}