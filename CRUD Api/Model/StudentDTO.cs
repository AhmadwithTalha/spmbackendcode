namespace CRUD_Api.Model
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Fathername { get; set; } = string.Empty;
        public DateOnly Dateofbirth { get; set; } 
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public int DepartmentID { get; set; }

        public bool Gender { get; set; }
        //public string DepartmentName { get; set;} 

    }
}
