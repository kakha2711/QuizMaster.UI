
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
            Console.WriteLine("3. View student from personalnumber");
            Console.WriteLine("4. Delete student from personalnumber");
            Console.WriteLine("5. Verifi student email");
            Console.WriteLine("6. LogIn Student");
            Console.WriteLine("7. Exit");
          
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

                case "2":
                    List<Student> students =  _studentService.GetAllStudents().Result;

                    foreach (var item in students)
                    {
                        Console.WriteLine($"Id: {item.Id},\n FirsName: {item.FirsName},\n Lastname: {item.Lastname},\n Email: {item.Email},\n PhoneNumber: {item.PhoneNumber},\n PersonalNumber: {item.PersonalNumber},\n Password: {item.Password},\n VerificationCode: {item.VerificationCode},\n IsVerified: {item.IsVerified},\n Role: {item.Role},\n Gender: {item.Gender},\n Grade: {item.Grade}\n\n");
                        //Console.WriteLine(item.ToString());
                    }

                break;

                case "3":

                    Console.WriteLine("Enter PersonalNumber");
                    string? findFromPersonalNumber = Console.ReadLine();

                    Student studentPeronalNumber = await _studentService.GetStudentByPersonalNumber(findFromPersonalNumber);

                    Console.WriteLine($" Id: {studentPeronalNumber.Id},\n FirsName: {studentPeronalNumber.FirsName},\n Lastname: {studentPeronalNumber.Lastname},\n Email: {studentPeronalNumber.Email},\n PhoneNumber: {studentPeronalNumber.PhoneNumber},\n PhoneNumber: {studentPeronalNumber.PhoneNumber},\n Password: {studentPeronalNumber.Password},\n VerificationCode: {studentPeronalNumber.VerificationCode},\n IsVerified: {studentPeronalNumber.IsVerified},\n Role: {studentPeronalNumber.Role},\n Gender: {studentPeronalNumber.Gender},\n Grade: {studentPeronalNumber.Grade}");
                    //Console.WriteLine(studentPeronalNumber.ToString());
                break;

                case "4":

                    Console.WriteLine("Enter PersonalNumber");
                    string? deleteFromPersonalNumber = Console.ReadLine();

                    await _studentService.DeleteStudentByPersonalNumber(deleteFromPersonalNumber);
                break;

                case "5":

                    Console.WriteLine("Enter student email");
                    string? studentEmail = Console.ReadLine();

                    Console.WriteLine("Enter Student VerificationCode");
                    string? StudentVerificationCode = Console.ReadLine();

                    await _studentService.VerifiStudentEmail(studentEmail, StudentVerificationCode);

                    break;

                case "6":

                    Console.WriteLine("Enter StudentUsername");
                    string? studentUserName = Console.ReadLine();

                    Console.WriteLine("Enter StudentPassword");
                    string? studentPassword = Console.ReadLine();

                    await _studentService.LogIn(studentUserName, studentPassword);

                    break;
            }
        }
    }
}
