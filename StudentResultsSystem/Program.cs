using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentResultsProcessingSystem
{
    class Program
    {
        // List to store all students
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            Console.Title = "Student Results Processing System";
            bool exit = false;

            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EnterStudentResults();
                        break;
                    case "2":
                        ViewStudentReport();
                        break;
                    case "3":
                        Console.WriteLine("\nThank you for using the Student Results Processing System.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        // Display main menu
        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
            Console.WriteLine();
            Console.WriteLine("1. Enter Student Results");
            Console.WriteLine("2. View Student Report");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            Console.Write("Choose an option: ");
        }

        // Option 1: Enter results for 3 students
        static void EnterStudentResults()
        {
            Console.Clear();
            Console.WriteLine("===== ENTER STUDENT RESULTS ====\n");

            // We can modify this to accept any number, but requirement says at least 3
            int numberOfStudents = 3;  // can be changed to dynamic

            for (int i = 0; i < numberOfStudents; i++)
            {
                Console.WriteLine($"Enter details for Student {i + 1}");
                Console.WriteLine();

                Student student = new Student();

                Console.Write("Enter full name: ");
                student.FullName = Console.ReadLine();

                Console.Write("Enter student ID: ");
                student.StudentID = Console.ReadLine();

                Console.Write("Enter programme: ");
                student.Programme = Console.ReadLine();

                Console.Write("Enter level: ");
                student.Level = Console.ReadLine();

                // Get scores for 5 courses with validation
                Console.WriteLine("\nEnter scores (0-100):");
                student.Scores = new int[5];
                string[] courseNames = { "Programming with C#", "Database Systems", "Computer Networks", "Web Development", "Mathematics for Computing" };

                for (int j = 0; j < courseNames.Length; j++)
                {
                    int score;
                    bool valid;
                    do
                    {
                        Console.Write($"Score for {courseNames[j]}: ");
                        string input = Console.ReadLine();
                        valid = int.TryParse(input, out score) && score >= 0 && score <= 100;
                        if (!valid)
                        {
                            Console.WriteLine("Invalid score. Score must be between 0 and 100.");
                        }
                    } while (!valid);
                    student.Scores[j] = score;
                }

                // Calculate total, average, grade, status
                student.Total = student.Scores.Sum();
                student.Average = student.Total / (double)student.Scores.Length;
                student.Grade = GetGrade(student.Average);
                student.Status = student.Average >= 50 ? "Passed" : "Failed";

                students.Add(student);
                Console.WriteLine($"\nStudent {i + 1} recorded successfully.\n");
            }

            Console.WriteLine("All students' results have been entered.");
        }

        // Grade mapping
        static string GetGrade(double average)
        {
            if (average >= 80) return "A";
            if (average >= 70) return "B";
            if (average >= 60) return "C";
            if (average >= 50) return "D";
            return "F";
        }

        // Option 2: Display report for all students
        static void ViewStudentReport()
        {
            Console.Clear();
            Console.WriteLine("===== STUDENT RESULTS REPORT =====\n");

            if (students.Count == 0)
            {
                Console.WriteLine("No student records found. Please enter results first.");
                return;
            }

            foreach (var student in students)
            {
                Console.WriteLine($"Student Name: {student.FullName}");
                Console.WriteLine($"Student ID: {student.StudentID}");
                Console.WriteLine($"Programme: {student.Programme}");
                Console.WriteLine($"Level: {student.Level}");
                Console.WriteLine();

                string[] courseNames = { "Programming with C#", "Database Systems", "Computer Networks", "Web Development", "Mathematics for Computing" };
                for (int i = 0; i < courseNames.Length; i++)
                {
                    Console.WriteLine($"{courseNames[i]}: {student.Scores[i]}");
                }

                Console.WriteLine();
                Console.WriteLine($"Total Score: {student.Total}");
                Console.WriteLine($"Average Score: {student.Average:F1}");
                Console.WriteLine($"Grade: {student.Grade}");
                Console.WriteLine($"Status: {student.Status}");
                Console.WriteLine(new string('-', 40));
            }

            // Extra: Find best student
            if (students.Count > 0)
            {
                var best = students.OrderByDescending(s => s.Average).First();
                Console.WriteLine($"Best Student: {best.FullName} with Average {best.Average:F1} (Grade {best.Grade})");
            }
        }
    }

    // Student class to hold data
    class Student
    {
        public string FullName { get; set; }
        public string StudentID { get; set; }
        public string Programme { get; set; }
        public string Level { get; set; }
        public int[] Scores { get; set; }
        public int Total { get; set; }
        public double Average { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
    }
}