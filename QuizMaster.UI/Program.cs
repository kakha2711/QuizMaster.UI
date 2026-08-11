using QuizMaster.Core.Interface;
using QuizMaster.Infrastructure.Repository;
using QuizMaster.Service;

namespace QuizMaster.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Menu menu = new Menu(new StudentService(new StudentRepository()));

            menu.ShowMenu();
        }
    }
}
