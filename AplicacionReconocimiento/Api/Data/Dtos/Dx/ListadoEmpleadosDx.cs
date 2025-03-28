using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Dtos.Dx
{
    public class ListadoEmpleadosDx
    {
        public List<EmpleadoDtoDx> Users { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
        public string CountUsers { get; set; }
        public string BranchName { get; set; }

        public ListadoEmpleadosDx()
        {
        }   

        public ListadoEmpleadosDx(List<EmpleadoDtoDx> users, string result, string errorMessage, string countUsers, string branchName)
        {
            Users = users;
            Result = result;
            ErrorMessage = errorMessage;
            CountUsers = countUsers;
            BranchName = branchName;
        }

        public override string ToString()
        {
            return "Users: " + Users + 
                ", Result: " + Result + 
                ", ErrorMessage: " + ErrorMessage + 
                ", CountUsers: " + CountUsers + 
                ", BranchName: " + BranchName;
        }

    }
}
