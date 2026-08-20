
using QuizMaster.Core.Model;

namespace QuizMaster.Core.Interface
{
    public interface IStudentRepository
    {
        
        public Task<List<T>> GetAllStudent<T>(string role) where T : Person;
        public Task<Student> GetStudentByPersonalNumber(string perSonalNumber);
        public Task AddStudent<T>(T student) where T : Person;
        public Task<string> UpdateStudent(Student student);
        public Task DeleteStudent(string personalNumber);

        public Task<Student> GetStudentByUserName(string username);
        

    }
}
