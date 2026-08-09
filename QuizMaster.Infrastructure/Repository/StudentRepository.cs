using QuizMaster.Core;
using QuizMaster.Core.Enum;
using QuizMaster.Core.Interface;
using QuizMaster.Core.Model;
using QuizMaster.Service.Exeption;
using System.Linq;
using System.Text.Json;

namespace QuizMaster.Infrastructure.Repository
{
    internal class StudentRepository : IStudentRepository
    {
        private readonly string _studentPath = "C:\\Users\\Kakha\\source\\repos\\QuizMaster.UI\\QuizMaster.Infrastructure\\Data\\Student.txt";


        public Task<List<Student>> GetAllStudent()
        {
            List<Student> students = new List<Student>();
            string[] lines = File.ReadAllLines(_studentPath);

            foreach (var line in lines)
            {
                Student? student = JsonSerializer.Deserialize<Student>(line);
                if (student != null)
                {
                    var studentData = line.Split(',');

                    Student studentAdd = new Student
                    {
                        Id = int.Parse(studentData[0]),
                        FirsName = studentData[1],
                        Lastname = studentData[2],
                        Email = studentData[3],
                        PhoneNumber = studentData[4],
                        PersonalNumber = studentData[5],
                        UserName = studentData[6],
                        Password = studentData[7],
                        VerificationCode = studentData[8],
                        IsVerified = bool.Parse(studentData[9]),
                        Role = Enum.Parse<Role>(studentData[10]),
                        Gender = Enum.Parse<Gender>(studentData[11]),
                        Grade = double.Parse(studentData[12]),
                        IsDelete = bool.Parse(studentData[13])
                    };

                    if(studentAdd.IsDelete == false)
                        students.Add(studentAdd);
                }
            }
                
            return Task.FromResult(students);
            //return students;
        }


        public Task<Student> GetStudentByPersonalNumber(string perSonalNumber)
        {
            if (string.IsNullOrWhiteSpace(perSonalNumber) || string.IsNullOrEmpty(perSonalNumber))
            {
                throw new InvalidPersonalNumberException("Personal number is null or empty.");
            }

            Student? student = GetAllStudent().Result.FirstOrDefault(x =>x.PersonalNumber == perSonalNumber);

            if(student == null)
                throw new DontFindlObjectExeption($"Student with personal number {perSonalNumber} not found.");

            return Task.FromResult(student);

        }

        public Task AddStudent(Student student)
        {
            if(student == null)
            {
                throw new ObjectEmptyException("Student object is null.");
            }

            var students = GetAllStudent().Result.ToList();


            if(students.Count == 0)
                student.Id = 1;
            else
                student.Id = students.Max(s => s.Id) + 1;


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

                return null;
        }


        public Task<string> UpdateStudent(Student student)
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

            return Task.FromResult(result);
        }

        public Task<string> DeleteStudent(string personalNumber)
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

            return null;
        }

        public Task<Student> LogInStudent(string Email, string password)
        {
            throw new NotImplementedException();
        }


        public Task<Student> VerifiStudentEmail(string email, string verifiCode)
        {
            throw new NotImplementedException();
        }
    }
}
