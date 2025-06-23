using Microsoft.ILP2025.EmployeeCRUD.Entities;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public static List<EmployeeEntity> employees = new List<EmployeeEntity>();

        private readonly string _jsonFilePath = "App_Data/employees.json";

        public EmployeeRepository()
        {
            if (!Directory.Exists("App_Data"))
                Directory.CreateDirectory("App_Data");

            if (!File.Exists(_jsonFilePath))
                File.WriteAllText(_jsonFilePath, "[]");

            // Load data from JSON file at startup
            employees = GetEmployeeDetails();
        }

        public async Task<List<EmployeeEntity>> GetAllEmployees()
        {
            return await Task.FromResult(this.GetEmployees());
        }

        public async Task<EmployeeEntity> GetEmployee(int id)
        {
            var employees = this.GetEmployees();

            return await Task.FromResult(employees.FirstOrDefault(e => e.Id == id));
        }

        private List<EmployeeEntity> GetEmployees()
        {
            

            // employees.Add(new EmployeeEntity { Id = 1, Name = "Pradip" });
            // employees.Add(new EmployeeEntity { Id = 2, Name = "Shrikanth" });

            return employees;
        }

        public void Create(EmployeeEntity emp)
        {
            emp.Id = employees.Count + 1;
            employees.Add(emp);
            SaveEmployees(); 
            // Console.Write("Added Successfully");
        }

        public void Edit(EmployeeEntity emp)
        {
            var employee = employees.FirstOrDefault(e => e.Id == emp.Id);
            if (employee != null)
            {
                employee.Name = emp.Name;
                employee.Department = emp.Department;
                employee.Age = emp.Age;
                employee.Salary = emp.Salary;
                SaveEmployees(); 
                // Console.Write("Updated Successfully");
            }
        }
        public void Delete(EmployeeEntity emp)
        {
            var employ = employees.FirstOrDefault(e => e.Id == emp.Id);
            if (employ != null)
            {
                employees.Remove(employ);
                SaveEmployees(); 
                // Console.WriteLine("Deleted Successfully");
            }
        }

         //Serialization Method: Save to JSON
        private void SaveEmployees()
        {
            var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_jsonFilePath, json);
            employees = GetEmployeeDetails();
        }

        // Deserialization Method: Load from JSON
        private List<EmployeeEntity> GetEmployeeDetails()
        {
            try
            {
                string json = File.ReadAllText(_jsonFilePath);
                return JsonSerializer.Deserialize<List<EmployeeEntity>>(json) ?? new List<EmployeeEntity>();
            }
            catch
            {
                return new List<EmployeeEntity>();
            }
        }
    }
}