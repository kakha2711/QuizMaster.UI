// ეს ენუმი გავარკვიო კარგად რა აზრი დევს  ეს ხაზია ტესტის დაწყება: სტუდენტი ირჩევს ტესტს; იქმნება Attempt საწყისი დროითა და InProgress სტატუსით.
//ალბათ ესენია ტესტის სტატუსები, Attempt ნიშნავს რომ სტუდენტმა დაიწყო ტესტი და InProgress ნიშნავს რომ ტესტი მიმდინარეობს.
namespace QuizMaster.Core.Enum
{
    public enum CompletedOngoingTests
    {
        Attempt,
        InProgress
    }
}
