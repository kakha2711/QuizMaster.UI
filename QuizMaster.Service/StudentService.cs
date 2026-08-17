
using BCrypt.Net;
using QuizMaster.Core.Interface;
using QuizMaster.Core.Model;
using QuizMaster.Service.Exeption;

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

            if(student.VerificationCode == verificationCode)
                student.IsVerified = true;

            throw new InvalidPersonalNumberException("This is an invalid verification code.");

            string tt = _studentRepository.UpdateStudent(student).Result;
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

            if (BCrypt.Net.BCrypt.Verify(password, student.Password))
                throw new InvalidPersonalNumberException("Password is invalid");

            return student;

        }

    }
}
