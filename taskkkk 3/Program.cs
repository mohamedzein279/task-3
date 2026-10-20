
using System;
using System.Collections.Generic;

namespace Task3_StudenManagementSystem
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public Instructor(int id, string name, string specialization)
        {
            InstructorId = id;
            Name = name;
            Specialization = specialization;
        }

        public string PrintDetails()
        {
            return $"[Instructor ID: {InstructorId}] Name: {Name} | Specialization: {Specialization}";
        }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }

        public Course(int id, string title, Instructor instructor)
        {
            CourseId = id;
            Title = title;
            Instructor = instructor;
        }

        public string PrintDetails()
        {
            string instructorName;

            if (Instructor != null)
            {
                instructorName = Instructor.Name;
            }
            else
            {
                instructorName = "No Instructor Assigned";
            }

            return $"[Course ID: {CourseId}] Title: {Title} | Instructor: {instructorName}";
        }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; }

        public Student(int id, string name, int age)
        {
            StudentId = id;
            Name = name;
            Age = age;
            Courses = new List<Course>();
        }

        public bool Enroll(Course course)
        {
            if (course == null)
            {
                return false;
            }

            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseId == course.CourseId)
                {
                    return false;
                }
            }

            Courses.Add(course);
            return true;
        }

        public string PrintDetails()
        {
            string details = $"[Student ID: {StudentId}] Name: {Name} | Age: {Age} | Courses: ";

            if (Courses.Count == 0)
            {
                details += "No courses";
            }
            else
            {
                for (int i = 0; i < Courses.Count; i++)
                {
                    details += Courses[i].Title + " ";
                }
            }

            return details;
        }
    }

    public class StudentManager
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Instructor> Instructors { get; set; }

        public StudentManager()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Instructors = new List<Instructor>();
        }

        public bool AddStudent(Student student)
        {
            if (student == null)
            {
                return false;
            }

            if (FindStudent(student.StudentId) != null)
            {
                return false;
            }

            Students.Add(student);
            return true;
        }

        public bool AddCourse(Course course)
        {
            if (course == null)
            {
                return false;
            }

            if (FindCourse(course.CourseId) != null)
            {
                return false;
            }

            Courses.Add(course);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (instructor == null)
            {
                return false;
            }

            if (FindInstructor(instructor.InstructorId) != null)
            {
                return false;
            }

            Instructors.Add(instructor);
            return true;
        }

        public Student FindStudent(int studentId)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == studentId)
                {
                    return Students[i];
                }
            }

            return null;
        }

        public Course FindCourse(int courseId)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseId == courseId)
                {
                    return Courses[i];
                }
            }

            return null;
        }

        public Instructor FindInstructor(int instructorId)
        {
            for (int i = 0; i < Instructors.Count; i++)
            {
                if (Instructors[i].InstructorId == instructorId)
                {
                    return Instructors[i];
                }
            }

            return null;
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student student = FindStudent(studentId);
            Course course = FindCourse(courseId);

            if (student != null && course != null)
            {
                return student.Enroll(course);
            }

            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("STUDENT MANAGEMENT SYSTEM");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Show All Students");
                Console.WriteLine("6. Show All Courses");
                Console.WriteLine("7. Show All Instructors");
                Console.WriteLine("8. Find Student by ID or Name");
                Console.WriteLine("9. Find Course by ID or Name");
                Console.WriteLine("10. Exit");
                Console.WriteLine("=========================================");
                Console.Write("Choose an option (1-10): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Student ID: ");
                        int sId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Student Name: ");
                        string sName = Console.ReadLine();

                        Console.Write("Enter Student Age: ");
                        int sAge = Convert.ToInt32(Console.ReadLine());

                        Student newStudent = new Student(sId, sName, sAge);

                        if (manager.AddStudent(newStudent))
                        {
                            Console.WriteLine("Student added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add student (ID might already exist).");
                        }

                        break;

                    case "2":
                        Console.Write("Enter Instructor ID: ");
                        int iId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Instructor Name: ");
                        string iName = Console.ReadLine();

                        Console.Write("Enter Specialization: ");
                        string spec = Console.ReadLine();

                        Instructor newInst = new Instructor(iId, iName, spec);

                        if (manager.AddInstructor(newInst))
                        {
                            Console.WriteLine("Instructor added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add instructor.");
                        }

                        break;

                    case "3":
                        Console.Write("Enter Course ID: ");
                        int cId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Course Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter Instructor ID for this course: ");
                        int instId = Convert.ToInt32(Console.ReadLine());

                        Instructor inst = manager.FindInstructor(instId);

                        if (inst != null)
                        {
                            Course newCourse = new Course(cId, title, inst);

                            if (manager.AddCourse(newCourse))
                            {
                                Console.WriteLine("Course added successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Course ID already exists.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Instructor not found! Please create instructor first.");
                        }

                        break;

                    case "4":
                        Console.Write("Enter Student ID: ");
                        int stId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Course ID: ");
                        int crsId = Convert.ToInt32(Console.ReadLine());

                        if (manager.EnrollStudentInCourse(stId, crsId))
                        {
                            Console.WriteLine("Student enrolled successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Enrollment failed. Check IDs or student enrollment.");
                        }

                        break;

                    case "5":
                        Console.WriteLine("--- Registered Students ---");

                        if (manager.Students.Count == 0)
                        {
                            Console.WriteLine("No students found.");
                        }

                        for (int i = 0; i < manager.Students.Count; i++)
                        {
                            Console.WriteLine(manager.Students[i].PrintDetails());
                        }

                        break;

                    case "6":
                        Console.WriteLine("--- Available Courses ---");

                        if (manager.Courses.Count == 0)
                        {
                            Console.WriteLine("No courses found.");
                        }

                        for (int i = 0; i < manager.Courses.Count; i++)
                        {
                            Console.WriteLine(manager.Courses[i].PrintDetails());
                        }

                        break;

                    case "7":
                        Console.WriteLine("--- Instructors List ---");

                        if (manager.Instructors.Count == 0)
                        {
                            Console.WriteLine("No instructors found.");
                        }

                        for (int i = 0; i < manager.Instructors.Count; i++)
                        {
                            Console.WriteLine(manager.Instructors[i].PrintDetails());
                        }

                        break;

                    case "8":
                        Console.Write("Enter Student ID or Name to search: ");
                        string studentSearch = Console.ReadLine();
                        bool studentFound = false;

                        for (int i = 0; i < manager.Students.Count; i++)
                        {
                            if (manager.Students[i].StudentId.ToString() == studentSearch ||
                                manager.Students[i].Name.Equals(studentSearch, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine(manager.Students[i].PrintDetails());
                                studentFound = true;
                            }
                        }

                        if (!studentFound)
                        {
                            Console.WriteLine("Student not found.");
                        }

                        break;

                    case "9":
                        Console.Write("Enter Course ID or Name to search: ");
                        string courseSearch = Console.ReadLine();
                        bool courseFound = false;

                        for (int i = 0; i < manager.Courses.Count; i++)
                        {
                            if (manager.Courses[i].CourseId.ToString() == courseSearch ||
                                manager.Courses[i].Title.Equals(courseSearch, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine(manager.Courses[i].PrintDetails());
                                courseFound = true;
                            }
                        }

                        if (!courseFound)
                        {
                            Console.WriteLine("Course not found.");
                        }

                        break;

                    case "10":
                        isRunning = false;
                        Console.WriteLine("Exiting program... Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please select between 1 and 10.");
                        break;
                }
            }
        }
    }
}

