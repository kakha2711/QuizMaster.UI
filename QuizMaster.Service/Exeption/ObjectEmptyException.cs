
using QuizMaster.Core;

namespace QuizMaster.Service.Exeption
{
    public class ObjectEmptyException : Exception
    {
        public ObjectEmptyException()
        {
        }

        public ObjectEmptyException(string? message) : base(message)
        {
            ColloringConsole.Error(message);
        }
    }
}
