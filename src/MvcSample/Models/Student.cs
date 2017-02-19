using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSample.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        [Display(Name = "Lastname")]
        public string LastName { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public int Age { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
