
using QuizMaster.Core.Model;

namespace QuizMaster.Core.Interface
{
    public interface IStudentRepository
    {
        
        public Task<List<Student>> GetAllStudent();
        public Task<Student> GetStudentByPersonalNumber(string perSonalNumber);
        public Task AddStudent(Student student);
        public Task<string> UpdateStudent(Student student);
        public Task<string> DeleteStudent(string personalNumber);

        public Task<Student> LogInStudent(string Email, string password);
        public Task<Student> VerifiStudentEmail(string email, string verifiCode);
    

    }
}
