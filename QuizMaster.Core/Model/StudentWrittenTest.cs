
using QuizMaster.Core.Enum;

namespace QuizMaster.Core.Model
{
    internal class StudentWrittenTest
    {
        public string FirsName { get; set; }
        public string Lastname { get; set; }
        public string PersonalNumber { get; set; }
        public string TestCatalogTitle { get; set; }
        public string TestCatalogTopic { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string CorrectAnswer { get; set; }
        public Progres progres { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
