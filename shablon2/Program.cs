using System;
using System.Collections.Generic;

public class Task
{
	private string title;
	private string text;
	private string subject;
	private DateTime dueDate;
	private List<Student> observers = new List<Student>();

	public Task(string title, string text, string subject, DateTime dueDate)
	{
		this.title = title;
		this.text = text;
		this.subject = subject;
		this.dueDate = dueDate;
	}

	public void Attach(Student observer)
	{
		observers.Add(observer);
	}

	public void Detach(Student observer)
	{
		observers.Remove(observer);
	}

	public void NotifyObservers()
	{
		foreach (var observer in observers)
		{
			observer.Update(this);
		}
	}

	public void SetTitle(string title)
	{
		this.title = title;
		NotifyObservers();
	}

	public void SetText(string text)
	{
		this.text = text;
		NotifyObservers();
	}

	public void SetSubject(string subject)
	{
		this.subject = subject;
		NotifyObservers();
	}

	public void SetDueDate(DateTime dueDate)
	{
		this.dueDate = dueDate;
		NotifyObservers();
	}
}

public class Student
{
	private string name;

	public Student(string name)
	{
		this.name = name;
	}

	public void Update(Task task)
	{
		Console.WriteLine($"{name} отримав(ла) сповіщення про нове завдання:");
		Console.WriteLine($"Назва: {task.Title}");
		Console.WriteLine($"Предмет: {task.Subject}");
		Console.WriteLine($"Дата виконання: {task.DueDate}");
		Console.WriteLine();
	}
}

// Example
class Program
{
	static void Main()
	{
		Task mathTask = new Task("Вправи з математики", "Вирішити ряд завдань", "Математика", DateTime.Now.AddDays(7));

		Student student1 = new Student("Іван");
		Student student2 = new Student("Марія");

		mathTask.Attach(student1);
		mathTask.Attach(student2);

		mathTask.SetTitle("Вправи з математики для середньої школи");
		mathTask.SetDueDate(DateTime.Now.AddDays(5));
	}
}