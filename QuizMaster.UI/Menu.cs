
using QuizMaster.Core.Enum;
using QuizMaster.Core.Model;
using QuizMaster.Service;

namespace QuizMaster.UI
{
    public class Menu
    {
        private readonly StudentService _studentService;

        public Menu(StudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task ShowMenu()
        {
            Console.WriteLine("Welcome to the QuizMaster!");
            Console.WriteLine("1. Register as a new student");
            Console.WriteLine("2. View all students");
            Console.WriteLine("3. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Student student = new Student();

                    Console.WriteLine("Enter FirsName");
                    student.FirsName = Console.ReadLine();

                    Console.WriteLine("Enter Lastname");
                    student.Lastname = Console.ReadLine();

                    Console.WriteLine("Enter Email");
                    student.Email = Console.ReadLine();

                    Console.WriteLine("Enter PhoneNumber");
                    student.PhoneNumber = Console.ReadLine();

                    Console.WriteLine("Enter PersonalNumber");
                    student.PersonalNumber = Console.ReadLine();

                    Console.WriteLine("Enter UserName");
                    student.UserName = Console.ReadLine();

                    Console.WriteLine("Enter Password");
                    student.Password = Console.ReadLine();

                    Console.WriteLine("Enter Gender");
                    student.Gender = Enum.Parse<Gender>(Console.ReadLine(), true);

                  

                    await _studentService.RegistrationStudent(student);
                    break;
                    //case "2":
                    //    await ViewAllStudents();
                    //    break;
                    //case "3":
            }
        }
    }
}
