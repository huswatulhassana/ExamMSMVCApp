using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamMSAppMVC.Contracts.Entities;

namespace ExamMSAppMVC.Models.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}