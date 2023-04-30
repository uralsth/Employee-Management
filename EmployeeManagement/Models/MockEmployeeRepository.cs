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
                new Employee() {Id = 1, Name = "Ural", Department = "IT", Email = "shresthaural100@gmai.com"},
                new Employee() {Id = 2, Name = "Sudan", Department = "HR", Email = "shresthasudan100@gmai.com"},
                new Employee() {Id = 3, Name = "Rajish", Department = "IT", Email = "shrestharajish100@gmai.com"}
            };
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
