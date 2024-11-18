using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                Console.Clear();
                Console.WriteLine("Welcome to student manager");
                Console.WriteLine("What would you like to do? Enter the number:");
                Console.WriteLine("1. Create a new student profile");
                Console.WriteLine("2. Delete existing profile");
                Console.WriteLine("3. Access student profile");
                Console.WriteLine("4. Edit student profile");
                action(input = Console.ReadLine(), LTISD);

            }
            while (exit == false);
        }

        //Manages the action of the user and calls the appropriate methods
        public static void action(string input, Profiles LTISD)
        {
            int value;
            if(!int.TryParse(input, out value) || value > 4 || value < 1)
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            else if(value == 1)
            {
                Console.WriteLine("\nEntering creating mode...");
                LTISD.createProfile();
            }
            else if(value == 2)
            { 
                Console.WriteLine("Entering deleting mode...");
                Console.WriteLine("--------------------");
                Console.WriteLine("Welcome to Delete Profile!");
                Console.WriteLine("Here is a list of all existing profiles and their IDs");
                LTISD.printAllIDs();
                Console.WriteLine("--------------------");
                Console.WriteLine("Which profile would you like to delete? Enter their ID below");
                string ID;
                LTISD.deleteProfile(ID = Console.ReadLine());
            }
            else if(value == 3)
            {
                string ID;
                Console.WriteLine("Entering profile mode...");
                Console.WriteLine("--------------------");
                Console.WriteLine("Welcome to Student Profiles!");
                Console.WriteLine("Here is a list of all existing profiles and their IDs");
                LTISD.printAllIDs();
                Console.WriteLine("--------------------");
                Console.WriteLine("Which profile would you like to acess? Enter their ID below");
                Student current = LTISD.getStudent(ID = Console.ReadLine());
                if (current != null)
                {
                    Console.WriteLine("Profile found!\n");
                    current.printStudentProfile();
                    Console.WriteLine("Press enter to close...");
                    Console.ReadLine();

                }
                else
                {
                    Console.WriteLine("Profile not found");
                    Console.ReadLine();
                }
                
            }
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
            bool found = false;
            
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].ID == id)
                {
                    found = true;
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
            if(!found)
            {
                Console.WriteLine("ID not found. No profile was deleted.");
                Console.ReadLine();
            }

        }
        //Creates a new student profile
        public void createProfile()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Welcome to Profile Creator!");
            Console.WriteLine("Please fill out the following information");
            //all of the variables for a new profile
            string firstName, lastName, phoneNum, email, ageInput;
            int loopNum = 0, age;
            do
            {
                if (loopNum > 0)
                    Console.WriteLine("One or more of the entered fields in invalid.\nPlease try again.");
                Console.WriteLine("--------About-------");
                Console.Write("First Name: ");
                firstName = Console.ReadLine();
                Console.Write("Last Name: ");
                lastName = Console.ReadLine();
                Console.Write("Age: ");
                ageInput = Console.ReadLine();
                loopNum++;
            }
            while (string.Equals(firstName, "") || string.Equals(lastName, "") || string.Equals(ageInput, "") || !int.TryParse(ageInput, out age) || age < 0);
            //optional contact info to go with the profile
            Console.WriteLine("-------Contact Info (optional)-------");
            Console.Write("Phone Number: ");
            phoneNum = Console.ReadLine();
            Console.Write("Email: ");
            email = Console.ReadLine();

            Console.WriteLine($"Creating Profile for {firstName} {lastName}...");
            string id;
            Student student;
            //Adds students based on given information and takes into account if they didn't enter information
            if (string.Equals(phoneNum, "") || string.Equals(email, ""))
            {
                if(string.Equals(phoneNum, "") && string.Equals(email, ""))
                {
                    student = new Student(firstName, lastName, age, out id);
                }
                else if (string.Equals(phoneNum, ""))
                {
                    student = new Student(firstName, lastName, age, out id);
                    student.PhoneNumber = email;
                }
                else
                {
                    student = new Student(firstName, lastName, age, out id);
                    student.Email = phoneNum;
                }
            }
            else
            {
                student = new Student(firstName, lastName, age, phoneNum, email, out id);
            }   
            students.Add(student);
            Console.WriteLine($"Successfully created profile! Student ID is {id}");
            Console.ReadLine();

        }
        //print out all student first and last names + their id
        public void printAllIDs ()
        {
            Console.WriteLine("Name:ID");
            Console.WriteLine("-------");
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}: {student.ID}");
            }
                
        }
        
        //getter for the student at index i based on their ID
        public Student getStudent(string ID)
        {
            int index = -1;
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].ID == ID)
                    index = i;
            }

            if(index != -1)
            {
                return students[index];
            }
            return null;
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

        public Student(string firstName, string lastName, int age, string phoneNumber, string email, out string id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.email = email;
            id = generateID(firstName, lastName);
            this.id = id;
            numOfStudents++;
        }
        public Student(string firstName, string lastName, int age, out string id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            phoneNumber = "N/A";
            email = "N/A";
            id = generateID(firstName, lastName);
            this.id = id;
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
            Console.WriteLine($"|Email: {email}\n|Phone Number: {phoneNumber}");
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
