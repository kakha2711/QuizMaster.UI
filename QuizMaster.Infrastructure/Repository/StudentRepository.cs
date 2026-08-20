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
        private readonly string _lecturePath = "C:\\Users\\Kakha\\source\\repos\\QuizMaster.UI\\QuizMaster.Infrastructure\\Data\\Lecture.txt";

        public async Task<List<T>> GetAllStudent<T>(string role) where T : Person
        {
            List<T> students = new List<T>();

            if (role == "Student")
            {

                string[] lines = File.ReadAllLines(_studentPath);

                foreach (var line in lines)
                {
                    Student? student = JsonSerializer.Deserialize<Student>(line);
                    if (student != null)
                    {
                        var studentData = line.Split(',');



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

                        //if (studentAdd.IsDelete == false)
                        students.Add(studentAdd as T);
                    }
                    else
                        return null;
                }

            }

            if (role == "Lecturer")
            {
                string[] lines = File.ReadAllLines(_lecturePath);

                foreach (string line in lines)
                {

                }
            }
            return students;
        }


        public async Task<Student> GetStudentByPersonalNumber(string perSonalNumber)
        {
            //if (string.IsNullOrWhiteSpace(perSonalNumber) || string.IsNullOrEmpty(perSonalNumber))
            //{
            //    throw new InvalidPersonalNumberException("Personal number is null or empty.");
            //}

            //Student? student = GetAllStudent().Result.FirstOrDefault(x => x.PersonalNumber == perSonalNumber);

            //if (student == null)
            //    throw new DontFindlObjectExeption($"Student with personal number {perSonalNumber} not found.");

            //return student;

            throw new NotImplementedException();

        }

        public async Task AddStudent<T>(T student1) where T : Person
        {

            if (student1 == null)
            {
                throw new ObjectEmptyException("Student object is null.");
            }

            if (student1 is Student student)
            {
                var students = GetAllStudent(student.Role).Result;


                if (students.Count == 0)
                    student.Id = 1;
                else
                    student.Id = students.Max(s => s.Id) + 1;

                student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
                string studentnew = JsonSerializer.Serialize(student);

                if (string.IsNullOrWhiteSpace(studentnew) || string.IsNullOrEmpty(studentnew))
                {
                    throw new InvalidDataException("Serialized student data is null or empty.");
                }

                if (students.Any(s => s.PersonalNumber == student.PersonalNumber))
                {
                    throw new DuplicatePersonalNumberException($"A student with personal number {student.PersonalNumber} already exists.");
                }

                if (students.Count == 0)
                    File.AppendAllText(_studentPath, studentnew);
                else
                    File.AppendAllText(_studentPath, Environment.NewLine + studentnew);

                Student? addedStudent = GetAllStudent().Result.FirstOrDefault(x => x.PersonalNumber == student.PersonalNumber);


                if (addedStudent != null)
                    ColloringConsole.Success($"Student with personal number {addedStudent.PersonalNumber} added successfully.");
                else
                    ColloringConsole.Error($"Failed to add student with personal number {student.PersonalNumber}.");
            }

            if (student1 is Lecturer lecturer)
            {
                // Handle Lecturer addition logic here if needed
                //aq unda davamatoleqtures kodi
            }

            
        }


        public async Task<string> UpdateStudent(Student student)
        {
            //if (student == null)
            //{
            //    throw new ObjectEmptyException("Student object is null.");
            //}

            //List<Student> students = await GetAllStudent();

            //int oldCount = students.Count;

            //var existingStudentId = students.FindIndex(s => s.PersonalNumber == student.PersonalNumber);

            //students[existingStudentId].FirsName = student.FirsName;
            //students[existingStudentId].Lastname = student.Lastname;
            //students[existingStudentId].Email = student.Email;
            //students[existingStudentId].PhoneNumber = student.PhoneNumber;
            //students[existingStudentId].PersonalNumber = student.PersonalNumber;
            //students[existingStudentId].UserName = student.UserName;
            //students[existingStudentId].Password = student.Password;
            //students[existingStudentId].VerificationCode = student.VerificationCode;
            //students[existingStudentId].IsVerified = student.IsVerified;
            //students[existingStudentId].Role = student.Role;
            //students[existingStudentId].Gender = student.Gender;
            //students[existingStudentId].Grade = student.Grade;
            //students[existingStudentId].IsDelete = student.IsDelete;


            //int newCount = 0;

            ////using (StreamWriter writer = new StreamWriter(_studentPath, false))
            ////{

            ////}

            ////using (StreamWriter writer = new StreamWriter(_studentPath, true))
            ////{
            ////    foreach (var item in students)
            ////    {
            ////        writer.WriteLine(item);
            ////        newCount++;
            ////    }
            ////}

            //File.WriteAllText(_studentPath, string.Empty);

            //foreach (var item in students)
            //{
            //    //File.AppendAllText(_studentPath, item + Environment.NewLine);
            //    //AddStudent(item);

            //    string studentnew = JsonSerializer.Serialize(item);

            //    var existingStudent = GetAllStudent().Result;

            //    if (existingStudent.Count == 0)

            //        File.AppendAllText(_studentPath, studentnew);
            //    else
            //        File.AppendAllText(_studentPath, Environment.NewLine + studentnew);
            //}


            //string result = "";

            //result = oldCount == newCount ? $"Student with personal number {student.PersonalNumber} updated successfully." : $"Failed to update student with personal number {student.PersonalNumber}.";

            //return result;

            throw new NotImplementedException();

        }

        public async Task DeleteStudent(string personalNumber)
        {
            //if (string.IsNullOrEmpty(personalNumber) || string.IsNullOrWhiteSpace(personalNumber))
            //{
            //    throw new InvalidPersonalNumberException("Personal number is null or empty.");
            //}

            //List<Student> students = await GetAllStudent();

            //if (students.Count > 0)
            //{
            //    Student? studentToDelete = students.FirstOrDefault(s => s.PersonalNumber == personalNumber);
            //    if (studentToDelete != null)
            //    {
            //        studentToDelete.IsDelete = true;
            //        await UpdateStudent(studentToDelete);
            //        ColloringConsole.Success($"Student with personal number {personalNumber} deleted successfully.");
            //    }
            //    else
            //    {
            //        throw new DontFindlObjectExeption($"Student with personal number {personalNumber} not found.");
            //    }
            //}
            //else
            //{
            //    throw new DontFindlObjectExeption("No students found to delete.");
            //}

            throw new NotImplementedException();

        }

        public async Task<Student> GetStudentByUserName(string username)
        {
            //if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(username))
            //{
            //    throw new InvalidPersonalNumberException("Username is null or empty.");
            //}
            //var students = await GetAllStudent();
            //return students.FirstOrDefault(s => s.UserName == username);

            throw new NotImplementedException();
        }
    }
}
