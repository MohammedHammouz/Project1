using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMBusiness.Mappers
{
    public class GenericMapper<DTO,Entity>
    {
        private readonly Func<Entity, DTO> _toDTO;
        private readonly Func<DTO, Entity> _toEntity;
        public enum enMode { Add, Update }
        public GenericMapper(
            Func<Entity, DTO> toDTO,
            Func<DTO, Entity> toEntity)
        {
            _toDTO = toDTO;
            _toEntity = toEntity;
        }
        public DTO ToDTO(Entity entity,enMode mode=enMode.Add)
        {
            return _toDTO(entity);
        }
        public Entity ToEntity(DTO dto, enMode mode = enMode.Add)
        {
            return _toEntity(dto);
        }
    }
}
