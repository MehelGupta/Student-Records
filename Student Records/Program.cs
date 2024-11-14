using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Records
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            do
            {
                Console.WriteLine("Welcome to student manager");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Create a new student profile");
                Console.WriteLine("2. Access student profile");
                Console.WriteLine("3. Delete existing profile");
            }
            while (exit == false);
        }
    }
    public class Student
    {
        //the num of students is used to generate IDs
        private static int numOfStudents = 0;
        private string id;
        private string firstName;
        private string lastName;
        private int age;
        private string gender;
        private string email;

        public Student(string firstName, string lastName, int age, string gender, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.gender = gender;
            this.email = email;
            id = generateID(firstName, lastName);
            numOfStudents++;
        }
        public Student(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            gender = "N/A";
            email = "N/A";
            id = generateID(firstName, lastName);
            numOfStudents++;
        }

        //Static method to generate ID
        public static string generateID (string firstName, string lastName)
        {
            return $"{firstName.Substring(0,1)}{lastName.Substring(0,1)}{numOfStudents}";
        }

    }
    
}
