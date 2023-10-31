using System;
using System.Collections.Generic;
using System.Linq;

public class Doctor
{
	public string LastName { get; set; }
	public string Specialization { get; set; }
}

public class Patient
{
	public string LastName { get; set; }
	public string DoctorLastName { get; set; }
	public string Diagnosis { get; set; }
	public DateTime AdmissionDate { get; set; }
	public DateTime DischargeDate { get; set; }
}

public class Hospital
{
	public List<Doctor> Doctors { get; set; }
	public List<Patient> Patients { get; set; }

	public Hospital()
	{
		Doctors = new List<Doctor>();
		Patients = new List<Patient>();
	}

	// Завдання а)
	public void TaskA(string patientLastName, int currentYear)
	{
		var doctorsWithDiagnosis = Patients
			.Where(p => p.LastName == patientLastName &&
						p.AdmissionDate.Year == currentYear)
			.Join(Doctors, p => p.DoctorLastName, d => d.LastName,
				  (p, d) => d)
			.Distinct();

		Console.WriteLine($"Лікарі, які ставили діагнози {patientLastName} у {currentYear} році:");
		foreach (var doctor in doctorsWithDiagnosis)
		{
			Console.WriteLine(doctor.LastName);
		}
	}

	// Завдання б)
	public void TaskB()
	{
		var patientsDuration = Patients
			.GroupBy(p => p.LastName)
			.Select(group => new
			{
				LastName = group.Key,
				TotalDuration = group.Sum(p => (p.DischargeDate - p.AdmissionDate).Days)
			});

		Console.WriteLine("Прізвища пацієнтів та сумарна тривалість їх перебування у лікарні:");
		foreach (var patient in patientsDuration)
		{
			Console.WriteLine($"{patient.LastName}: {patient.TotalDuration} днів");
		}
	}

	// Завдання в)
	public void TaskC()
	{
		var doctorSpecializations = Doctors
			.GroupJoin(Patients, d => d.LastName, p => p.DoctorLastName,
					   (doctor, patients) => new
					   {
						   doctor.Specialization,
						   DiagnosisCount = patients.Count()
					   })
			.OrderByDescending(d => d.DiagnosisCount)
			.First();

		Console.WriteLine($"Спеціалізації лікарів, лікарі яких загалом поставили найбільше діагнозів:");
		Console.WriteLine(doctorSpecializations.Specialization);
	}
}

class Program
{
	static void Main()
	{
		Hospital hospital = new Hospital();

		// Ініціалізація лікарів
		hospital.Doctors.Add(new Doctor { LastName = "Лікар1", Specialization = "Хірург" });
		hospital.Doctors.Add(new Doctor { LastName = "Лікар2", Specialization = "Терапевт" });

		// Ініціалізація пацієнтів
		hospital.Patients.Add(new Patient { LastName = "Пацієнт1", DoctorLastName = "Лікар1", Diagnosis = "Грип", AdmissionDate = new DateTime(2023, 1, 1), DischargeDate = new DateTime(2023, 1, 5) });
		hospital.Patients.Add(new Patient { LastName = "Пацієнт2", DoctorLastName = "Лікар2", Diagnosis = "Зламана нога", AdmissionDate = new DateTime(2023, 2, 1), DischargeDate = new DateTime(2023, 2, 10) });

		// Виклик методів для вирішення завдань
		hospital.TaskA("Пацієнт1", 2023);
		hospital.TaskB();
		hospital.TaskC();

		Console.ReadLine();
	}
}
