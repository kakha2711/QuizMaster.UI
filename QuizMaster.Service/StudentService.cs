
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

    }
}
