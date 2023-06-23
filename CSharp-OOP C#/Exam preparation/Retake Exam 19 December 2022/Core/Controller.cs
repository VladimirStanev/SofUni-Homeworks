using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private string[] allowedcatoegories = new string[]
        { "TechnicalSubject", "EconomicalSubject", "HumanitySubject" };

        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if(!allowedcatoegories.Contains(subjectType))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            if(subjects.FindByName(subjectName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            Subject subject = null;
            if(subjectType == typeof(TechnicalSubject).Name)
            {
                subject = new TechnicalSubject(0, subjectName);
            }
            if (subjectType == typeof(EconomicalSubject).Name)
            {
                subject = new EconomicalSubject(0, subjectName);
            }
            if (subjectType == typeof(HumanitySubject).Name)
            {
                subject = new HumanitySubject(0, subjectName);
            }

            subjects.AddModel(subject);

            return string.Format(OutputMessages.SubjectAddedSuccessfully,
                subject.GetType().Name, subjectName, subjects.GetType().Name);
        }


        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if(universities.FindByName(universityName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            List<int> requiredSubjectsAsIntegers = requiredSubjects.Select
                (s => subjects.FindByName(s).Id).ToList();

            University university = new University
                (0, universityName, category, capacity, requiredSubjectsAsIntegers);

            universities.AddModel(university);

            return string.Format(OutputMessages.UniversityAddedSuccessfully,
                universityName, universities.GetType().Name);
        }

         
        public string AddStudent(string firstName, string lastName)
        {
            if(students.FindByName($"{firstName} {lastName}") != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            Student student = new Student(0, firstName, lastName);
            students.AddModel(student);

            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName,
                students.GetType().Name);
        }


        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            if (student == null)
            {
                return OutputMessages.InvalidStudentId;
            }

            ISubject subject = subjects.FindById(subjectId);
            if(subject == null)
            {
                return OutputMessages.InvalidSubjectId;
            }

            if(student.CoveredExams.Contains(subjectId))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, 
                    student.FirstName, student.LastName, subject.Name);
            }

            student.CoverExam(subject);

            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam,
                    student.FirstName, student.LastName, subject.Name);
        }


        public string ApplyToUniversity(string studentName, string universityName)
        {
            IStudent student = students.FindByName(studentName);
            if(student == null)
            {
                return string.Format($"{studentName} is not registered in the application!");
            }

            IUniversity university = universities.FindByName(universityName);
            if (university == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            foreach (var requiredExam in university.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(requiredExam))
                {
                    return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                }
            }

            if(student.University != null && student.University.Name == university.Name)
            {
                return string.Format(OutputMessages.StudentAlreadyJoined,
                    student.FirstName, student.LastName, universityName);
            }

            student.JoinUniversity(university);

            return string.Format(OutputMessages.StudentSuccessfullyJoined,
                    student.FirstName, student.LastName, universityName);
        }


        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            StringBuilder sb = new StringBuilder();
            int studentThatAreAdmitted = CountStudents(university);

            sb.AppendLine($"*** {university.Name} ***")
                .AppendLine($"Profile: {university.Category}")
                .AppendLine($"Students admitted: {studentThatAreAdmitted}")
                .AppendLine($"University vacancy: {university.Capacity - studentThatAreAdmitted}");

            return sb.ToString().TrimEnd();

        }

        private int CountStudents(IUniversity university)
        {
            int count = 0;
            foreach(var student in students.Models)
            {
                if(student.University?.Id == university.Id)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
