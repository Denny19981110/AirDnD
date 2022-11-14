using Airdnd.Web.Interfaces;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Base;
using Airdnd.Web.ViewModels.Partial;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Airdnd.Web.Services
{
    public class SearchDapperService :ISearchService
    {
        private readonly IDbConnection _db;

        public SearchDapperService( IDbConnection db )
        {
            _db = db;
        }
        public IEnumerable<ProductDto> GetListingByConditions( string location, int userId, string checkIn, string checkOut, int adult, int child )
        {

            using var dbConn = _db;
            if( location.ToLower() == "any" )
            {
                location = "";
            }
            StringBuilder sqlbuilder = new StringBuilder();
            string queryLoc = @"
    SELECT
    l.ListingId AS Id
   , (SELECT Top 1
        VALUE
    FROM STRING_SPLIT(l.Address,'-')) AS County
   , (SELECT PropertyName
    FROM PropertyType AS p
    WHERE p.PropertyId = l.PropertyId) AS PropertyTitle
   , l.ListingName AS ListingName
   , l.DefaultPrice AS Price
   , l.Expected AS Expected
   , COALESCE(FORMAT(( SELECT AVG(RatingAvg) FROM Ratings AS r GROUP BY r.ListingID HAVING r.ListingID=l.ListingId ),'0.0' ), N'最新') AS Rating
   , ( SELECT CAST(
        CASE WHEN l.ListingID IN 
                (SELECT wd.ListingID
                FROM WishListDetail AS wd
                WHERE wd.WishlistID IN  
                    (SELECT w.WishlistID
                FROM WishList AS w
                WHERE w.UserAccountID = @userId))
            THEN 1
            ELSE 0
        END AS BIT)) AS IsWish
    FROM (  SELECT *
        FROM Listing as ll
        WHERE ll.Address LIKE '%'+ @loc+'%'
        OR ll.ListingName LIKE '%'+ @loc+'%') AS l
    WHERE l.Status = 1
    ;";
            string queryService = @" 
    SELECT s.ListingId, s.ServiceId
    FROM ServiceListing AS s
    WHERE s.ListingId in (  SELECT ll.ListingId
    FROM Listing as ll
    WHERE ll.Address LIKE '%'+ @loc+'%'
        OR ll.ListingName LIKE '%'+ @loc+'%');";
            string queryRoom = @" 
    SELECT
        l.ListingId
       , l.Bedroom AS Bedrooms
        , l.Bed AS Beds
        , l.Bathroom As Bathrooms
    FROM (  SELECT *
        FROM Listing as ll
        WHERE ll.Address LIKE '%'+ @loc+'%'
        OR ll.ListingName LIKE '%'+ @loc+'%') AS l;";
            string queryGeo = @" 
    SELECT
        l.ListingId 
        , l.Lat AS Lat
        , l.Lng AS Lng

    FROM (  SELECT *
        FROM Listing as ll
        WHERE ll.Address LIKE '%'+ @loc+'%'
        OR ll.ListingName LIKE '%'+ @loc+'%') AS l;";
            string queryImg = @"
    SELECT 
        i.ListingId
        ,i.ListingImagePath
    FROM ListingImage AS i
    WHERE i.ListingID in (  SELECT ll.ListingId
                            FROM Listing as ll
                            WHERE ll.Address LIKE '%'+ @loc+'%'
                            OR ll.ListingName LIKE '%'+ @loc+'%')
;";
            string queryNotAvailableDate = @"
    SELECT c.ListingId, c.CalendarDate AS NotAvailableDate
    FROM Calendar AS c
    WHERE c.ListingId   in (  SELECT ll.ListingId
                            FROM Listing as ll
                            WHERE ll.Address LIKE '%'+ @loc+'%'
                            OR ll.ListingName LIKE '%'+ @loc+'%')
    AND c.Available = 0
;";

            sqlbuilder.AppendLine(queryLoc);
            sqlbuilder.AppendLine(queryService);
            sqlbuilder.AppendLine(queryRoom);
            sqlbuilder.AppendLine(queryGeo);
            sqlbuilder.AppendLine(queryImg);
            sqlbuilder.AppendLine(queryNotAvailableDate);
            string sql = sqlbuilder.ToString();

            var conditionLoc = dbConn.QueryMultiple(sql, 
                                    new { loc = new DbString { Value =location,Length=50},
                                    userId = userId });
            List<List<dynamic>> resultLoc = new List<List<dynamic>>();
            while( !conditionLoc.IsConsumed )
            {
                resultLoc.Add(conditionLoc.Read<dynamic>().ToList());
            }
            var resultListing = resultLoc[0];
            var resultServices = resultLoc[1];
            var resultRoom = resultLoc[2];
            var resultLocation = resultLoc[3];
            var resultImgpath = resultLoc[4];
            var resultDate = resultLoc[5];

            var result = resultListing.Select(l => new ProductDto
            {
                Id = l.Id,
                County = l.County,
                PropertyTitle = l.PropertyTitle,
                ListingName = l.ListingName,
                Price = l.Price,
                Expected = l.Expected,
                Rating = l.Rating,
                IsWish = l.IsWish,
                ServiceId = resultServices.Where(s => s.ListingId == l.Id).Select(s => (int)s.ServiceId),
                Rooms = resultRoom.Where(r => r.ListingId == l.Id).Select(r => new Rooms { Bedrooms = (int)r.Bedrooms, Beds = (int)r.Beds, Bathrooms = (int)r.Bathrooms }).First(),
                Location = resultLocation.Where(r => r.ListingId == l.Id).Select(r => new Location { Lat = (double)r.Lat, Lng = (double)r.Lng }).First(),
                ImgPath = resultImgpath.Where(r => r.ListingId == l.Id).Select(r => (string)r.ListingImagePath).ToList(),
                NotAvailableDate = resultDate.Where(r => r.ListingId == l.Id)?.Select(r => (DateTime)r.NotAvailableDate)
            });

            if( checkIn != "Any" && checkOut != "Any" )
            {
                DateTime checkInDate = Convert.ToDateTime(checkIn);
                DateTime checkOutDate = Convert.ToDateTime(checkOut);
                if( result.Count() != 0 && result.Where(l => l.NotAvailableDate != null).Count() != 0 )
                    result = result.Where(l => !l.NotAvailableDate.Any(d => d >= checkInDate && d < checkOutDate)).ToList();
            }
            int guest = adult + child;
            if( guest == 1 )
            {
                return result;
            }
            else if( guest != 20 )
            {
                result = result.Where(r => r.Expected == guest).ToList();
            }
            else
            {
                result = result.Where(r => r.Expected > 20).ToList();
            }


            return result;
        }
        public int CountAllPages( IEnumerable<ProductDto> listings )
        {
            var listingCount = listings.Count();
            if( listingCount % 16 == 0 )
            {
                return listingCount / 16;
            }
            else
            {
                return (listingCount / 16) + 1;
            }
        }
        public IEnumerable<ProductDto> Take16Listings( int page, IEnumerable<ProductDto> productDto )
        {
            int skipItems = (page - 1) * 16;
            return productDto.Skip(skipItems).Take(16).ToList();
        }
    }
}
