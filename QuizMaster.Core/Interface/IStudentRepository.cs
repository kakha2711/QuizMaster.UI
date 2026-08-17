
using QuizMaster.Core.Model;

namespace QuizMaster.Core.Interface
{
    public interface IStudentRepository
    {
        
        public Task<List<Student>> GetAllStudent();
        public Task<Student> GetStudentByPersonalNumber(string perSonalNumber);
        public Task AddStudent(Student student);
        public Task<string> UpdateStudent(Student student);
        public Task DeleteStudent(string personalNumber);

        public Task<Student> GetStudentByUserName(string username);
        

    }
}
