using Airdnd.Web.ViewModels;
using System.Collections.Generic;
using System.Data.Common;

namespace Airdnd.Web.Interfaces
{
    public interface ISearchService
    {
        int CountAllPages(IEnumerable<ProductDto> listings);
        IEnumerable<ProductDto> GetListingByConditions(string location, int userId, string checkIn, string checkOut, int adult, int child);
        IEnumerable<ProductDto> Take16Listings(int page, IEnumerable<ProductDto> productDto);
    }
}