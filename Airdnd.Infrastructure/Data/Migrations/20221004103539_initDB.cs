using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airdnd.Infrastructure.Data.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Legal",
                columns: table => new
                {
                    LegalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legal", x => x.LegalID);
                });

            migrationBuilder.CreateTable(
                name: "PrivacyType",
                columns: table => new
                {
                    PrivacyTypeID = table.Column<int>(type: "int", nullable: false, comment: "整套、獨立、合住")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrivacyTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrivacyTypeContent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacyType", x => x.PrivacyTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyGroup",
                columns: table => new
                {
                    PropertyGroupID = table.Column<int>(type: "int", nullable: false, comment: "公寓、獨棟房屋...等")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyGroupName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyGroup", x => x.PropertyGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Recommend",
                columns: table => new
                {
                    RecommendID = table.Column<int>(type: "int", nullable: false, comment: "房屋特色(商品頁三欄)")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecommendName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecommendContent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommend", x => x.RecommendID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    ServiceTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.ServiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "頭貼"),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "性別"),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: ""),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "填寫地址"),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lng = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.UserAccountID);
                });

            migrationBuilder.CreateTable(
                name: "PropertyType",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false, comment: "穀倉、船、城堡")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PropertyContent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PropertyGroupID = table.Column<int>(type: "int", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Description_Room Type",
                        column: x => x.PropertyGroupID,
                        principalTable: "PropertyGroup",
                        principalColumn: "PropertyGroupID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false, comment: "特殊服務與設施")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServiceTypeID = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_Service_ServiceType",
                        column: x => x.ServiceTypeID,
                        principalTable: "ServiceType",
                        principalColumn: "ServiceTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    WishlistID = table.Column<int>(type: "int", nullable: false, comment: "心願單")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountID = table.Column<int>(type: "int", nullable: false),
                    WishGroupName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.WishlistID);
                    table.ForeignKey(
                        name: "FK_Wish List_User Acount",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Listing",
                columns: table => new
                {
                    ListingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "房價"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: ""),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    ListingName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "房源名"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "撰寫房源描述"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Expected = table.Column<int>(type: "int", nullable: false, comment: "人數"),
                    UserAcountID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "狀態: 0編輯中，1上架、2 下架"),
                    Bed = table.Column<int>(type: "int", nullable: false, comment: "床位"),
                    BedRoom = table.Column<int>(type: "int", nullable: false, comment: "臥室"),
                    BathRoom = table.Column<int>(type: "int", nullable: false, comment: "衛浴"),
                    Toilet = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "建立時間"),
                    EditTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "編輯時間"),
                    IndieBathroom = table.Column<bool>(type: "bit", nullable: false, comment: "有任何客房專用浴室?"),
                    HighlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.ListingID);
                    table.ForeignKey(
                        name: "FK_Listing_PropertyType",
                        column: x => x.PropertyID,
                        principalTable: "PropertyType",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Room_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "PrivacyType",
                        principalColumn: "PrivacyTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Room_User Acount",
                        column: x => x.UserAcountID,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Highlight",
                columns: table => new
                {
                    HighlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HighlightName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(N'')"),
                    ListingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highlight", x => x.HighlightId);
                    table.ForeignKey(
                        name: "FK_Highlight_Listing_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LegalListing",
                columns: table => new
                {
                    LLegalID = table.Column<int>(type: "int", nullable: false, comment: "監視器、武器")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalID = table.Column<int>(type: "int", nullable: false),
                    ListingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Special Detail", x => x.LLegalID);
                    table.ForeignKey(
                        name: "FK_Special Detail_Room",
                        column: x => x.ListingID,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Special Detail_Special",
                        column: x => x.LegalID,
                        principalTable: "Legal",
                        principalColumn: "LegalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListingImage",
                columns: table => new
                {
                    ListingImageID = table.Column<int>(type: "int", nullable: false, comment: "房源圖庫")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingImage", x => x.ListingImageID);
                    table.ForeignKey(
                        name: "FK_Image_Room",
                        column: x => x.ListingID,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    ListingID = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "入住日"),
                    StayNight = table.Column<int>(type: "int", nullable: false, comment: "住幾晚"),
                    PaymentType = table.Column<int>(type: "int", nullable: false, comment: "付款方式"),
                    Adults = table.Column<int>(type: "int", nullable: false, comment: "(13+歲)人數"),
                    Children = table.Column<int>(type: "int", nullable: false, comment: "3~13歲"),
                    Infants = table.Column<int>(type: "int", nullable: false, comment: "0~2歲"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "當下原價"),
                    FinishDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "完成訂單日期"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "enum 訂單狀態"),
                    TranStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Room",
                        column: x => x.ListingID,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User Acount",
                        column: x => x.CustomerID,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Promo",
                columns: table => new
                {
                    PromoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<decimal>(type: "decimal(18,0)", nullable: false, comment: "整數 => 整數%off"),
                    PromoName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ListingID = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EditTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promo", x => x.PromoID);
                    table.ForeignKey(
                        name: "FK_Promo_Listing",
                        column: x => x.ListingID,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    RatingAvg = table.Column<double>(type: "float", nullable: true, comment: "平均"),
                    Clean = table.Column<int>(type: "int", nullable: true, comment: "乾淨度"),
                    Precise = table.Column<int>(type: "int", nullable: true, comment: "準確性"),
                    Communication = table.Column<int>(type: "int", nullable: true, comment: "溝通"),
                    Location = table.Column<int>(type: "int", nullable: true, comment: "位置"),
                    CheckIn = table.Column<int>(type: "int", nullable: true, comment: "入住"),
                    CostPrice = table.Column<int>(type: "int", nullable: true, comment: "性價比"),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Ratings_Listing",
                        column: x => x.ListingID,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_UserAccount",
                        column: x => x.UserID,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecommendListing",
                columns: table => new
                {
                    LRecommendID = table.Column<int>(type: "int", nullable: false),
                    RecommendID = table.Column<int>(type: "int", nullable: false),
                    ListingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendListing", x => x.LRecommendID);
                    table.ForeignKey(
                        name: "FK_Recommend Detail_Recommend",
                        column: x => x.RecommendID,
                        principalTable: "Recommend",
                        principalColumn: "RecommendID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recommend Detail_Room",
                        column: x => x.ListingID,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceListing",
                columns: table => new
                {
                    LServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListingId = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service&Facility Detail", x => x.LServiceID);
                    table.ForeignKey(
                        name: "FK_Service&Facility Detail_Room",
                        column: x => x.ListingId,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Service&Facility Detail_Service&Facility",
                        column: x => x.ServiceID,
                        principalTable: "Service",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishListDetail",
                columns: table => new
                {
                    WishlistDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistID = table.Column<int>(type: "int", nullable: false),
                    ListingID = table.Column<int>(type: "int", nullable: false),
                    CreatTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListDetail", x => x.WishlistDetailID);
                    table.ForeignKey(
                        name: "FK_WishList Detail_Room",
                        column: x => x.ListingID,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishList Detail_Wish List1",
                        column: x => x.WishlistID,
                        principalTable: "WishList",
                        principalColumn: "WishlistID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    CalendarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalendarDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ListingID = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false, comment: "是否上架"),
                    OrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.CalendarID);
                    table.ForeignKey(
                        name: "FK_Calendar_Listing",
                        column: x => x.ListingID,
                        principalTable: "Listing",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calendar_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false, comment: "星等"),
                    CreatTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Clean = table.Column<int>(type: "int", nullable: true, comment: "乾淨度"),
                    Precise = table.Column<int>(type: "int", nullable: true, comment: "準確性"),
                    Communication = table.Column<int>(type: "int", nullable: true, comment: "溝通"),
                    Location = table.Column<int>(type: "int", nullable: true, comment: "位置"),
                    CheckIn = table.Column<int>(type: "int", nullable: true, comment: "入住"),
                    CostPrice = table.Column<int>(type: "int", nullable: true, comment: "性價比"),
                    HostID = table.Column<int>(type: "int", nullable: true, comment: "房東ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_User Acount",
                        column: x => x.HostID,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDate",
                columns: table => new
                {
                    OrderDateID = table.Column<int>(type: "int", nullable: false, comment: "列出order的每一天")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    OrderedDate = table.Column<DateTime>(type: "datetime", nullable: false, comment: "透過程式把入住日跟天數算出每一天"),
                    Available = table.Column<bool>(type: "bit", nullable: false, comment: "一定是False")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDate", x => x.OrderDateID);
                    table.ForeignKey(
                        name: "FK_OrderDate_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Highlight",
                columns: new[] { "HighlightId", "HighlightName", "IconPath", "ListingId", "Sort" },
                values: new object[,]
                {
                    { 1, "搶手", "paid", null, 1 },
                    { 2, "鄉村", "emoji_nature", null, 2 },
                    { 3, "擁抱大自然", "nature_people", null, 3 },
                    { 4, "令人難忘", "sentiment_very_satisfied", null, 4 },
                    { 5, "浪漫", "volunteer_activism", null, 5 },
                    { 6, "歷史悠久", "public", null, 6 }
                });

            migrationBuilder.InsertData(
                table: "Legal",
                columns: new[] { "LegalID", "LegalName", "Sort" },
                values: new object[,]
                {
                    { 1, "監視錄影器", 1 },
                    { 2, "武器", 2 },
                    { 3, "危險動物", 3 }
                });

            migrationBuilder.InsertData(
                table: "PrivacyType",
                columns: new[] { "PrivacyTypeID", "PrivacyTypeContent", "PrivacyTypeName" },
                values: new object[,]
                {
                    { 1, "房源或飯店內的獨立客房，以及部分共用空間", "獨立房間" },
                    { 2, "可能要和他人共用的就寢空間和公共區域", "合住房間" },
                    { 3, "獨享整間房源", "整套房源" }
                });

            migrationBuilder.InsertData(
                table: "PropertyGroup",
                columns: new[] { "PropertyGroupID", "IconPath", "ImagePath", "PropertyGroupName" },
                values: new object[,]
                {
                    { 6, "https://i.imgur.com/fyYyfUz.png", "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031229/%E7%B2%BE%E5%93%81%E6%97%85%E5%BA%97_pw4nwd.jpg", "精品旅店" },
                    { 5, "https://i.imgur.com/fyYyfUz.png", "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031228/%E5%AE%B6%E5%BA%AD%E5%BC%8F%E6%97%85%E9%A4%A8_yagqxz.jpg", "家庭式旅館" },
                    { 4, "https://i.imgur.com/sRMPNMt.png", "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031228/%E7%8D%A8%E7%89%B9%E6%88%BF%E6%BA%90_uplvab.jpg", "獨特房源" },
                    { 2, "https://i.imgur.com/sRMPNMt.png", "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031229/%E7%8D%A8%E6%A3%9F%E6%88%BF%E5%B1%8B_ed2p1c.jpg", "獨棟房屋" },
                    { 1, "https://i.imgur.com/R6F4VFX.png", "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031229/%E5%85%AC%E5%AF%93_ybdrzs.jpg", "公寓" },
                    { 3, "https://i.imgur.com/jSKRfb1.png", "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031228/%E9%99%84%E5%B1%AC%E5%BB%BA%E7%AF%89_al5ksd.jpg", "附屬建築" }
                });

            migrationBuilder.InsertData(
                table: "ServiceType",
                columns: new[] { "ServiceTypeID", "ServiceTypeName" },
                values: new object[,]
                {
                    { 1, "房源是否設有任何獨特的設備與服務？" },
                    { 2, "這些最受旅客歡迎的設備與服務呢？" },
                    { 3, "是否備有以下保安設備？" }
                });

            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "UserAccountID", "AboutMe", "Address", "AvatarUrl", "Birthday", "CreateDate", "Email", "Gender", "Lat", "Lng", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { 13, "你蒐集了地圖上每一次的風和日麗，你擁抱熱情的島嶼", "台東縣台東市中華路一段721號", "~/assert/exPerson/exPerson13.webp", new DateTime(1981, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "kvh44320@nezid.com", 0, 22.747766800000001, 121.1465661, "珮君", "ymtVePGE", "0930692667" },
                    { 21, "日記上寫滿了夢想 我決定要用這一生背誦", "臺中市北區育才街2號", "~/assert/exPerson/exPerson21.webp", new DateTime(1976, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "slq39844@cdfaq.com", 0, 24.150251999999998, 120.6864899, "Ching Ning", "wxjy7v", "0928862518" },
                    { 20, "一二三 牽著手，四五六，抬起頭，七八九 我們私奔到月球", "臺中市西區自由路一段95號", "~/assert/exPerson/exPerson20.webp", new DateTime(1982, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "nmt67730@nezid.com", 1, 24.135846000000001, 120.67759599999999, "Snow", "e75fv5", "0986233420" },
                    { 19, "這裡的生活步調沒有大都市的緊湊，放鬆慢慢的渡過生活的的每一刻，不錯過每一個正在發生的感動。", "台南市中西區大埔街97號", "~/assert/exPerson/exPerson19.webp", new DateTime(1985, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "nmt67730@nezid.com", 0, 22.987255999999999, 120.206568, "柏綸", "d2m4un", "0913332005" },
                    { 18, "Photographer/Video editor", "台南市東區民族路一段1號", "~/assert/exPerson/exPerson18.webp", new DateTime(1991, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "sms61743@cdfaq.com", 0, 22.994036000000001, 120.216347, "柏澔", "bg7g6r", null },
                    { 17, "住在臺灣", "高雄市三民區建東里建國三路52號", "~/assert/exPerson/exPerson17.webp", new DateTime(1978, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0, 22.638100000000001, 120.2979, "劉瓊芬", "nfndqa", "0936871248" },
                    { 16, "如果需要行程推薦景點介紹的話歡迎隨時與我聯繫", "高雄市前金區五福三路122號", "~/assert/exPerson/exPerson16.webp", new DateTime(1980, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "lxx97166@nezid.com", 1, 22.621200000000002, 120.2928, "Fan", "xgapm6", "0938792665" },
                    { 15, "台灣宜蘭是我的家鄉，我愛這個地方，想要把這裡美麗的風景，熱情的人們介紹給各位朋友，我就是這樣熱血的人!", "宜蘭縣宜蘭市復興路三段8號", "~/assert/exPerson/exPerson15.webp", new DateTime(1979, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1, 24.750806000000001, 121.741944, "Yi Yi", "ci5ahz", "0921619108" },
                    { 14, "當您進入花蓮，有如發現了人間桃花源，柳暗花明的喜悅讓人忘記崎嶇山路的考驗", "花蓮縣花蓮市民權路42號", "~/assert/exPerson/exPerson14.webp", new DateTime(1990, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "jeh68365@xcoxc.com", 0, 23.986314, 121.625817, "Fish", "FgbYnmDX", null },
                    { 12, "哈囉!各位朋友大家好，歡迎來蘭嶼鄉度假、放鬆～", "蘭嶼鄉", "~/assert/exPerson/exPerson12.webp", new DateTime(1984, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "sob27752@xcoxc.com", 0, 22.044716000000001, 121.547571, "Emily Wang", "qhK2Rbvs", "0938800707" },
                    { 1, "我們為了這個房子花了很多心思設計與設置許多舒適的傢俱，希望來入住的你，能好好享用並愛護。", "台北市大安區信義路三段143號", "~/assert/exPerson/exPerson01.webp", new DateTime(1996, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "willy19036@gmail.com", 1, 25.033674000000001, 121.540396, "黃崇瑋", "c598ec651b34ac117bea07da8ddf18776f826bee3766a0a913d4d74a9af2eb17", "0952951560" },
                    { 10, "花若盛開 蝴蝶自來 人若精彩 天自安排", "台北市中正區南海路56號", "~/assert/exPerson/exPerson10.webp", new DateTime(1982, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "dyk31316@cdfaq.com", 1, 25.031210000000002, 25.031210000000002, "Nelson Kuang Yao", "m6vzhs69", "0928110881" },
                    { 9, "享受簡單自然的私生活", "台北市中山區長安東路二段141號", "~/assert/exPerson/exPerson09.webp", new DateTime(1988, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "lqd03913@nezid.com", 1, 25.048532000000002, 121.537161, "Yao-Sheng", "hek4p5af", "0929064077" },
                    { 8, "我跟老公長期住在紐約,偶爾來台灣玩~~第一次用AirDnD 新手請大家多多指教 :)", "台北市中正區重慶南路一段165號", "~/assert/exPerson/exPerson08.webp", new DateTime(1975, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "pmi32851@nezid.com", 1, 25.038606000000001, 121.513015, "翔宇", "VQ2zfxu2", null },
                    { 7, "背包客浪人", "桃園市桃園區成功路三段38號", "~/assert/exPerson/exPerson07.webp", new DateTime(1985, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1, 24.999460500000001, 121.3272106, "Satine", "Zfz5y5ew", "0988016073" },
                    { 6, "在這兒能感覺平和 .心靈沉靜,經過一夜的好眠 希望能為您充飽電再出發~", "桃園市中壢區三光路115號", "~/assert/exPerson/exPerson06.webp", new DateTime(1993, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "tommylin001@gmail.com", 1, 24.962071999999999, 121.211168, "林峪廷", "c598ec651b34ac117bea07da8ddf18776f826bee3766a0a913d4d74a9af2eb17", "0933271403" },
                    { 5, "如果你只想要獨自靜靜的體會大自然帶來的安寧與清新,我們這裡也絕對會是你最愛的選擇!: )", "台北市文山區木新路三段312號", "~/assert/exPerson/exPerson05.webp", new DateTime(1993, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "k01midy@gmail.com", 0, 24.98068, 121.55622, "KamiLiu", "c598ec651b34ac117bea07da8ddf18776f826bee3766a0a913d4d74a9af2eb17", null },
                    { 4, "我和您一樣熱愛旅遊, 喜歡簡單的生活, 改造房子也是我的興趣, 灰和白 是我最愛的顏色 ,所以我的房源用了大量的灰階色, 中性色的灰並不沉悶, 反而耐人尋味.....", "台北市中正區濟南路一段71號", "~/assert/exPerson/exPerson04.webp", new DateTime(1995, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "linchin207@gmail.com", 1, 25.04233, 121.52368, "林勁宇", "c598ec651b34ac117bea07da8ddf18776f826bee3766a0a913d4d74a9af2eb17", "0932698842" },
                    { 3, "您好，乾淨整齊的住宿空間，期待您的到來！", "台北市信義區基隆路一段156號", "~/assert/exPerson/exPerson03.webp", new DateTime(1994, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "anguswanghi@gmail.com", 1, 25.043659999999999, 121.56577, "王浩宇", "c598ec651b34ac117bea07da8ddf18776f826bee3766a0a913d4d74a9af2eb17", "0933182929" },
                    { 2, "同樣喜愛旅遊,將自身旅遊面臨的問題,當作客人的所需,提供準確有效的服務,更喜愛與遊客交流｡", "台北市信義區基隆路一段156號", "~/assert/exPerson/exPerson02.webp", new DateTime(1994, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "pomeleaf1124@gmail.com", 1, 24.987130000000001, 121.58588, "李嘉威", "c598ec651b34ac117bea07da8ddf18776f826bee3766a0a913d4d74a9af2eb17", "0968637923" },
                    { 22, "", "", "", null, new DateTime(2022, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0, null, null, "FakeAccount", "", "" }
                });

            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "UserAccountID", "AboutMe", "Address", "AvatarUrl", "Birthday", "CreateDate", "Email", "Gender", "Lat", "Lng", "Name", "Password", "Phone" },
                values: new object[] { 11, "生活不止眼前的苟且，還有詩和遠方的田野", "新竹市東區中華路二段2號", "~/assert/exPerson/exPerson11.webp", new DateTime(1995, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "住在臺灣", 1, 24.8094158, 120.9834549, "Ling Chih", "8k8sec64", null });

            migrationBuilder.InsertData(
                table: "UserAccount",
                columns: new[] { "UserAccountID", "AboutMe", "Address", "AvatarUrl", "Birthday", "CreateDate", "Email", "Gender", "Lat", "Lng", "Name", "Password", "Phone" },
                values: new object[] { 23, "", "", "", null, new DateTime(2022, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1, null, null, "DannWu", "", "" });

            migrationBuilder.InsertData(
                table: "PropertyType",
                columns: new[] { "PropertyID", "IconPath", "PropertyContent", "PropertyGroupID", "PropertyName" },
                values: new object[,]
                {
                    { 1, "。", "集合住宅大樓或複合式建築中的出租屋。", 1, "出租住所" },
                    { 58, "https://a0.muscache.com/pictures/5cdb8451-8f75-4c5f-a17d-33ee228e3db8.jpg", "在用來風力發電的建築中，可供居住的起居空間。", 4, "風車" },
                    { 57, "", "由守車、篷車和其他有軌車輛改造而成的居住空間。", 4, "火車" },
                    { 56, "https://a0.muscache.com/pictures/d721318f-4752-417d-b4fa-77da3cbc3269.jpg", "層數很多且可欣賞風景的獨立式結構。", 4, "塔樓" },
                    { 55, "", "以桿子之稱的錐形帳篷，帶掀開式的門與開放式的頂部。", 4, "印地安帳篷" },
                    { 54, "", "面積通常不到 11 坪的獨立房屋。", 4, "米你屋" },
                    { 53, "", "以布料和桿子搭建而成的結構，通常可折疊移動。", 4, "帳篷" },
                    { 51, "https://a0.muscache.com/pictures/747b326c-cb8f-41cf-a7f9-809ab646e10c.jpg", "最初用於牧羊的小型四輪貨車。", 4, "牧人小屋" },
                    { 59, "https://a0.muscache.com/pictures/4759a0a7-96a8-4dcd-9490-ed785af6df14.jpg", "。", 4, "蒙古包" },
                    { 50, "", "由教堂或清真寺等禮拜場所改造而成的空間。", 4, "宗教建築" },
                    { 48, "", "由飛機改造而成的住宿。", 4, "飛機" },
                    { 47, "", "濱水的塔樓，配有強光可以指引船舶。", 4, "燈塔" },
                    { 46, "https://a0.muscache.com/pictures/8e507f16-4943-4be9-b707-59bd38d56309.jpg", "一片四面環水的土地。", 4, "島嶼" },
                    { 45, "https://a0.muscache.com/pictures/89faf9ae-bbbc-4bc4-aecd-cc15bf36cbca.jpg", "以冰雪建成的圓頂建築，常見於寒冷地區。", 4, "冰製圓頂屋" },
                    { 44, "", "以木頭或泥土建成的房屋，屋頂可能是用茅草製成。", 4, "小屋" },
                    { 43, "https://a0.muscache.com/pictures/c027ff1a-b89c-4331-ae04-f8dee1cdc287.jpg", "水上的房屋，建造時是用陸上房屋相似的建材。", 4, "船屋" },
                    { 42, "https://a0.muscache.com/pictures/aaa02c2d-9f0d-4c41-878a-68c12ec6c6bd.jpg", "位於鄉村地區的房屋，旅客可以體驗農村生活或與動物共度時光。", 4, "農場住宿" },
                    { 49, "", "位於大片畜牧土地上的住宿。", 4, "牧場" },
                    { 41, "https://a0.muscache.com/pictures/d7445031-62c4-46d0-91c3-4f29f9790f7a.jpg", "建在地底下或以夯土等材料建成的房屋。", 4, "生態屋" },
                    { 60, "", "設有露天庭院或花園的摩洛哥傳統房屋。", 4, "Riad" },
                    { 62, "", "裝潢齊全的出租房源，包括廚房和浴室，且可會提供前台等客房服務。", 4, "度假屋" },
                    { 78, "", "具有當地特色和完善設施的中國住宿。", 6, "客棧" },
                    { 77, "", "由歷史建築改造而成的印度住宿。", 6, "文化遺產旅店" },
                    { 76, "", "由專業管理公司提供飯店式管理服務公寓。", 6, "服務式公寓" },
                    { 75, "", "提供飯店式服務與房間的公寓式住宿。", 6, "公寓式旅店" },
                    { 74, "", "具有獨特風格或裝潢主題的專業管理民宿。", 6, "精品旅店" },
                    { 73, "", "靠近森林、山間等自然環境的專業管理住宿。", 6, "自然小屋" },
                    { 72, "", "裝潢齊全的出租房源，包括廚房和浴室，且可會提供前台等客房服務。", 6, "渡假村" },
                    { 61, "", "帶有燒烤和公共空間的韓國鄉村房屋。", 4, "韓國膳宿公寓" },
                    { 71, "", "出租和住房間床位和獨立房間的專業管理住宿。", 6, "青年旅舍" },
                    { 69, "", "為房客提供獨特文化體驗的日本小旅館。", 5, "日式旅館" },
                    { 68, "https://a0.muscache.com/pictures/251c0635-cc91-4ef7-bb13-1084d5229446.jpg", "帶有燒烤和公共空間的韓國鄉村房屋。", 5, "古巴式家庭旅館" },
                    { 67, "", "位房客提供獨立房間的台灣專業管理住宿。", 5, "台灣民宿" },
                    { 66, "", "位於鄉村地區的房屋，旅客可以體驗農村生活或與動物共度時光。", 5, "農場住宿" },
                    { 65, "", "靠近森林、山間等自然環境的專業管理住宿。", 5, "自然小屋" },
                    { 64, "", "為房客提供早餐，且有房東在現場專業管理民宿。", 5, "家庭式旅館" },
                    { 63, "", "", 4, "其他" },
                    { 70, "", "位房客提供獨立房間、套房或獨特房源的專業管理住宿。", 6, "飯店" },
                    { 40, "", "圓頂或球形房屋，例如氣泡屋。", 4, "圓頂屋" },
                    { 52, "https://a0.muscache.com/pictures/0ff9740e-52a2-4cd5-ae5a-94e1bfb560d6.jpg", "由貨運鋼製貨櫃改造而成的房源。", 4, "貨櫃屋" },
                    { 38, "https://a0.muscache.com/pictures/1b6a8b70-a3b6-48b5-88e1-2243d9172c06.jpg", "可能具有歷史意義的宏偉建築，也許設有塔樓和護城河。", 4, "城堡" },
                    { 17, "", "圓頂或球形房屋，例如氣泡屋。", 2, "圓頂屋" }
                });

            migrationBuilder.InsertData(
                table: "PropertyType",
                columns: new[] { "PropertyID", "IconPath", "PropertyContent", "PropertyGroupID", "PropertyName" },
                values: new object[,]
                {
                    { 16, "", "位於鄉村地區的房屋，旅客可以體驗農村生活或與動物共度時光。", 2, "農場住宿" },
                    { 15, "", "以木頭或泥土建成的房屋，屋頂可能是用茅草製成。", 2, "小屋" },
                    { 14, "https://a0.muscache.com/pictures/c027ff1a-b89c-4331-ae04-f8dee1cdc287.jpg", "水上的房屋，建造時是用陸上房屋相似的建材。", 2, "船屋" },
                    { 13, "", "建在地底下或以夯土等材料建成的房屋。", 2, "生態屋" },
                    { 12, "", "具有寬敞的前門廊和傾斜屋頂的單層住宅。", 2, "平房" },
                    { 11, "https://a0.muscache.com/pictures/6ad4bd95-f086-437d-97e3-14d12155ddfe.jpg", "建於鄉間或湖水或海灘附近的溫馨房屋", 2, "村舍" },
                    { 18, "https://a0.muscache.com/pictures/e4b12c1b-409b-4cb6-a674-7c1284449f6e.jpg", "位於希臘基克拉澤斯群島的白色坪頂房屋。", 2, "基克拉澤斯屋" },
                    { 10, "", "多樓層的連棟房屋或設有露台的房屋，彼此之間有共用牆壁，戶外空間也可能要共享。", 2, "連棟房屋" },
                    { 39, "https://a0.muscache.com/pictures/4221e293-4770-4ea8-a4fa-9972158d4004.jpg", "位於山坡或懸崖上，自然生成或人為開挖出來的住所。", 4, "洞穴" },
                    { 7, "", "獨棟房屋或與其他建築共用牆壁的房屋。", 2, "房源" },
                    { 6, "", "裝潢齊全的出租房源，包括廚房和浴室，且可會提供前台等客房服務。", 1, "度假屋" },
                    { 5, "https://a0.muscache.com/pictures/251c0635-cc91-4ef7-bb13-1084d5229446.jpg", "古巴式家庭旅館內的獨立房間。", 1, "古巴式家庭旅館" },
                    { 4, "", "由專業管理公司提供飯店管理服務的公寓。", 1, "服務式公寓" },
                    { 3, "", "開放式的公寓，內牆可能較矮。", 1, "Loft空間" },
                    { 2, "", "位於集合住宅大樓或複合式建築的住處。", 1, "私有公寓" },
                    { 9, "", "豪華住宅，可能具有室內外空間、花園和泳池。", 2, "別墅" },
                    { 19, "https://a0.muscache.com/pictures/732edad8-3ae0-49a8-a451-29a8010dcc0c.jpg", "帶斜屋頂的木屋，常常作為滑雪或夏日度假的住宿。", 2, "度假小木屋" },
                    { 8, "https://a0.muscache.com/pictures/732edad8-3ae0-49a8-a451-29a8010dcc0c.jpg", "以木材等天然建材蓋成的房屋，且位於大自然中。", 2, "小木屋" },
                    { 21, "", "濱水的塔樓，配有強光可以指引船舶。", 2, "燈塔" },
                    { 20, "https://a0.muscache.com/pictures/c9157d0a-98fe-4516-af81-44022118fbc7.jpg", "位於義大利潘泰萊里亞島的圓頂石造屋。", 2, "義式傳統石屋" },
                    { 37, "https://a0.muscache.com/pictures/ca25c7f3-0d1f-432b-9efa-b9f5dc6d8770.jpg", "可供房客搭設帳篷、蒙古包、露營車或迷你屋的場地。", 4, "營地" },
                    { 36, "https://a0.muscache.com/pictures/4d4a4eba-c7e4-43eb-9ce2-95e1d200d10e.jpg", "建於樹幹之中、樹枝上，或位於樹林中蓋在樹根上的住所。", 4, "樹屋" },
                    { 35, "https://a0.muscache.com/pictures/31c1d523-cc46-45b3-957a-da76c30c85f9.jpg", "車上住宿空間或露營拖車，是住家兼交通工具。", 4, "露營車/休旅車" },
                    { 33, "https://a0.muscache.com/pictures/687a8682-68b3-4f21-8d71-3c3aef6c1110.jpg", "在房客入住其靠岸停泊的船隻、帆船或遊艇，但不是船屋。", 4, "船" },
                    { 32, "https://a0.muscache.com/pictures/f60700bc-8ab5-424c-912b-6ef17abc479a.jpg", "用作存放穀物、農作器具或四養生醋的改造住宅空間。", 4, "穀倉" },
                    { 31, "", "裝潢齊全的出租房源，包括廚房和浴室，且可能會提供前台等客房服務。", 3, "度假屋" },
                    { 30, "", "位於鄉村地區的房屋，旅客可以體驗農村生活或與動物共度時光。", 3, "農場住宿" },
                    { 34, "", "經過改造的大型客車，內部裝潢充滿創意巧思。", 4, "巴士" },
                    { 28, "", "與屋主分開的獨立建築。", 3, "客用住房" },
                    { 27, "", "裝潢齊全的出租房源，包括廚房和浴室，且可能會提供前台等客房服務。", 2, "度假屋" },
                    { 26, "", "帶有燒烤和公共空間的韓國鄉村房屋。", 2, "韓國膳宿公寓" },
                    { 25, "https://a0.muscache.com/pictures/251c0635-cc91-4ef7-bb13-1084d5229446.jpg", "帶有燒烤和公共空間的韓國鄉村房屋。", 2, "古巴式家庭旅館" },
                    { 24, "https://a0.muscache.com/pictures/33848f9e-8dd6-4777-b905-ed38342bacb9.jpg", "擁有錐形屋頂的圓柱狀石造屋，源自義大利。", 2, "普利亞斗笠屋" },
                    { 23, "https://a0.muscache.com/pictures/35919456-df89-4024-ad50-5fcb7a472df9.jpg", "面積通常不到 11 坪的獨立房屋。", 2, "迷你屋" },
                    { 29, "", "擁有獨立入口的房源，位於較大建築物或與之相聯。", 3, "客用套房" },
                    { 22, "", "最初用於牧羊的小型四輪貨車。", 2, "牧人小屋" }
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "ServiceID", "IconPath", "ServiceName", "ServiceTypeID", "Sort" },
                values: new object[,]
                {
                    { 14, "local_parking", "室內免費停車", 2, 5 },
                    { 15, "garage", "室內收費停車", 2, 6 },
                    { 16, "air", "空調設備", 2, 7 },
                    { 17, "work", "工作空間", 2, 8 },
                    { 20, "medical_services", "急救包", 3, 2 },
                    { 19, "detector_smoke", "煙霧警報器", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "ServiceID", "IconPath", "ServiceName", "ServiceTypeID", "Sort" },
                values: new object[,]
                {
                    { 21, "detector_alarm", "一氧化碳警報器", 3, 3 },
                    { 13, "soap", "洗衣機", 2, 4 },
                    { 22, "fire_extinguisher", "滅火器", 3, 4 },
                    { 18, "shower", "戶外淋浴空間", 2, 9 },
                    { 12, "kitchen", "廚房", 2, 3 },
                    { 4, "outdoor_grill", "烤肉區", 1, 4 },
                    { 10, "rss_feed", "Wifi", 2, 1 },
                    { 9, "sports_tennis", "運動器材", 1, 9 },
                    { 8, "kebab_dining", "戶外用餐區", 1, 8 },
                    { 7, "fireplace", "室內壁爐", 1, 7 },
                    { 6, "sports_handball", "撞球桌", 1, 6 },
                    { 5, "local_fire_department", "火坑", 1, 5 },
                    { 3, "outdoor_garden", "庭院", 1, 3 },
                    { 2, "spa", "按摩浴池", 1, 2 },
                    { 1, "waves", "游泳池", 1, 1 },
                    { 11, "tv", "電視", 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "WishList",
                columns: new[] { "WishlistID", "UserAccountID", "WishGroupName" },
                values: new object[,]
                {
                    { 3, 2, "黃金之風" },
                    { 1, 1, "星塵鬥士" },
                    { 2, 1, "不滅鑽石" },
                    { 4, 4, "石之海" }
                });

            migrationBuilder.InsertData(
                table: "Listing",
                columns: new[] { "ListingID", "Address", "BathRoom", "Bed", "BedRoom", "CategoryID", "CreateTime", "DefaultPrice", "Description", "EditTime", "Expected", "HighlightId", "IndieBathroom", "Lat", "ListingName", "Lng", "PropertyID", "Status", "Toilet", "UserAcountID" },
                values: new object[,]
                {
                    { 4, "台南市-東區-大學路1號", 1, 1, 1, 2, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1450m, "小日曆文旅是老宅改造後的文藝空間，屋主是個喜歡獨立樂團的女孩。裝潢很簡單，屬於北歐簡約風格，公共區域沒有廚房、沒有電視，但都來台南了，誰晚上會窩在房間？", null, 2, 5, false, 22.997264999999999, "帶我去找夜生活", 120.219981, 1, 1, 1, 2 },
                    { 5, "桃園市-中壢區-中大路300號", 1, 1, 1, 2, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 750m, "北歐簡約風格，房內療癒的綠色植物可以讓你達到放鬆的感覺。位於桃園中壢，享有詳和寧靜的住宅空間及便利的生活機能條件。", null, 2, 1, false, 24.965696699999999, "穿越光年的孤獨", 121.194406, 1, 1, 1, 2 },
                    { 8, "台中市-西屯區-臺灣大道四段1727號", 1, 1, 1, 2, new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 999m, "客房主要已簡單溫馨的北歐風格，雅致舒適，乾淨整潔,獨立乾濕分離衛浴。獨立套房，不會與任何人共用，歡迎獨立女性入住", null, 2, 4, false, 24.176976400000001, "有朵玫瑰玖依依", 120.64243329999999, 3, 1, 1, 5 },
                    { 7, "高雄市-鼓山區-蓮海路70號", 1, 2, 2, 2, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500m, "鄰近駁二藝術特區、愛河、城市光廊、高雄展覽館、星光水岸公園等知名景點；距離六合夜市也短短車程便可抵達，座落市區的地理位置更是讓旅人可便利地搭乘大眾交通工具輕鬆遨遊各景點，是您來到高雄旅行的最佳住宿選擇。房間內基本設備一應俱全，充滿設計感的完善規劃減輕了旅人的壓迫感，可全然地在此放鬆身心，讓不論是家庭出遊、戀人甜蜜旅行或是好友結伴都能在此度過難忘的美好假期。", null, 4, 4, false, 22.628388000000001, "法蘭黛，接下來要去哪", 120.264737, 4, 1, 1, 4 },
                    { 6, "臺北市-中正區-羅斯福路四段一號", 4, 8, 4, 3, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000m, "以簡約輕奢的風格打造台北市稀有城市空間!在庸碌的市中心忙裡偷閒，午後陽光灑落，愜意地在大片落地窗旁放鬆，晚上和親朋好友喝個兩杯，帶著一絲醉意愉快的散步回住處，絕對是最棒的享受！", null, 8, 4, true, 25.014105399999998, "山海", 121.5348496, 6, 1, 4, 3 },
                    { 9, "澎湖縣-馬公市-六合路300號", 1, 2, 1, 3, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3600m, "此房型位置海景第ㄧ排，別墅附有私人泳池，當然幾步路即可走到隱密沙灘戲水游泳自潛，有獨立的衛浴、陽台、廚房、烤肉區、吊床、戶外泳池", null, 4, 3, true, 23.573561600000001, "澎湖流浪指南", 119.5831233, 6, 1, 1, 6 },
                    { 10, "台北市-文山區-木柵路一段17巷", 1, 2, 1, 3, new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1688m, "我們是一般民宿，沒有飯店高規格的設備，盡不是最完美，但我們絕對用心。", null, 4, 1, true, 24.9885096, "住在名為未來的波浪裡", 121.5439205, 6, 1, 1, 6 },
                    { 1, "花蓮縣-花蓮市-壽豐鄉大學路二段1號", 1, 1, 1, 3, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3999m, "森林中的玻璃屋，位於半山坡上，可以遠眺大海，離花蓮市區也僅 10 分鐘車程。夜晚還有美麗夜景伴隨。溫馨日系風格的房間、客廳和浴室，加上山景環繞，讓您度假感十足。", null, 2, 2, false, 23.894196399999998, "麋途", 121.5414543, 8, 1, 1, 1 },
                    { 12, "嘉義縣-民雄鄉-大學路一段168號", 1, 2, 2, 3, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2880m, "使用現代經典傢具，搭配清水模主牆面和南洋植物，加上戶外露台，讓您度假感十足。", null, 4, 6, true, 23.562181899999999, "在室逃跑計劃", 120.46981959999999, 8, 1, 1, 7 },
                    { 3, "台北市-文山區-政大一街353號", 5, 9, 4, 3, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 7890m, "全新裝潢的時尚別墅，擁有廣闊的客廳與餐廳，廚房用具一應俱全，還有各式娛樂設備，任天堂switch遊戲定期更新，KTV歡唱與麻將也是基本的，歡迎揪團來包棟開派對。", null, 10, 5, true, 24.987175799999999, "草西充滿派對", 121.5858604, 10, 1, 6, 3 },
                    { 11, "新竹市-東區-光復路二段101號", 1, 1, 1, 1, new DateTime(2021, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1280m, "我想要開一間尊重狗基本需求的旅館，歡迎狗狗愛好者享受我的空間", null, 3, 4, true, 24.7957091, "房東的狗", 120.9947106, 10, 1, 1, 7 },
                    { 2, "屏東縣-屏東市-民生東路51號", 5, 8, 4, 1, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1350m, "你有多久沒有給自己好好放一個假？我們位在墾丁臨海的小村莊。「大口呼吸用心生活，是我們的訴求」。想要短暫的鄉村體驗的你們，歡迎加入我們！", null, 10, 1, true, 22.657650799999999, "他們說我是有用的年輕人", 120.51230150000001, 15, 1, 5, 1 },
                    { 13, "桃園市-中壢區-中大路300號", 1, 1, 1, 3, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 799m, "想像過住在太空艙裡是什麼樣的感覺嗎？有間青年旅館「你的酒館對我打了烊」位於中壢市區，不但床位都是獨立包廂，也能享受青旅價格，更重要的是入住就送調酒一杯", null, 1, 6, false, 23.951143099999999, "你的酒館對我打了烊", 120.9306649, 23, 1, 1, 22 },
                    { 14, "南投縣-埔里鎮-大學路1號", 1, 4, 2, 3, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 7900m, "你曾經夢想”樹屋”出現在真實生活中嗎?可以在爬到樹屋裡玩耍,鳥瞰遠方？相信許多人小時候都曾經希望能到樹屋裡玩耍或是有個樹屋當秘密基地。“樹屋”與自然並存的方式，給人一種趣味及夢幻的感覺，讓人們可以融合在大自然裡，被溫柔的療癒及紓壓。如果你也想體驗住在樹屋的奇幻經驗，現在不用只能看故事書想像，也不用飛到國外，在台灣就有特色的”樹屋”民宿，等你來進入夢幻國度~~", null, 4, 6, true, 24.965696699999999, "浪流連", 121.194406, 36, 1, 1, 22 },
                    { 15, "台北市-大安區-忠孝東路三段1號", 1, 1, 1, 3, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 699m, "好地方，住了就怕出不來。接受包年方案，也接受分期付款", null, 1, 4, true, 25.0424604, "BuildSchool Style Hotel", 121.53565039999999, 37, 1, 1, 23 }
                });

            migrationBuilder.InsertData(
                table: "Calendar",
                columns: new[] { "CalendarID", "Available", "CalendarDate", "ListingID", "OrderID", "Price" },
                values: new object[,]
                {
                    { 3, true, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 2500m },
                    { 1, false, new DateTime(2023, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 1500m },
                    { 5, false, new DateTime(2022, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 1500m },
                    { 6, true, new DateTime(2022, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 999m },
                    { 7, true, new DateTime(2022, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 999m },
                    { 2, false, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 1500m },
                    { 4, false, new DateTime(2022, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 1300m }
                });

            migrationBuilder.InsertData(
                table: "LegalListing",
                columns: new[] { "LLegalID", "LegalID", "ListingID" },
                values: new object[,]
                {
                    { 4, 1, 1 },
                    { 2, 1, 1 },
                    { 1, 1, 1 },
                    { 5, 1, 1 },
                    { 3, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "ListingImage",
                columns: new[] { "ListingImageID", "ListingID", "ListingImagePath" },
                values: new object[,]
                {
                    { 1, 1, "~/assert/houseIMG/HousePic001.webp" },
                    { 2, 1, "~/assert/houseIMG/HousePic002.webp" },
                    { 4, 1, "~/assert/houseIMG/HousePic004.webp" },
                    { 5, 1, "~/assert/houseIMG/HousePic005.webp" },
                    { 56, 12, "~/assert/houseIMG/HousePic056.webp" },
                    { 16, 4, "~/assert/houseIMG/HousePic016.webp" },
                    { 55, 10, "~/assert/houseIMG/HousePic055.webp" },
                    { 54, 10, "~/assert/houseIMG/HousePic054.webp" },
                    { 53, 10, "~/assert/houseIMG/HousePic053.webp" },
                    { 52, 10, "~/assert/houseIMG/HousePic052.webp" },
                    { 51, 10, "~/assert/houseIMG/HousePic051.webp" },
                    { 45, 9, "~/assert/houseIMG/HousePic045.webp" },
                    { 44, 9, "~/assert/houseIMG/HousePic044.webp" },
                    { 43, 9, "~/assert/houseIMG/HousePic043.webp" },
                    { 42, 9, "~/assert/houseIMG/HousePic042.webp" },
                    { 41, 9, "~/assert/houseIMG/HousePic041.webp" },
                    { 30, 6, "~/assert/houseIMG/HousePic030.webp" },
                    { 29, 6, "~/assert/houseIMG/HousePic029.webp" },
                    { 28, 6, "~/assert/houseIMG/HousePic028.webp" },
                    { 57, 12, "~/assert/houseIMG/HousePic057.webp" },
                    { 27, 6, "~/assert/houseIMG/HousePic027.webp" },
                    { 58, 12, "~/assert/houseIMG/HousePic058.webp" },
                    { 60, 12, "~/assert/houseIMG/HousePic060.webp" },
                    { 75, 15, "~/assert/houseIMG/HousePic075.webp" },
                    { 74, 15, "~/assert/houseIMG/HousePic074.webp" },
                    { 73, 15, "~/assert/houseIMG/HousePic073.webp" },
                    { 72, 15, "~/assert/houseIMG/HousePic072.webp" },
                    { 71, 15, "~/assert/houseIMG/HousePic071.webp" },
                    { 70, 14, "~/assert/houseIMG/HousePic070.webp" },
                    { 69, 14, "~/assert/houseIMG/HousePic069.webp" }
                });

            migrationBuilder.InsertData(
                table: "ListingImage",
                columns: new[] { "ListingImageID", "ListingID", "ListingImagePath" },
                values: new object[,]
                {
                    { 68, 14, "~/assert/houseIMG/HousePic068.webp" },
                    { 67, 14, "~/assert/houseIMG/HousePic067.webp" },
                    { 66, 14, "~/assert/houseIMG/HousePic066.webp" },
                    { 65, 13, "~/assert/houseIMG/HousePic065.webp" },
                    { 64, 13, "~/assert/houseIMG/HousePic064.webp" },
                    { 63, 13, "~/assert/houseIMG/HousePic063.webp" },
                    { 62, 13, "~/assert/houseIMG/HousePic062.webp" },
                    { 59, 12, "~/assert/houseIMG/HousePic059.webp" },
                    { 61, 13, "~/assert/houseIMG/HousePic061.webp" },
                    { 9, 2, "~/assert/houseIMG/HousePic009.webp" },
                    { 8, 2, "~/assert/houseIMG/HousePic008.webp" },
                    { 7, 2, "~/assert/houseIMG/HousePic007.webp" },
                    { 6, 2, "~/assert/houseIMG/HousePic006.webp" },
                    { 50, 11, "~/assert/houseIMG/HousePic050.webp" },
                    { 49, 11, "~/assert/houseIMG/HousePic049.webp" },
                    { 48, 11, "~/assert/houseIMG/HousePic048.webp" },
                    { 47, 11, "~/assert/houseIMG/HousePic047.webp" },
                    { 46, 11, "~/assert/houseIMG/HousePic046.webp" },
                    { 15, 3, "~/assert/houseIMG/HousePic015.webp" },
                    { 14, 3, "~/assert/houseIMG/HousePic014.webp" },
                    { 13, 3, "~/assert/houseIMG/HousePic013.webp" },
                    { 12, 3, "~/assert/houseIMG/HousePic012.webp" },
                    { 11, 3, "~/assert/houseIMG/HousePic011.webp" },
                    { 10, 2, "~/assert/houseIMG/HousePic010.webp" },
                    { 26, 6, "~/assert/houseIMG/HousePic026.webp" },
                    { 3, 1, "~/assert/houseIMG/HousePic003.webp" },
                    { 23, 5, "~/assert/houseIMG/HousePic023.webp" },
                    { 31, 7, "~/assert/houseIMG/HousePic031.webp" },
                    { 24, 5, "~/assert/houseIMG/HousePic024.webp" },
                    { 25, 5, "~/assert/houseIMG/HousePic025.webp" },
                    { 34, 7, "~/assert/houseIMG/HousePic034.webp" },
                    { 35, 7, "~/assert/houseIMG/HousePic035.webp" },
                    { 22, 5, "~/assert/houseIMG/HousePic022.webp" },
                    { 20, 4, "~/assert/houseIMG/HousePic020.webp" },
                    { 19, 4, "~/assert/houseIMG/HousePic019.webp" },
                    { 32, 7, "~/assert/houseIMG/HousePic032.webp" },
                    { 17, 4, "~/assert/houseIMG/HousePic017.webp" },
                    { 36, 8, "~/assert/houseIMG/HousePic036.webp" },
                    { 37, 8, "~/assert/houseIMG/HousePic037.webp" },
                    { 38, 8, "~/assert/houseIMG/HousePic038.webp" },
                    { 39, 8, "~/assert/houseIMG/HousePic039.webp" },
                    { 40, 8, "~/assert/houseIMG/HousePic040.webp" }
                });

            migrationBuilder.InsertData(
                table: "ListingImage",
                columns: new[] { "ListingImageID", "ListingID", "ListingImagePath" },
                values: new object[,]
                {
                    { 33, 7, "~/assert/houseIMG/HousePic033.webp" },
                    { 18, 4, "~/assert/houseIMG/HousePic018.webp" },
                    { 21, 5, "~/assert/houseIMG/HousePic021.webp" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderID", "Adults", "CheckInDate", "Children", "CreateDate", "CustomerID", "FinishDate", "Infants", "ListingID", "PaymentType", "Status", "StayNight", "TranStatus", "UnitPrice" },
                values: new object[,]
                {
                    { 5, 2, new DateTime(2022, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new DateTime(2022, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 12, 1, 2, 2, 2, 2990m },
                    { 2, 2, new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2, 1, 2, 2, 3, 2990m },
                    { 10, 1, new DateTime(2022, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, new DateTime(2022, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2, 1, 3, 3, 1, 1350m },
                    { 3, 2, new DateTime(2022, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, new DateTime(2022, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3, 1, 2, 2, 2, 2990m },
                    { 7, 2, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, new DateTime(2021, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 11, 1, 2, 3, 2, 4999m },
                    { 8, 2, new DateTime(2022, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 6, 1, 2, 5, 1, 3500m },
                    { 4, 2, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, new DateTime(2022, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 1, 2, 2, 3, 1990m },
                    { 1, 2, new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 1, 2, 2, 1, 1990m },
                    { 9, 3, new DateTime(2022, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 5, 1, 3, 3, 1, 1590m },
                    { 6, 2, new DateTime(2022, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, new DateTime(2022, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 9, 1, 3, 2, 3, 14000m }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RatingID", "CheckIn", "Clean", "CommentContent", "Communication", "CostPrice", "CreatTime", "ListingID", "Location", "Precise", "RatingAvg", "UserID" },
                values: new object[,]
                {
                    { 55, 5, 5, "房子裡的東西不多，但備品和擺設都選的很用心~有質感~", 4, 4, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4, 4, 4.3333333329999997, 16 },
                    { 41, 3, 3, "房東熱心介紹附近美食，有任何問題也樂意幫忙，房間乾淨舒適，下次有機會還會想來住", 5, 4, new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4, 4, 3.8333333330000001, 14 },
                    { 18, 5, 3, "入住沒有壓力，房東回覆快速又很有耐心也會推薦當地美食+景點👍🏻", 5, 4, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 5, 4.5, 10 },
                    { 28, 5, 3, "環境很大 很棒~房東很貼心很親切品味也很好，下次會二訪~~", 5, 3, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4, 5, 4.1666666670000003, 12 },
                    { 62, 4, 4, "因為自己入住時間較晚，房東夫妻不僅彈性調整入住時間，還提供熱敷袋(剛好遇到生理期)，真的很照顧房客。,公共空間與衛浴就與照片一樣，相當舒適，同住也不會有壓力", 5, 3, new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4, 3, 3.8333333330000001, 17 },
                    { 69, 5, 3, "房間乾淨地點極佳", 3, 4, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 4, 5, 4.0, 19 },
                    { 8, 5, 4, "來住第二次了，真的覺得CP值超高~而且房東人很好，有問題詢問都回覆很快", 5, 3, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 4, 5, 4.3333333329999997, 8 },
                    { 1, 4, 5, "房間入住舒適，房東也很親切及立即回覆！cp值很高的房源喔！附近生活機能都很方便！,值得下次再回訪入住！大推", 3, 4, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 5, 4.1666666670000003, 7 },
                    { 12, 3, 3, "房間乾淨整潔，環境清幽舒適，房東親切友善。很適合放鬆休息。在山林之中有蚊蟲是正常現象，記得關好門窗即可", 4, 3, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 3, 3.5, 8 },
                    { 13, 3, 3, "房源非常好~房東非常貼心､熱心，回覆速度非常快！我們騎單車，還讓我們把單車放進一樓，真的覺得很感謝。", 3, 5, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 3, 3.3333333330000001, 9 },
                    { 15, 3, 5, "雖然交通不太好到達，但是整體房間蠻美的，適合幾個朋友上山，帶東西上來煮，廚具餐盤完整，別有一番意境", 5, 3, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 3, 3.6666666669999999, 10 },
                    { 26, 3, 5, "民宿備有多款電器､餐具提供使用，都保持非常新且乾淨。床墊軟硬適中，枕頭也有兩種選擇。會想再訂其他房型體驗。民宿主人很用心也很客氣，有問題會馬上協助解決。", 5, 4, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 5, 4.5, 12 },
                    { 77, 4, 4, "接待及服務的人員都很親切，房內舒適且溫暖(寒流來襲也不擔心，有很給力的暖氣)，民宿很漂亮，每個角落都很好拍照，也有電梯，非常方便。", 4, 3, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 3, 5, 3.8333333330000001, 21 },
                    { 36, 4, 4, "感受非常的好，有置身在森林中的感覺", 4, 5, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 5, 4.5, 13 },
                    { 60, 5, 4, "首先，地點真的非常偏遠，去市區相當不方便，只適合自己帶東西上去煮的旅客，但偏偏此房型又沒有廚房，水槽還在陽台外面。一個晚上打死了四隻小蟑螂，雖然知道在山裡難免，但還是有點難理解。", 3, 5, new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 4, 4.0, 17 },
                    { 70, 5, 4, "非常棒的民宿！環境､人員､設備､地點､服務，都沒話說！從很多小細節，都能看到店家的用心與貼心！", 3, 3, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 5, 4.1666666670000003, 20 },
                    { 78, 4, 4, "民宿週邊視野很好<br>屋頂就可看到日出超棒<br>早餐中西合併好吃😋<br>搭配夢幻的花園景觀更是享受😌💕<br>相信四季都有它不同的美<br>是會讓人想再訪的茶園秘境👍", 4, 3, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 5, 3.8333333330000001, 22 },
                    { 11, 4, 4, "晚上安靜，設備齊全，提供瓶裝水茶包及小點心很貼心。房東回覆快速，入住退房都方便快速。房間只有廁所有對外窗､電視沒第四台稍嫌可惜，其餘都很棒，值得推薦。", 3, 3, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 3, 5, 3.6666666669999999, 8 },
                    { 14, 3, 5, "房間整潔乾淨，冷氣也很涼 好棒！", 4, 4, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 3, 3, 3.6666666669999999, 9 },
                    { 50, 4, 4, "整體來說還不錯！有機會會再選同個房東的房子~", 5, 3, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 5, 5, 4.3333333329999997, 15 },
                    { 51, 5, 5, "非常適合想帶寵物遠離塵囂，去山裡享受山景和蟲鳴的旅客。", 4, 3, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 5, 4.5, 16 },
                    { 3, 3, 5, "房間乾淨舒服，浴室還有暖氣功能喔！附近好停車，還有不錯的早餐店，有空的話可以到附近社區走走，感受一下被綠意包覆的美好。對了！房東親切又貼心，回訊息速度比我們這些整天抱手機的小屁孩還快喔，你可以試試😁", 3, 4, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 4, 4.0, 7 },
                    { 57, 3, 3, "窗簾遮光度不強，淺眠的人白天可能會被亮醒。聽得到樓下的車輛噪音，淺眠的人可能比較難入睡。我是淺眠的人！", 3, 3, new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 3, 3, 3.0, 16 },
                    { 22, 4, 3, "地理位置方便，附近有麥當勞&amp;全聯可以買東西！", 5, 4, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 4, 4.1666666670000003, 11 },
                    { 84, 5, 5, "讚的", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 5, 5, 5.0, 1 },
                    { 83, 5, 5, "讚的", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 5, 5, 5.0, 6 },
                    { 82, 5, 5, "讚的", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 5, 5, 5.0, 5 },
                    { 81, 5, 5, "讚的", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 5, 5, 5.0, 4 },
                    { 80, 5, 5, "讚的", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 5, 5, 5.0, 3 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RatingID", "CheckIn", "Clean", "CommentContent", "Communication", "CostPrice", "CreatTime", "ListingID", "Location", "Precise", "RatingAvg", "UserID" },
                values: new object[,]
                {
                    { 79, 5, 5, "讚的", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 5, 5, 5.0, 2 },
                    { 4, 5, 4, "很棒的空間！超級舒適:)處處都可以感受到屋主滿滿的用心", 5, 4, new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 5, 4.6666666670000003, 7 },
                    { 6, 4, 3, "房間空間不大，但是非常乾淨，應有盡有，房東溝通迅速確實相當有效率，週邊有很多好餐廳，提供相當便利性，性價比高，非常推薦", 3, 4, new DateTime(2021, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 5, 3.8333333330000001, 8 },
                    { 17, 5, 3, "房間空間､整潔度都很棒，唯一美中不足的就是枕頭稍硬", 4, 5, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 5, 4.5, 10 },
                    { 33, 5, 4, "位置很好，住宿指令明確簡單，感淨舒適", 5, 4, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, 5, 4.3333333329999997, 12 },
                    { 40, 5, 3, "房東非常親切", 3, 4, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 4, 3.8333333330000001, 13 },
                    { 54, 4, 4, "住宿空間日系簡約且乾淨舒適，讓人一直想待在房源不離開，非常滿意此次的住宿！", 5, 5, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 5, 4.6666666670000003, 16 },
                    { 66, 4, 5, "超乾淨的房間，床大又好睡，房東還會親自送上現切水果到房門，超優的。免費的停車場。還有提供推薦飲食資訊。非常推推(>^ω^<)", 3, 5, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 5, 4.3333333329999997, 18 },
                    { 73, 3, 3, "房東非常親切！回覆也很迅速~ 房源位置非常好，空間也很寬敞", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, 3, 3.6666666669999999, 21 },
                    { 16, 4, 3, "房間的床略硬一些", 4, 3, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 3, 3.3333333330000001, 10 },
                    { 64, 4, 4, "台灣一行最乾淨最漂亮的一間民宿，空間很大而且真的一點灰塵都沒有。房東姐姐很貼心，入住的第一晚開車帶我們出去吃飯，住的兩晚都準備了水果，非常開心可以住在這裡，以後再來希望還有機會入住", 5, 3, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 4, 4.0, 18 },
                    { 32, 3, 4, "房間風格很棒，乾淨整潔；房東跟小幫手也都很親切，整體入住體驗不錯。", 4, 5, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 4, 3.8333333330000001, 12 },
                    { 5, 5, 5, "有任何問題民宿內管理的小幫手都很熱情幫忙；地點很清幽離恆春市區開車也不遠，房內和公共空間細節都很到位也很用心，期待下次再入住！", 5, 3, new DateTime(2021, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 4, 4.1666666670000003, 8 },
                    { 2, 4, 5, "房間乾淨舒適，我是對灰塵容易過敏的人，這裡完全沒有這個困擾。房東適時貼心的問候，還準備了多種小餅乾供房客食用。", 5, 5, new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 4, 4.5, 7 },
                    { 68, 5, 5, "超級棒的地方，晚上會很安靜。房東的寵物小米超可愛，房東人很nice，會仔細講解附近遊覽方式，還會幫忙訂外賣。房間也很棒，入住當晚地震了一下.......有點嚇人。總之是一個非常棒的地方", 3, 5, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 5, 3, 4.3333333329999997, 19 },
                    { 47, 3, 5, "房東很親切很好溝通，房源跟照片完全一樣，很溫馨､很漂亮､空間舒服又很乾淨，交通也方便，很值得再次入住！", 5, 3, new DateTime(2021, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 5, 4, 4.1666666670000003, 15 },
                    { 31, 4, 4, "狗狗有經過訓練，非常親人", 4, 5, new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 4, 4, 4.1666666670000003, 12 },
                    { 25, 4, 4, "狗狗可愛", 4, 5, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 3, 5, 4.1666666670000003, 11 },
                    { 21, 3, 4, "聯繫上房東很快回覆！cp 值不錯的房間，床非常舒服！", 5, 4, new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 3, 3, 3.6666666669999999, 10 },
                    { 10, 4, 5, "這一次入住的體驗房間 地點 整潔度全部都很好，更重要的是，跟以往住宿體驗的不同，我感受到更多的是人與人之間的溫度，未來再來花蓮玩，一定還會再二訪的。", 4, 4, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 5, 4, 4.3333333329999997, 8 },
                    { 24, 4, 4, "整理住宿體驗很好，房內環境很好，房東也都很快速的回覆相關訊息。非常推薦！", 4, 5, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 5, 4, 4.3333333329999997, 11 },
                    { 65, 4, 5, "客廳看起來比照片上大很多！！！", 3, 4, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, 4, 4.0, 18 },
                    { 61, 3, 5, "ktv很不錯，基本上最新的流行音樂都有~但晚上十點後需要降低音量，隔壁還有住戶", 4, 4, new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, 5, 4.1666666670000003, 17 },
                    { 53, 5, 5, "我必須很誠實地說，我住過這麼多 Airbnb,這邊給我的感覺最為溫馨，在過程中給了我很多的關心與祝福，這樣的感受是非常獨特且針對的！", 3, 3, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, 5, 4.1666666670000003, 16 },
                    { 27, 5, 3, "", 5, 4, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, 4, 4.3333333329999997, 12 },
                    { 52, 5, 4, "房源和描述完全符合！很舒服的地方，在小鄉村裡很安靜，很適合家人和朋友一同入住！提供的早餐食材豐富~", 3, 4, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 5, 4.0, 16 },
                    { 39, 3, 5, "房間舒適乾淨，地理位置有很方便", 5, 3, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 4, 3, 3.8333333330000001, 13 },
                    { 48, 4, 5, "房間乾淨舒適~", 5, 4, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 4, 3, 4.1666666670000003, 15 },
                    { 49, 5, 5, "空間很新很乾淨，交通方便，房東很積極回覆問題，到處都可以看到兩位房東的貼心，很值得再次造訪的房源！", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 3, 4, 4.5, 15 },
                    { 59, 4, 4, "浴室偏小但跟房間都很乾淨舒適，隔音佳，現場跟照片上一摸一樣！,除了招待的水果外知道是慶生之旅還有額外送小蛋糕，覺得很貼心！", 3, 5, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 3, 3, 3.6666666669999999, 16 },
                    { 45, 4, 4, "很乾淨舒服的房源，交通方便，房東人也很親切。住宿以外，商用租借和家景拍攝都很適合。非常推！", 4, 3, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 4, 3, 3.6666666669999999, 15 },
                    { 37, 4, 4, "目前澎湖最喜歡的民宿", 5, 3, new DateTime(2021, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 3, 5, 4.0, 13 },
                    { 30, 5, 3, "非常推薦的好地方！雖然很不想推薦怕以後訂不到", 4, 3, new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 3, 5, 3.8333333330000001, 12 },
                    { 44, 4, 4, "房間就跟照片一樣，且房東能一直保持快速回覆！住宿周圍機能佳！", 3, 4, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 4, 4, 3.8333333330000001, 14 },
                    { 74, 5, 4, "還不錯 只是香薰一點多", 4, 5, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 5, 5, 4.6666666670000003, 21 },
                    { 56, 5, 5, "房間乾淨，只是半夜好像會有水管漏水的聲音，大概持續一個小時。窗簾遮光度不夠，怕光的人要自備眼罩。但是房東人很好又準備水果，cp值很高，整體很讚~", 3, 5, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4, 4, 4.3333333329999997, 16 },
                    { 42, 5, 5, "房間內容和照片上的一模一樣，房東會很貼心提醒事務，也很有耐心回答入住期間的所以問題，下次會再入住的~謝謝房東", 5, 3, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4, 4, 4.3333333329999997, 14 },
                    { 34, 4, 4, "我是下訂兩晚的時間，但可能沒記錄到隔天就有傳訊息通知退房時間到了，會造成一點小小的不愉快，再麻煩要紀錄清楚！", 3, 4, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 3, 3, 3.5, 12 },
                    { 23, 4, 3, "舒適，地點方便", 5, 5, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 5, 4, 4.3333333329999997, 11 },
                    { 7, 5, 3, "訂房後到入住當天，都能很即時的跟房東溝通，她也會提醒我們注意的事情；入住時第一印象是房間整潔乾淨，乾式分離的浴室也讓住宿品質有所加分", 5, 4, new DateTime(2021, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 3, 5, 4.1666666670000003, 8 },
                    { 19, 4, 5, "地理位置很好，價格合理！", 5, 5, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 3, 4, 4.3333333329999997, 10 },
                    { 29, 4, 4, "房間如照片一樣！", 5, 3, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 5, 3, 4.0, 12 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "RatingID", "CheckIn", "Clean", "CommentContent", "Communication", "CostPrice", "CreatTime", "ListingID", "Location", "Precise", "RatingAvg", "UserID" },
                values: new object[,]
                {
                    { 38, 4, 4, "整體空間乾淨整齊，對外窗採光佳，但如其他房客評論，隔音確實較差了些，容易聽到外頭大雨滂沱或是改裝車呼嘯而過，算是較大聲的白噪音；需要自備牙刷及浴巾等個人衛生用品，但有提供洗髮精與沐浴乳，也有多雙室內拖鞋。", 5, 3, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 5, 3, 4.0, 13 },
                    { 43, 5, 5, "採光很好", 4, 5, new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 4, 4, 4.5, 14 },
                    { 67, 3, 5, "房間牆面有壁癌，沙發有小塊污漬，若整理一下應該會更好。床算好睡，水夠熱但水量有點偏小", 4, 3, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 3, 3, 3.5, 19 },
                    { 63, 3, 4, "環境整潔有待加強，感覺很久沒人住了，蠻多地方真的灰塵蠻多的，沒有清理確實，床下的東西也沒有塞好。不過房東態度很好！住宿位置真的很推", 3, 4, new DateTime(2021, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 5, 4, 3.8333333330000001, 17 },
                    { 72, 4, 3, "房間非常舒適，晚上可以看到滿天的星星，真心覺得住的非常開心！值得再次住宿", 4, 4, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 4, 4, 3.8333333330000001, 20 },
                    { 75, 3, 3, "好喜歡這民宿的風格，所有細節都做得很好，包括裝潢､早餐和服務，有機會一定會再去！希望下次有機會再來的時候可以看到我們神出鬼沒的白色三花貓，哈哈！再次感謝，我們下次見囉 ^_^", 4, 4, new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 3, 3, 3.3333333330000001, 21 },
                    { 20, 5, 5, "到這個民宿真的很想多住幾天，民宿主人非常好雖然都沒見到面，有問題或是聯絡非常迅速", 4, 3, new DateTime(2021, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 5, 5, 4.5, 10 },
                    { 9, 5, 4, "便宜離鬧區又很近，全程可自行辦理入住很方便~一下樓旁邊就有一個小酒吧，非常推薦來這邊住宿", 5, 3, new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 3, 5, 4.1666666670000003, 8 },
                    { 76, 3, 3, "管家小姐親切，設備與照片相符合，孩子玩的很開心。入住方便。簡單快速，房間上住起來大致舒適", 4, 5, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 4, 4, 3.8333333330000001, 21 },
                    { 35, 3, 3, "地點近捷運很方便，價格也合理，房間乾淨，睡起來還算舒適", 3, 4, new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 5, 5, 3.8333333330000001, 13 },
                    { 46, 3, 5, "床好睡､冷氣夠強､吹風機很好用，住起來很舒適也很安靜", 4, 4, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 5, 5, 4.3333333329999997, 15 },
                    { 58, 3, 4, "有任何問題房東都會儘快回答，下次有機會會再訂房", 5, 4, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 3, 3, 3.6666666669999999, 16 },
                    { 71, 4, 5, "房東人很好，沐浴乳用完了還叫外送送來，房間內也有Netflix ､Disney +可以看", 3, 5, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 3, 3, 3.8333333330000001, 20 }
                });

            migrationBuilder.InsertData(
                table: "ServiceListing",
                columns: new[] { "LServiceID", "ListingId", "ServiceID" },
                values: new object[,]
                {
                    { 15, 2, 10 },
                    { 16, 2, 13 },
                    { 17, 2, 14 },
                    { 18, 2, 16 },
                    { 19, 2, 18 },
                    { 22, 2, 21 },
                    { 21, 2, 20 },
                    { 14, 2, 8 },
                    { 23, 2, 22 },
                    { 41, 4, 15 },
                    { 40, 4, 13 },
                    { 39, 4, 10 },
                    { 20, 2, 19 },
                    { 13, 2, 7 },
                    { 11, 2, 3 },
                    { 51, 5, 19 },
                    { 110, 11, 20 },
                    { 111, 11, 21 },
                    { 112, 11, 22 },
                    { 46, 4, 22 },
                    { 45, 4, 21 },
                    { 44, 4, 20 },
                    { 43, 4, 19 },
                    { 42, 4, 16 },
                    { 60, 6, 13 },
                    { 59, 6, 12 },
                    { 58, 6, 11 },
                    { 57, 6, 10 },
                    { 56, 6, 7 }
                });

            migrationBuilder.InsertData(
                table: "ServiceListing",
                columns: new[] { "LServiceID", "ListingId", "ServiceID" },
                values: new object[,]
                {
                    { 55, 6, 2 },
                    { 100, 10, 16 },
                    { 12, 2, 4 },
                    { 52, 5, 20 },
                    { 121, 13, 10 },
                    { 122, 13, 11 },
                    { 103, 10, 21 },
                    { 104, 10, 22 },
                    { 74, 7, 17 },
                    { 73, 7, 16 },
                    { 72, 7, 14 },
                    { 71, 7, 13 },
                    { 70, 7, 12 },
                    { 69, 7, 11 },
                    { 68, 7, 10 },
                    { 137, 15, 10 },
                    { 138, 15, 11 },
                    { 139, 15, 14 },
                    { 140, 15, 16 },
                    { 141, 15, 19 },
                    { 142, 15, 20 },
                    { 136, 14, 22 },
                    { 109, 11, 19 },
                    { 135, 14, 21 },
                    { 133, 14, 19 },
                    { 123, 13, 14 },
                    { 124, 13, 16 },
                    { 125, 13, 19 },
                    { 126, 13, 20 },
                    { 127, 13, 21 },
                    { 128, 13, 22 },
                    { 143, 15, 21 },
                    { 53, 5, 21 },
                    { 54, 5, 22 },
                    { 101, 10, 19 },
                    { 102, 10, 20 },
                    { 129, 14, 10 },
                    { 130, 14, 11 },
                    { 131, 14, 14 },
                    { 132, 14, 16 },
                    { 134, 14, 20 },
                    { 75, 8, 10 }
                });

            migrationBuilder.InsertData(
                table: "ServiceListing",
                columns: new[] { "LServiceID", "ListingId", "ServiceID" },
                values: new object[,]
                {
                    { 107, 11, 14 },
                    { 106, 11, 11 },
                    { 94, 9, 20 },
                    { 93, 9, 19 },
                    { 92, 9, 16 },
                    { 91, 9, 14 },
                    { 113, 12, 10 },
                    { 114, 12, 11 },
                    { 95, 9, 21 },
                    { 115, 12, 14 },
                    { 117, 12, 19 },
                    { 118, 12, 20 },
                    { 119, 12, 21 },
                    { 120, 12, 22 },
                    { 90, 9, 11 },
                    { 89, 9, 10 },
                    { 116, 12, 16 },
                    { 96, 9, 22 },
                    { 78, 8, 20 },
                    { 47, 5, 9 },
                    { 76, 8, 16 },
                    { 77, 8, 19 },
                    { 1, 1, 10 },
                    { 2, 1, 12 },
                    { 3, 1, 13 },
                    { 4, 1, 14 },
                    { 5, 1, 16 },
                    { 6, 1, 17 },
                    { 7, 1, 19 },
                    { 8, 1, 20 },
                    { 9, 1, 21 },
                    { 10, 1, 22 },
                    { 50, 5, 16 },
                    { 49, 5, 13 },
                    { 48, 5, 10 },
                    { 88, 9, 9 },
                    { 108, 11, 16 },
                    { 87, 9, 8 },
                    { 85, 9, 6 },
                    { 37, 3, 21 },
                    { 38, 3, 22 },
                    { 97, 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "ServiceListing",
                columns: new[] { "LServiceID", "ListingId", "ServiceID" },
                values: new object[,]
                {
                    { 98, 10, 11 },
                    { 99, 10, 14 },
                    { 79, 8, 21 },
                    { 36, 3, 20 },
                    { 67, 6, 22 },
                    { 65, 6, 20 },
                    { 64, 6, 19 },
                    { 63, 6, 17 },
                    { 62, 6, 16 },
                    { 61, 6, 14 },
                    { 105, 11, 10 },
                    { 66, 6, 21 },
                    { 86, 9, 7 },
                    { 35, 3, 19 },
                    { 33, 3, 16 },
                    { 84, 9, 5 },
                    { 83, 9, 4 },
                    { 82, 9, 3 },
                    { 81, 9, 2 },
                    { 80, 9, 1 },
                    { 24, 3, 3 },
                    { 25, 3, 5 },
                    { 34, 3, 17 },
                    { 26, 3, 8 },
                    { 28, 3, 10 },
                    { 29, 3, 11 },
                    { 30, 3, 12 },
                    { 31, 3, 13 },
                    { 32, 3, 14 },
                    { 27, 3, 9 },
                    { 144, 15, 22 }
                });

            migrationBuilder.InsertData(
                table: "WishListDetail",
                columns: new[] { "WishlistDetailID", "CreatTime", "ListingID", "WishlistID" },
                values: new object[,]
                {
                    { 2, new DateTime(2022, 10, 4, 10, 35, 39, 162, DateTimeKind.Utc).AddTicks(4951), 2, 1 },
                    { 1, new DateTime(2022, 10, 4, 10, 35, 39, 162, DateTimeKind.Utc).AddTicks(4607), 1, 1 },
                    { 3, new DateTime(2022, 10, 4, 10, 35, 39, 162, DateTimeKind.Utc).AddTicks(4954), 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "CheckIn", "Clean", "CommentContent", "Communication", "CostPrice", "CreatTime", "HostID", "Location", "OrderID", "Precise", "Rating" },
                values: new object[,]
                {
                    { 6, null, null, null, null, null, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 6, null, 4 },
                    { 1, null, null, null, null, null, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 1, null, 5 },
                    { 4, null, null, null, null, null, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 4, null, 2 },
                    { 5, null, null, null, null, null, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 5, null, 2 },
                    { 3, null, null, null, null, null, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, 3, null, 4 },
                    { 2, null, null, null, null, null, new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 2, null, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_ListingID",
                table: "Calendar",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_OrderID",
                table: "Calendar",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_HostID",
                table: "Comment",
                column: "HostID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OrderID",
                table: "Comment",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_ListingId",
                table: "Highlight",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalListing_LegalID",
                table: "LegalListing",
                column: "LegalID");

            migrationBuilder.CreateIndex(
                name: "IX_LegalListing_ListingID",
                table: "LegalListing",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Highligh_HighlightId",
                table: "Listing",
                column: "HighlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_CategoryID",
                table: "Listing",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_PropertyID",
                table: "Listing",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_UserAcountID",
                table: "Listing",
                column: "UserAcountID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingImage_ListingID",
                table: "ListingImage",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ListingID",
                table: "Order",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDate_OrderID",
                table: "OrderDate",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Promo_ListingID",
                table: "Promo",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyType_PropertyGroupID",
                table: "PropertyType",
                column: "PropertyGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ListingID",
                table: "Ratings",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserID",
                table: "Ratings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendListing_ListingID",
                table: "RecommendListing",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendListing_RecommendID",
                table: "RecommendListing",
                column: "RecommendID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceTypeID",
                table: "Service",
                column: "ServiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceListing_ListingId",
                table: "ServiceListing",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceListing_ServiceID",
                table: "ServiceListing",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_UserAccountID",
                table: "WishList",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_WishListDetail_ListingID",
                table: "WishListDetail",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_WishListDetail_WishlistID",
                table: "WishListDetail",
                column: "WishlistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Special Highlight",
                table: "Listing",
                column: "HighlightId",
                principalTable: "Highlight",
                principalColumn: "HighlightId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Highlight_Listing_ListingId",
                table: "Highlight");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "LegalListing");

            migrationBuilder.DropTable(
                name: "ListingImage");

            migrationBuilder.DropTable(
                name: "OrderDate");

            migrationBuilder.DropTable(
                name: "Promo");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RecommendListing");

            migrationBuilder.DropTable(
                name: "ServiceListing");

            migrationBuilder.DropTable(
                name: "WishListDetail");

            migrationBuilder.DropTable(
                name: "Legal");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Recommend");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.DropTable(
                name: "Listing");

            migrationBuilder.DropTable(
                name: "PropertyType");

            migrationBuilder.DropTable(
                name: "PrivacyType");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "Highlight");

            migrationBuilder.DropTable(
                name: "PropertyGroup");
        }
    }
}
