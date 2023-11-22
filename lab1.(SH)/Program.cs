using System;
using System.Collections.Generic;
// використовуємо породжуючий шаблон "Фабрика"
class Student
{
	public string LastName { get; set; }
	public string FirstName { get; set; }
	public string Specialty { get; set; }
	public int Course { get; set; }
	public List<int> Grades { get; set; }

	public Student(string lastName, string firstName, string specialty, int course, List<int> grades)
	{
		LastName = lastName;
		FirstName = firstName;
		Specialty = specialty;
		Course = course;
		Grades = grades;
	}
}

class StudentFactory
{
	private Random random = new Random();
	private List<string> lastNames = new List<string> { "Островська", "Боднар", "Барно", "Козачук", "Іванов", "Марощук" };
	private List<string> firstNames = new List<string> { "Кароліна", "Олександра", "Михайло", "Ольга", "Євген", "Дмитро" };

	public List<Student> GenerateStudents(int numStudents, int numSubjects, string specialty, int course)
	{
		List<Student> students = new List<Student>();

		for (int i = 0; i < numStudents; i++)
		{
			string lastName = lastNames[random.Next(lastNames.Count)];
			string firstName = firstNames[random.Next(firstNames.Count)];
			List<int> grades = GenerateRandomGrades(numSubjects);

			Student student = new Student(lastName, firstName, specialty, course, grades);
			students.Add(student);
		}

		return students;
	}

	private List<int> GenerateRandomGrades(int numSubjects)
	{
		List<int> grades = new List<int>();

		for (int i = 0; i < numSubjects; i++)
		{
			grades.Add(random.Next(1, 101));
		}

		return grades;
	}
}

class Program
{
	static void Main()
	{
		int numStudents = 6;
		int numSubjects = 6;
		string specialty = "Системний аналіз";
		int course = 2;

		StudentFactory factory = new StudentFactory();
		List<Student> group = factory.GenerateStudents(numStudents, numSubjects, specialty, course);

		// Виведення даних студентів в консоль
		foreach (var student in group)
		{
			Console.WriteLine($"Прізвище: {student.LastName}, Ім'я: {student.FirstName}, Спеціальність: {student.Specialty}, " +
				$"Курс: {student.Course}, Бали: {string.Join(", ", student.Grades)}");
		}
	}
}