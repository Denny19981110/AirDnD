using Airdnd.Web.ViewModels;
using System.Collections.Generic;

namespace Airdnd.Web.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> Get16Listings( int? loginUserId, int page );
        IEnumerable<ProductDto> GetAllListings( int? loginUserId);
        IEnumerable<HomePropertyDto> GetHomeProperties();
    }
}