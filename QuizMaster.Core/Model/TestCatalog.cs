
using FluentValidation;

namespace QuizMaster.Core.Model
{
    public class TestCatalog
    {
        public int Id { get; set; }
        public string TestTitle { get; set; }
        public string Topic { get; set; }
        public double QuctionsNumber { get; set; }
        public double MaximumScore { get; set; }
        public byte DateTime { get; set; }
        public double PassingPercentage { get; set; }
        public bool IsDelete { get; set; } = false;


        //public Lecturer lecturer { get; set; }
        public string LecturerFirsName { get; set; }
        public string LecturerLastname { get; set; }
        public string LecturerPersonalNumber { get; set; }


        public override string? ToString()
        {
            return $"Id: {Id}, TestTitle: {TestTitle}, Topic: {Topic}, QuctionsNumber: {QuctionsNumber}, MaximumScore: {MaximumScore}," +
                    $"DateTime: {DateTime}, PassingPercentage: {PassingPercentage}, LecturerFirsName: {LecturerFirsName}," +
                    $"LecturerLastname: {LecturerLastname}, LecturerPersonalNumber: {LecturerPersonalNumber}";
        }
    }

    public class TestCatalogValidator : AbstractValidator<TestCatalog>
    {
        public TestCatalogValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.TestTitle).NotEmpty().WithMessage("TestTitle is required.");
            RuleFor(x => x.Topic).NotEmpty().WithMessage("Topic is required.");
            RuleFor(x => x.QuctionsNumber).GreaterThan(0).WithMessage("QuctionsNumber must be greater than 0.");
            RuleFor(x => x.MaximumScore).GreaterThan(0).WithMessage("MaximumScore must be greater than 0.");
            RuleFor(x => x.DateTime).InclusiveBetween((byte)1, (byte)8).WithMessage("DateTime must be greater than 0.");
            RuleFor(x => x.PassingPercentage).InclusiveBetween(0, 100).WithMessage("PassingPercentage must be between 0 and 100.");
            RuleFor(x => x.LecturerFirsName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LecturerLastname).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.LecturerPersonalNumber)
                .NotEmpty()
                .WithMessage("Personal number is required.")
                .Matches(@"[0-9]")
                .WithMessage("Personal number must be only numbers");
        }
    }


}
