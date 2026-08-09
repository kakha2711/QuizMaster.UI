
using QuizMaster.Core;

namespace QuizMaster.Service.Exeption
{
    public class DontFindlObjectExeption : Exception
    {
        public DontFindlObjectExeption()
        {
        }

        public DontFindlObjectExeption(string? message) : base(message)
        {
            ColloringConsole.Error(message);
        }
    }
}
