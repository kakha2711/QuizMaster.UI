
using QuizMaster.Core.Interface;

namespace QuizMaster.Service
{
    internal class StudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


    }
}
