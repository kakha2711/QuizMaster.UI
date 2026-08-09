
using QuizMaster.Core;

namespace QuizMaster.Service.Exeption
{
    public class InvalidPersonalNumberException : Exception
    {
        public InvalidPersonalNumberException()
        {
        }

        public InvalidPersonalNumberException(string? message) : base(message)
        {
            ColloringConsole.Error(message);
        }
    }
}
