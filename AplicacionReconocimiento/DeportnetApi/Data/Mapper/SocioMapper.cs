using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx.Socios;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;

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
    }
}
