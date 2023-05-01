using System.Xml.Linq;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "Ural", Department =Dept.IT, Email = "shresthaural100@gmai.com"},
                new Employee() {Id = 2, Name = "Sudan", Department =Dept.Hr, Email = "shresthasudan100@gmai.com"},
                new Employee() {Id = 3, Name = "Rajish", Department =Dept.Payroll, Email = "shrestharajish100@gmai.com"}
            };
         }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployeee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
