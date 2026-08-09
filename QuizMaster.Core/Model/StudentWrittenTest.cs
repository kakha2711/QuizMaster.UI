
using QuizMaster.Core.Enum;

namespace QuizMaster.Core.Model
{
    internal class StudentWrittenTest
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string TestCatalogId { get; set; }
        public string Answer { get; set; }

        
        //public Progres progres { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
