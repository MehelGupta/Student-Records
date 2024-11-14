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
            Profiles LTISD = new Profiles();
            string input;
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
    public class Profiles
    {
        List<Student> students;

        public Profiles()
        {
            students = new List<Student>();
        }
        //Deletes profile of choice
        public void deleteProfile(string id)
        {
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].ID == id)
                {
                    Console.WriteLine($"Are you sure you'd like to delete {students[i].FirstName} {students[i].LastName}'s profile?(yes/no)");
                    string input = Console.ReadLine();
                    if (string.Equals(input.ToLower(), "yes"))
                    {
                        Console.WriteLine("Deleting...");
                        students.RemoveAt(i);
                        Console.WriteLine("Delete successful!");
                    }
                    else if (string.Equals(input.ToLower(), "no"))
                    {
                        Console.WriteLine("Not deleting...");
                    }
                }
            }
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
        private string phoneNumber;
        private string email;

        public Student(string firstName, string lastName, int age, string phoneNumber, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.email = email;
            id = generateID(firstName, lastName);
            numOfStudents++;
        }
        public Student(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            phoneNumber = "N/A";
            email = "N/A";
            id = generateID(firstName, lastName);
            numOfStudents++;
        }

        //Static method to generate ID
        public static string generateID(string firstName, string lastName)
        {
            return $"{firstName.Substring(0, 1)}{lastName.Substring(0, 1)}{numOfStudents}";
        }

        //Static method to print student profile
        public void printStudentProfile()
        {
            Console.WriteLine($"             Student: {id}             ");
            Console.WriteLine($"---------------------------------------");
            Console.WriteLine($"|First: {firstName}  Last: {lastName}");
            Console.WriteLine($"|Age: {age}\n");
            Console.WriteLine("             Contact Informaion             ");
            Console.WriteLine($"---------------------------------------");
            Console.WriteLine($"|Email: {email}\nPhone Number: {phoneNumber}");
        }

        //GETTERS AND SETTERS TILL LINE 156
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (age >= 18)
                    firstName = value;
                else
                    Console.WriteLine("Not an adult.Cannot change name");
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (age >= 18)
                    lastName = value;
                else
                    Console.WriteLine("Not an adult.Cannot change name");
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        //END OF GETTERS AND SETTERS
    }

}
