using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using DeportNetReconocimiento.DeportnetApi.Data.Dto.Dx.Socios;


namespace DeportNetReconocimiento.Api.Data.Mapper
{
    public class SocioMapper : ISocioMapper
    {
        public Socio SocioDtoDxToSocio(SocioDtoDx socioDto)
        {
            return new Socio
            {
                IdDx = socioDto.Id,
                Email = socioDto.Email,
                FirstName = socioDto.FirstName,
                LastName = socioDto.LastName,
                BirthDate = socioDto.BirthDate,
                Cellphone = socioDto.Cellphone,
                IsActive = socioDto.IsActive,
                CardNumber = socioDto.CardNumber,
                ImageUrl = socioDto.ImageUrl,
                Gender = socioDto.Gender,
                IsValid = socioDto.IsValid,
                IsSincronizado = "T",
                FechaHoraSincronizado = DateTime.Now,
            };
        }

        public List<Socio> ListaSocioDtoDxToListaSocio(List<SocioDtoDx> sociosDto)
        {
            return sociosDto.Select(SocioDtoDxToSocio).ToList();
        }

        public NuevoSocio SocioToNuevoSocio(Socio socio)
        {
            return new NuevoSocio
            {
                Id = socio.Id,
                BirthDate = socio.BirthDate, //YYYY-MM-DD
                CardNumber = socio.CardNumber,
                Email = socio.Email,
                FirstName = socio.FirstName,
                LastName = socio.LastName,
                Gender = socio.Gender
            };
        }

        public List<NuevoSocio> ListaSocioToListaNuevoSocio(List<Socio> listadoSocios)
        {
            return listadoSocios.Select(SocioToNuevoSocio).ToList();
        }

    }
}
