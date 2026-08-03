
using QuizMaster.Core.Model;

namespace QuizMaster.Core.Interface
{
    public interface IQuizMasterReposytory
    {
        #region Student
        /*ესეინი სუყველა ფუნქცია ლექტორმა უნდა გააკეთოს */
        public  Task<T> GetAllStudentAndLecture<T>();
        public Task<T> GetStudentAndLectureByPersonalNumber<T>(string perSonalNumber);
        public Task<T> AddStudentAndLecture<T>(Student student);
        public Task<T> UpdateStudentAndLecture<T>(Student student);
        public Task<T> DeleteStudentAndLecture<T>(string personalNumber);

        public Task<T> LogInStudentAndLecture<T>(string Email, string password);
        public Task<T> VerifiStudentAndLectureEmail<T>(string email, string verifiCode);
        #endregion



        //#region Lecturer
        //public Task<Student> GetAllLecturer();
        //public Task<Student> GetLecturerByPersonalNumber(string perSonalNumber);
        //public Task<Student> AddLecturer(Lecturer lecturer);
        //public Task<Student> UpdateLecturer(Lecturer lecturer);
        //public Task<Student> DeleteLecturer(string personalNumber);

        //public Task<Student> LogInLecturer(string Email, string password);
        //public Task<Student> VerifiLecturerEmail(string email, string verifiCode);

        //#endregion



        public Task<List<QuestionTest>> GetAllQuestionTest();

    }
}
