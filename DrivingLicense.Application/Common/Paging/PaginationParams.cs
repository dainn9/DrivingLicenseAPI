using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.Common.Paging
{
    public class PaginationParams
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        public void Normalize()
        {
            if (PageNumber < 1) PageNumber = 1;
            if (PageSize < 1 || PageSize > 100) PageSize = 10;
        }
    }


}