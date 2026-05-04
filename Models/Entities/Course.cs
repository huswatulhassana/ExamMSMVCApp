using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamMSAppMVC.Contracts.Entities;

namespace ExamMSAppMVC.Models.Entities
{
    public class Course : BaseEntity
    {
        public required string Name { get; set; }
        public required string CourseCode { get; set; }
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }

}