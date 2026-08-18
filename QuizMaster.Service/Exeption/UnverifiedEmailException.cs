
using QuizMaster.Core;

namespace QuizMaster.Service.Exeption
{
    internal class UnverifiedEmailException : Exception
    {
        public UnverifiedEmailException()
        {
        }

        public UnverifiedEmailException(string message) : base(message)
        {
            ColloringConsole.Error(message);
        }
    }
}
