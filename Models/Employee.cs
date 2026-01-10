using System.ComponentModel.DataAnnotations;

namespace DockerDeep.Model
{
   
        public class Employee
        {
            [Key]
            public int empId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string sal { get; set; } = string.Empty;
            public string dept { get; set; }
        }
    

}
