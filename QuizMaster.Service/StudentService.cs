
using BCrypt.Net;
using QuizMaster.Core;
using QuizMaster.Core.Interface;
using QuizMaster.Core.Model;
using QuizMaster.Service.Exeption;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuizMaster.Service
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentRepository.GetAllStudent();
        }

        public async Task RegistrationStudent(Student student)
        {
            if(student == null) 
                throw new ObjectEmptyException("Student object is null");

            Random random = new Random();

            student.VerificationCode = random.Next(1000, 9999).ToString();

            await _studentRepository.AddStudent(student);

            var StudentWithPersonalNumber = GetStudentByPersonalNumber(student.PersonalNumber);

            if (StudentWithPersonalNumber == null)
            {
                throw new DontFindlObjectExeption("Student not found after registration.");
            }

            EmailService.SendEmail(student.Email, "Email Verification", $"Your verification code is: {student.VerificationCode}");

        }

        public async Task<Student> GetStudentByPersonalNumber(string personalNumber)
        {
            if (string.IsNullOrWhiteSpace(personalNumber) || string.IsNullOrEmpty(personalNumber))
            {
                throw new InvalidPersonalNumberException("Personal number is null or empty.");
            }
            return await _studentRepository.GetStudentByPersonalNumber(personalNumber);
        }



        public async Task DeleteStudentByPersonalNumber(string personalNumber)
        {
            if (string.IsNullOrWhiteSpace(personalNumber) || string.IsNullOrEmpty(personalNumber))
            {
                throw new InvalidPersonalNumberException("Personal number is null or empty.");
            }
            await _studentRepository.DeleteStudent(personalNumber);
        }



        public async Task VerifiStudentEmail(string email, string verificationCode)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            {
                throw new ObjectEmptyException("Email is null or empty.");
            }

            if (string.IsNullOrWhiteSpace(verificationCode) || string.IsNullOrEmpty(verificationCode))
            {
                throw new ObjectEmptyException("Verification code is null or empty.");
            }

            Student? student =  GetAllStudents().Result.Find(s => s.Email == email);

            if (student == null)
            {
                throw new DontFindlObjectExeption("Student not found.");
            }

            var tt1 = student.VerificationCode;

            if (tt1 == verificationCode)
                student.IsVerified = true;
            else
            throw new InvalidPersonalNumberException("This is an invalid verification code.");

           await _studentRepository.UpdateStudent(student);

            ColloringConsole.Success("Email verification successful.");
        }

        public async Task<Student> LogIn(string userName, string password)
        {

            if (string.IsNullOrWhiteSpace(userName) && string.IsNullOrWhiteSpace(password))
                throw new ObjectEmptyException("Username is null or empty.");

            if (string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(password))
                throw new ObjectEmptyException("password is null or empty.");

            Student? student = await _studentRepository.GetStudentByUserName(userName);

            if(student == null)
                throw new DontFindlObjectExeption("Dont find this username");

            if (student.IsDelete)
                throw new ObjectEmptyException("This student with this username has been deleted.");

            if (student.IsVerified == false)
                throw new UnverifiedEmailException("This student's email is not verified.");

            if (BCrypt.Net.BCrypt.Verify(password, student.Password))
                throw new InvalidPersonalNumberException("Password is invalid");

            return student;

        }

    }
}
