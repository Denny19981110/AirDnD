using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airdnd.Infrastructure.Data.Queries
{
    public class WishlistQueryService :IWishlistQuery
    {
        private readonly AirBnBContext _context;

        public WishlistQueryService( AirBnBContext context )
        {
            _context = context;
        }

        public IEnumerable<WishListDetail> GetWishDetailByUserlistIds( IEnumerable<int> wishlistIds )
        {
            var wdLists = _context.WishListDetails.Where(wd=> wishlistIds.Contains(wd.WishlistId)).AsEnumerable();
            return wdLists;
        }

        public IEnumerable<WishList> GetWishListByUserId( int userId )
        {
            var lists = _context.WishLists.Where(w => w.UserAccountId == userId).AsNoTracking().AsEnumerable();
            return lists;
        }

        public async Task<IEnumerable<WishList>> GetWishListByUserIdAsync( int userId )
        {
            var lists = await _context.WishLists.Where(w => w.UserAccountId == userId).AsNoTracking().ToListAsync();
            return lists;
        }


       
    }
}
