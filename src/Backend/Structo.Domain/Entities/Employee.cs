namespace Structo.Domain.Entities
{
    public class Employee : EntityBase
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty; //Cargo
        public string EmployeePhone { get; set; } = string.Empty;//Telefone
        public long EmployeeId { get; set; }
        public long CompanyId { get; set; }
    }
}
