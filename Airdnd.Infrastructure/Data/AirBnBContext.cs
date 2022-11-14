using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Airdnd.Core.Entities;

#nullable disable

namespace Airdnd.Infrastructure.Data
{
    public partial class AirBnBContext : DbContext
    {
        public AirBnBContext()
        {
        }

        public AirBnBContext(DbContextOptions<AirBnBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlockToken> BlockTokens { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Highlight> Highlights { get; set; }
        public virtual DbSet<Legal> Legals { get; set; }
        public virtual DbSet<LegalListing> LegalListings { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<ListingImage> ListingImages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDate> OrderDates { get; set; }
        public virtual DbSet<PrivacyType> PrivacyTypes { get; set; }
        public virtual DbSet<Promo> Promos { get; set; }
        public virtual DbSet<PropertyGroup> PropertyGroups { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Recommend> Recommends { get; set; }
        public virtual DbSet<RecommendListing> RecommendListings { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceListing> ServiceListings { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<WishListDetail> WishListDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:Airdnd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BlockToken>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.ToTable("Calendar");

                entity.HasIndex(e => e.ListingId, "IX_Calendar_ListingID");

                entity.HasIndex(e => e.OrderId, "IX_Calendar_OrderID");

                entity.Property(e => e.CalendarId).HasColumnName("CalendarID");

                entity.Property(e => e.Available).HasComment("是否上架");

                entity.Property(e => e.CalendarDate).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Calendars)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Calendar_Listing");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Calendars)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Calendar_Order");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.HostId, "IX_Comment_HostID");

                entity.HasIndex(e => e.OrderId, "IX_Comment_OrderID");

                entity.Property(e => e.CheckIn).HasComment("入住");

                entity.Property(e => e.Clean).HasComment("乾淨度");

                entity.Property(e => e.Communication).HasComment("溝通");

                entity.Property(e => e.CostPrice).HasComment("性價比");

                entity.Property(e => e.CreatTime).HasColumnType("datetime");

                entity.Property(e => e.HostId)
                    .HasColumnName("HostID")
                    .HasComment("房東ID");

                entity.Property(e => e.Location).HasComment("位置");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Precise).HasComment("準確性");

                entity.Property(e => e.Rating).HasComment("星等");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.HostId)
                    .HasConstraintName("FK_Comment_User Acount");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Order");
            });

            modelBuilder.Entity<Highlight>(entity =>
            {
                entity.ToTable("Highlight");

                entity.HasIndex(e => e.ListingId, "IX_Highlight_ListingId");

                entity.Property(e => e.HighlightName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IconPath)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Highlights)
                    .HasForeignKey(d => d.ListingId);
            });

            modelBuilder.Entity<Legal>(entity =>
            {
                entity.ToTable("Legal");

                entity.Property(e => e.LegalId).HasColumnName("LegalID");

                entity.Property(e => e.LegalName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LegalListing>(entity =>
            {
                entity.HasKey(e => e.LlegalId)
                    .HasName("PK_Special Detail");

                entity.ToTable("LegalListing");

                entity.HasIndex(e => e.LegalId, "IX_LegalListing_LegalID");

                entity.HasIndex(e => e.ListingId, "IX_LegalListing_ListingID");

                entity.Property(e => e.LlegalId)
                    .HasColumnName("LLegalID")
                    .HasComment("監視器、武器");

                entity.Property(e => e.LegalId).HasColumnName("LegalID");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.HasOne(d => d.Legal)
                    .WithMany(p => p.LegalListings)
                    .HasForeignKey(d => d.LegalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Special Detail_Special");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.LegalListings)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Special Detail_Room");
            });

            modelBuilder.Entity<Listing>(entity =>
            {
                entity.ToTable("Listing");

                entity.HasIndex(e => e.HighlightId, "IX_Highligh_HighlightId");

                entity.HasIndex(e => e.CategoryId, "IX_Listing_CategoryID");

                entity.HasIndex(e => e.PropertyId, "IX_Listing_PropertyID");

                entity.HasIndex(e => e.UserAccountId, "IX_Listing_UserAcountID");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("");

                entity.Property(e => e.BathRoom).HasComment("衛浴");

                entity.Property(e => e.Bed).HasComment("床位");

                entity.Property(e => e.BedRoom).HasComment("臥室");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("建立時間");

                entity.Property(e => e.DefaultPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("房價");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasComment("撰寫房源描述");

                entity.Property(e => e.EditTime)
                    .HasColumnType("datetime")
                    .HasComment("編輯時間");

                entity.Property(e => e.Expected).HasComment("人數");

                entity.Property(e => e.IndieBathroom).HasComment("有任何客房專用浴室?");

                entity.Property(e => e.ListingName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("房源名");

                entity.Property(e => e.PropertyId).HasColumnName("PropertyID");

                entity.Property(e => e.Status).HasComment("狀態: 0編輯中，1上架、2 下架");

                entity.Property(e => e.UserAccountId).HasColumnName("UserAcountID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_CategoryID");

                entity.HasOne(d => d.Highlight)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.HighlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Special Highlight");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Listing_PropertyType");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.UserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_User Acount");
            });

            modelBuilder.Entity<ListingImage>(entity =>
            {
                entity.ToTable("ListingImage");

                entity.HasIndex(e => e.ListingId, "IX_ListingImage_ListingID");

                entity.Property(e => e.ListingImageId)
                    .HasColumnName("ListingImageID")
                    .HasComment("房源圖庫");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.ListingImagePath).IsRequired();

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ListingImages)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Image_Room");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasIndex(e => e.CustomerId, "IX_Order_CustomerID");

                entity.HasIndex(e => e.ListingId, "IX_Order_ListingID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Adults).HasComment("(13+歲)人數");

                entity.Property(e => e.CheckInDate)
                    .HasColumnType("datetime")
                    .HasComment("入住日");

                entity.Property(e => e.Children).HasComment("3~13歲");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.FinishDate)
                    .HasColumnType("datetime")
                    .HasComment("完成訂單日期");

                entity.Property(e => e.Infants).HasComment("0~2歲");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.PayId).IsRequired();

                entity.Property(e => e.PaymentType).HasComment("付款方式");

                entity.Property(e => e.Status).HasComment("enum 訂單狀態");

                entity.Property(e => e.StayNight).HasComment("住幾晚");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("當下原價");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User Acount");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Room");
            });

            modelBuilder.Entity<OrderDate>(entity =>
            {
                entity.ToTable("OrderDate");

                entity.HasIndex(e => e.OrderId, "IX_OrderDate_OrderID");

                entity.Property(e => e.OrderDateId)
                    .HasColumnName("OrderDateID")
                    .HasComment("列出order的每一天");

                entity.Property(e => e.Available).HasComment("一定是False");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderedDate)
                    .HasColumnType("datetime")
                    .HasComment("透過程式把入住日跟天數算出每一天");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDates)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDate_Order");
            });

            modelBuilder.Entity<PrivacyType>(entity =>
            {
                entity.ToTable("PrivacyType");

                entity.Property(e => e.PrivacyTypeId)
                    .HasColumnName("PrivacyTypeID")
                    .HasComment("整套、獨立、合住");

                entity.Property(e => e.PrivacyTypeContent)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PrivacyTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Promo>(entity =>
            {
                entity.ToTable("Promo");

                entity.HasIndex(e => e.ListingId, "IX_Promo_ListingID");

                entity.Property(e => e.PromoId).HasColumnName("PromoID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("整數 => 整數%off");

                entity.Property(e => e.EditTime).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.PromoName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Promos)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Promo_Listing");
            });

            modelBuilder.Entity<PropertyGroup>(entity =>
            {
                entity.ToTable("PropertyGroup");

                entity.Property(e => e.PropertyGroupId)
                    .HasColumnName("PropertyGroupID")
                    .HasComment("公寓、獨棟房屋...等");

                entity.Property(e => e.PropertyGroupName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("PK_Description");

                entity.ToTable("PropertyType");

                entity.HasIndex(e => e.PropertyGroupId, "IX_PropertyType_PropertyGroupID");

                entity.Property(e => e.PropertyId)
                    .HasColumnName("PropertyID")
                    .HasComment("穀倉、船、城堡");

                entity.Property(e => e.IconPath).IsRequired();

                entity.Property(e => e.PropertyContent)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PropertyGroupId).HasColumnName("PropertyGroupID");

                entity.Property(e => e.PropertyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.PropertyGroup)
                    .WithMany(p => p.PropertyTypes)
                    .HasForeignKey(d => d.PropertyGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Description_Room Type");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasIndex(e => e.ListingId, "IX_Ratings_ListingID");

                entity.HasIndex(e => e.UserId, "IX_Ratings_UserID");

                entity.Property(e => e.RatingId).HasColumnName("RatingID");

                entity.Property(e => e.CheckIn).HasComment("入住");

                entity.Property(e => e.Clean).HasComment("乾淨度");

                entity.Property(e => e.Communication).HasComment("溝通");

                entity.Property(e => e.CostPrice).HasComment("性價比");

                entity.Property(e => e.CreatTime).HasColumnType("datetime");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.Location).HasComment("位置");

                entity.Property(e => e.Precise).HasComment("準確性");

                entity.Property(e => e.RatingAvg).HasComment("平均");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.ListingId)
                    .HasConstraintName("FK_Ratings_Listing");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Ratings_UserAccount");
            });

            modelBuilder.Entity<Recommend>(entity =>
            {
                entity.ToTable("Recommend");

                entity.Property(e => e.RecommendId)
                    .HasColumnName("RecommendID")
                    .HasComment("房屋特色(商品頁三欄)");

                entity.Property(e => e.RecommendContent)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RecommendName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RecommendListing>(entity =>
            {
                entity.HasKey(e => e.LrecommendId);

                entity.ToTable("RecommendListing");

                entity.HasIndex(e => e.ListingId, "IX_RecommendListing_ListingID");

                entity.HasIndex(e => e.RecommendId, "IX_RecommendListing_RecommendID");

                entity.Property(e => e.LrecommendId)
                    .ValueGeneratedNever()
                    .HasColumnName("LRecommendID");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.RecommendId).HasColumnName("RecommendID");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.RecommendListings)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommend Detail_Room");

                entity.HasOne(d => d.Recommend)
                    .WithMany(p => p.RecommendListings)
                    .HasForeignKey(d => d.RecommendId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommend Detail_Recommend");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.HasIndex(e => e.ServiceTypeId, "IX_Service_ServiceTypeID");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .HasComment("特殊服務與設施");

                entity.Property(e => e.IconPath).IsRequired();

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Service_ServiceType");
            });

            modelBuilder.Entity<ServiceListing>(entity =>
            {
                entity.HasKey(e => e.LserviceId)
                    .HasName("PK_Service&Facility Detail");

                entity.ToTable("ServiceListing");

                entity.HasIndex(e => e.ListingId, "IX_ServiceListing_ListingId");

                entity.HasIndex(e => e.ServiceId, "IX_ServiceListing_ServiceID");

                entity.Property(e => e.LserviceId).HasColumnName("LServiceID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.ServiceListings)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Service&Facility Detail_Room");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceListings)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Service&Facility Detail_Service&Facility");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.ToTable("ServiceType");

                entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceTypeID");

                entity.Property(e => e.ServiceTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount");

                entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

                entity.Property(e => e.AboutMe).HasMaxLength(500);

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasComment("填寫地址");

                entity.Property(e => e.AvatarUrl).HasComment("頭貼");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Gender).HasComment("性別(0:女性, 1:男性, 2:未指定)");

                entity.Property(e => e.LoginType).HasComment("登入方式  信箱:0  Facebook:1  Google:2  Line:3");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Password).HasComment("");

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.ToTable("WishList");

                entity.HasIndex(e => e.UserAccountId, "IX_WishList_UserAccountID");

                entity.Property(e => e.WishlistId)
                    .HasColumnName("WishlistID")
                    .HasComment("心願單");

                entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");

                entity.Property(e => e.WishGroupName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.UserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wish List_User Acount");
            });

            modelBuilder.Entity<WishListDetail>(entity =>
            {
                entity.ToTable("WishListDetail");

                entity.HasIndex(e => e.ListingId, "IX_WishListDetail_ListingID");

                entity.HasIndex(e => e.WishlistId, "IX_WishListDetail_WishlistID");

                entity.Property(e => e.WishlistDetailId).HasColumnName("WishlistDetailID");

                entity.Property(e => e.ListingId).HasColumnName("ListingID");

                entity.Property(e => e.WishlistId).HasColumnName("WishlistID");

                entity.HasOne(d => d.Listing)
                    .WithMany(p => p.WishListDetails)
                    .HasForeignKey(d => d.ListingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WishList Detail_Room");

                entity.HasOne(d => d.Wishlist)
                    .WithMany(p => p.WishListDetails)
                    .HasForeignKey(d => d.WishlistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WishList Detail_Wish List1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
