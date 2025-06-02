namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Empleados
{
    public class EmpleadoDtoDx
    {
        public string CompanyMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string IsActive { get; set; }

        public EmpleadoDtoDx()
        {
        }

        public EmpleadoDtoDx(string companyMemberId, string firstName, string lastName, string password, string isActive)
        {
            CompanyMemberId = companyMemberId;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            IsActive = isActive;
        }
    }
}
