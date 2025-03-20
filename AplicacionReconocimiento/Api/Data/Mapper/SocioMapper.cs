using DeportNetReconocimiento.Api.Data.Domain;
using DeportNetReconocimiento.Api.Data.Dtos.Dx;
using DeportNetReconocimiento.Api.Data.Mapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.Api.Data.Mapper
{
    public class SocioMapper : ISocioMapper
    {
        public Socio MapearDtoASocio(SocioDtoDx socioDto)
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
                IsValid = socioDto.IsValid
            };
        }

        public List<Socio> MapearListaDtoASocio(List<SocioDtoDx> sociosDto)
        {
            return sociosDto.Select(MapearDtoASocio).ToList();
        }
    }
}
