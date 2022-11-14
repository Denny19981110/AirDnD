using Airdnd.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airdnd.Core.Interfaces
{
    public interface IWishlistQuery
    {
        IEnumerable<WishList> GetWishListByUserId( int userId );
        IEnumerable<WishListDetail> GetWishDetailByUserlistIds(  IEnumerable<int> wishlistIds );
        Task<IEnumerable<WishList>> GetWishListByUserIdAsync( int userId );
    }
}
