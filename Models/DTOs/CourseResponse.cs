using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamMSAppMVC.Models.DTOs
{
    public class CourseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string CourseCode { get; set; } = default!;
    }
}