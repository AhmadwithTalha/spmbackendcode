using System.ComponentModel.DataAnnotations;

namespace CRUD_Api.Model
{
    public class crudclass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Fathername { get; set; } = string.Empty;
        public DateOnly Dateofbirth { get; set; }  
        public bool Gender { get; set;}
        public string Password { get; set; } = string.Empty;

        //public int DepartmentID { get; set; } // FK
        //public Department? Department { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }  // Navigation property


    }
}
