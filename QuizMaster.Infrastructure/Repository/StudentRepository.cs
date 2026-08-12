using BCrypt.Net;
using QuizMaster.Core;
using QuizMaster.Core.Enum;
using QuizMaster.Core.Interface;
using QuizMaster.Core.Model;
using QuizMaster.Service.Exeption;
using System.Text.Json;

namespace QuizMaster.Infrastructure.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _studentPath = "C:\\Users\\Kakha\\source\\repos\\QuizMaster.UI\\QuizMaster.Infrastructure\\Data\\Student.txt";


        public async Task<List<Student>> GetAllStudent()
        {
            List<Student> students = new List<Student>();
            string[] lines = File.ReadAllLines(_studentPath);

            foreach (var line in lines)
            {
                Student? student = JsonSerializer.Deserialize<Student>(line);
                if (student != null)
                {
                    var studentData = line.Split(',');

                    //var Id00 = studentData[0];
                    //var Id0 = studentData[0].Split(':')[1];
                    //var Id01 = int.Parse(studentData[0].Split(':')[1]);

                    //var FirsName00 = studentData[4];
                    //var FirsName0 = studentData[4].Split(':')[1];

                    //var Lastname00 = studentData[5];
                    //var Lastname0 = studentData[5].Split(':')[1];

                    //var Email00 = studentData[6];
                    //var Email0 = studentData[6].Split(':')[1];

                    //var PhoneNumber00 = studentData[7];
                    //var PhoneNumber0 = studentData[7].Split(':')[1];

                    //var PersonalNumber00 = studentData[8];
                    //var PersonalNumber0 = studentData[8].Split(':')[1];

                    //var UserName00 = studentData[9];
                    //var UserName0 = studentData[9].Split(':')[1];

                    //var Password00 = studentData[10];
                    //var Password0 = studentData[10].Split(':')[1];

                    //var VerificationCode00 = studentData[11];
                    //var VerificationCode0 = studentData[11].Split(':')[1];

                    //var IsVerified00 = studentData[12];
                    //var IsVerified0 = studentData[12].Split(':')[1];
                    //var IsVerified = bool.Parse(studentData[12].Split(':')[1]);

                    //var role00 = studentData[13];
                    //var role0 = studentData[13].Split(':')[1];
                    //var Role = Enum.Parse<Role>(studentData[13].Split(':')[1]);

                    //var Gender00 = studentData[14];
                    //var Gender0 = studentData[14].Split(':')[1];
                    //var Gender = Enum.Parse<Gender>(studentData[14].Split(':')[1][0].ToString());

                    //var Grade00 = studentData[1];
                    //var Grade0 = studentData[1].Split(':')[1];
                    //var Grade = double.Parse(studentData[1].Split(':')[1]);

                    //var IsDelete00 = studentData[2];
                    //var IsDelete0 = studentData[2].Split(':')[1];
                    //var IsDelete = bool.Parse(studentData[2].Split(':')[1]);

                    //var subject000 = studentData[3];
                    //var subject0000 = studentData[3].Split(':')[1];

                    Student studentAdd = new Student
                    {
                        Id = int.Parse(studentData[0].Split(':')[1]),
                        FirsName = studentData[4].Split(':')[1].Trim('"'),
                        Lastname = studentData[5].Split(':')[1].Trim('"'),
                        Email = studentData[6].Split(':')[1].Trim('"'),
                        PhoneNumber = studentData[7].Split(':')[1].Trim('"'),
                        PersonalNumber = studentData[8].Split(':')[1].Trim('"'),
                        UserName = studentData[9].Split(':')[1].Trim('"'),
                        Password = studentData[10].Split(':')[1].Trim('"'),
                        VerificationCode = studentData[11].Split(':')[1].Trim('"'),
                        IsVerified = bool.Parse(studentData[12].Split(':')[1]),
                        Role = Enum.Parse<Role>(studentData[13].Split(':')[1]),
                        Gender = Enum.Parse<Gender>(studentData[14].Split(':')[1][0].ToString()),
                        Grade = double.Parse(studentData[1].Split(':')[1]),
                        IsDelete = bool.Parse(studentData[2].Split(':')[1]),
                        //Subjects = studentData[3].Split(':')[1]
                    };

                    if(studentAdd.IsDelete == false)
                        students.Add(studentAdd);
                }
            }

            
            //return Task.FromResult(students);
            return students;
        }


        public async Task<Student> GetStudentByPersonalNumber(string perSonalNumber)
        {
            if (string.IsNullOrWhiteSpace(perSonalNumber) || string.IsNullOrEmpty(perSonalNumber))
            {
                throw new InvalidPersonalNumberException("Personal number is null or empty.");
            }

            Student? student = GetAllStudent().Result.FirstOrDefault(x =>x.PersonalNumber == perSonalNumber);

            if(student == null)
                throw new DontFindlObjectExeption($"Student with personal number {perSonalNumber} not found.");

            return student;

        }

        public async Task AddStudent(Student student)
        {
            if(student == null)
            {
                throw new ObjectEmptyException("Student object is null.");
            }

            var students = GetAllStudent().Result;


            if(students.Count == 0)
                student.Id = 1;
            else
                student.Id = students.Max(s => s.Id) + 1;

            student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
            string studentnew = JsonSerializer.Serialize(student);

            if(string.IsNullOrWhiteSpace(studentnew) || string.IsNullOrEmpty(studentnew))
            {
                throw new InvalidDataException("Serialized student data is null or empty.");
            }

            if (students.Any(s => s.PersonalNumber == student.PersonalNumber))
            {
                throw new DuplicatePersonalNumberException($"A student with personal number {student.PersonalNumber} already exists.");
            }

            File.AppendAllText(_studentPath, studentnew + Environment.NewLine);

            Student? addedStudent = GetAllStudent().Result.FirstOrDefault(x => x.PersonalNumber == student.PersonalNumber);

            //string result = addedStudent != null ? $"Student with personal number {addedStudent.PersonalNumber} added successfully." : $"Failed to add student with personal number {student.PersonalNumber}.";

            if (addedStudent != null)
                ColloringConsole.Success($"Student with personal number {addedStudent.PersonalNumber} added successfully.");
            else
                ColloringConsole.Error($"Failed to add student with personal number {student.PersonalNumber}.");

                //return null;
        }


        public async Task<string> UpdateStudent(Student student)
        {
            if (student == null)
            {
                throw new ObjectEmptyException("Student object is null.");
            }

            List<Student> students = GetAllStudent().Result.ToList();

            int oldCount = students.Count;

            int existingStudentId = students.FirstOrDefault(s => s.PersonalNumber == student.PersonalNumber).Id;

            if (existingStudentId > 0)
            {
                students[existingStudentId].FirsName = student.FirsName;
                students[existingStudentId].Lastname = student.Lastname;
                students[existingStudentId].Email = student.Email;
                students[existingStudentId].PhoneNumber = student.PhoneNumber;
                students[existingStudentId].PersonalNumber = student.PersonalNumber;
                students[existingStudentId].UserName = student.UserName;
                students[existingStudentId].Password = student.Password;
                students[existingStudentId].VerificationCode = student.VerificationCode;
                students[existingStudentId].IsVerified = student.IsVerified;
                students[existingStudentId].Role = student.Role;
                students[existingStudentId].Gender = student.Gender;
                students[existingStudentId].Grade = student.Grade;
                students[existingStudentId].IsDelete = student.IsDelete;
            }
            else
                throw new DontFindlObjectExeption($"Student with personal number {student.PersonalNumber} not found.");

            int newCount = 0;

            using(StreamWriter writer = new StreamWriter(_studentPath, true))
            {
                writer.WriteLine();

                foreach (var item in students)
                {
                    writer.WriteLine(item);
                    newCount++;
                }
            }

            string result = "";

            result = oldCount == newCount ? $"Student with personal number {student.PersonalNumber} updated successfully." : $"Failed to update student with personal number {student.PersonalNumber}.";

            return result;
        }

        public async Task DeleteStudent(string personalNumber)
        //public Task DeleteStudent(string personalNumber)
        {
            if(string.IsNullOrEmpty(personalNumber) || string.IsNullOrWhiteSpace(personalNumber))
            {
                throw new InvalidPersonalNumberException("Personal number is null or empty.");
            }

            List<Student> students = GetAllStudent().Result.ToList();

            if(students.Count > 0)
            {
                Student? studentToDelete = students.FirstOrDefault(s => s.PersonalNumber == personalNumber);
                if (studentToDelete != null)
                {
                    studentToDelete.IsDelete = true;
                    UpdateStudent(studentToDelete);
                    ColloringConsole.Success($"Student with personal number {personalNumber} deleted successfully.");
                }
                else
                {
                    throw new DontFindlObjectExeption($"Student with personal number {personalNumber} not found.");
                }
            }
            else
            {
                throw new DontFindlObjectExeption("No students found to delete.");
            }

        }

        //public Task<Student> LogInStudent(string Email, string password)
        //{
        //    throw new NotImplementedException();
        //}


        //public Task<Student> VerifiStudentEmail(string email, string verifiCode)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
