using EFproject.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFproject.models
{
    public enum ResourceType
    {
        Video,
        Presentation,
        Document,
        Other
    }
    public enum ContentType
    {
        Application,
        Pdf,
        Zip
    }
    public class Student
    {


        public int StudentId { get; set; }

        public string Name { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<StudentCourse> CourseEnrollments { get; set; } = new List<StudentCourse>();
        public ICollection<Homework> HomeworkSubmissions { get; set; } = new List<Homework>();
    }  
}
public class Course
{
    public int CourseId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }

    // Navigation Properties
    public ICollection<StudentCourse> StudentsEnrolled { get; set; } = new List<StudentCourse>();
    public ICollection<Resource> Resources { get; set; } = new List<Resource>();
    public ICollection<Homework> HomeworkSubmissions { get; set; } = new List<Homework>();

}
public class Resource
{
    public int ResourceId { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public ResourceType ResourceType { get; set; }

    // Foreign Key & Navigation Property
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;



}
public class Homework
{
    public int HomeworkId { get; set; }
    public string Content { get; set; } = null!;
    public ContentType ContentType { get; set; }
    public DateTime SubmissionTime { get; set; }

    
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
}
public class StudentCourse
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

}