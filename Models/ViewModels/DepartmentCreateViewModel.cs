namespace UniversityApp.ViewModels
{
    public class DepartmentCreateViewModel
    {
        public string Name { get; set; }
        public List<string> ExistingDepartments { get; set; } = new List<string>();
    }
}
