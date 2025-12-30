using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}