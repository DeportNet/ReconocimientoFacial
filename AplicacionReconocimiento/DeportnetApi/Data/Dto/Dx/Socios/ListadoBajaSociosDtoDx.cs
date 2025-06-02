namespace DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios
{
    
    public class ListadoBajaSociosDtoDx
    {
        public string ProcessResult { get; set; }
        public string? ErrorMessage { get; set; }
        public int CountDeletedMembers { get; set; }
        public string[] DeletedBranchMembers { get; set; }


        public ListadoBajaSociosDtoDx()
        {

        }

        public ListadoBajaSociosDtoDx(string processResult, string? errorMessage, int countDeletedMembers, string[] deletedBranchMembers)
        {
            ProcessResult = processResult;
            ErrorMessage = errorMessage;
            CountDeletedMembers = countDeletedMembers;
            DeletedBranchMembers = deletedBranchMembers;
        }




    }
}
