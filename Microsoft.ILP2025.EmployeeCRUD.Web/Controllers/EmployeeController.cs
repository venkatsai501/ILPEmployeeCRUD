using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ILP2025.EmployeeCRUD.Repositores;
using Microsoft.ILP2025.EmployeeCRUD.Servcies;
using Microsoft.ILP2025.EmployeeCRUD.Entities;

namespace Microsoft.ILP2025.EmployeeCRUD.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public IEmployeeService employeeService { get; set; }

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var employees = await this.employeeService.GetAllEmployees();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var employee = await this.employeeService.GetEmployee(id);
            return View(employee);
        }   

        public IActionResult Create(EmployeeEntity emp)
        {
            if (ModelState.IsValid)
            {
                employeeService.Create(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var emp = await employeeService.GetEmployee(id); 
            if (emp == null)
            {
                return NotFound(); 
            }
            return View(emp); 
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEntity emp)
        {

            if (ModelState.IsValid)
            {
                employeeService.Edit(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public IActionResult Delete(EmployeeEntity emp)
        {
            
            employeeService.Delete(emp);
            return View(emp);
        } 
    }
}