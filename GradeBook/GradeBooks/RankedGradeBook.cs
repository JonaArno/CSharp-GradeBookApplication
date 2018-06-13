using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook:BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5) throw new InvalidOperationException();

            var treshold = (int)Math.Ceiling(Students.Count * 0.2);
            var treshold2 = (int)Math.Ceiling(Students.Count * 0.4);
            var treshold3 = (int) Math.Ceiling(Students.Count * 0.6);
            var treshold4 = (int) Math.Ceiling(Students.Count * 0.8);

            var allGradesDesc = Students
                .OrderByDescending(e => e.AverageGrade)
                .Select(e => e.AverageGrade)
                .ToList();

            if (allGradesDesc[treshold - 1] <= averageGrade) return 'A';
            else if (allGradesDesc[treshold - 1] > averageGrade && allGradesDesc[treshold2 - 1] <= averageGrade)
                return 'B';
            else if (allGradesDesc[treshold2 - 1] > averageGrade &&
                     allGradesDesc[treshold3 - 1] <= averageGrade) return 'C';
            else if (allGradesDesc[treshold3 - 1] > averageGrade &&
                     allGradesDesc[treshold4 - 1] <= averageGrade) return 'D';
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5) Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade");
            else base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5 ) Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else base.CalculateStudentStatistics(name);
        }
    }
}
