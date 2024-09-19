﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NFT.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NFT.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NFT.Core.Entities.Collection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("FloorPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MarketCapital")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfSale")
                        .HasColumnType("integer");

                    b.Property<string>("SocialLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Supply")
                        .HasColumnType("integer");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("NFT.Core.Entities.HistoryLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("DealPrice")
                        .HasColumnType("numeric");

                    b.Property<Guid>("NftItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserBuyerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserSellerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("NftItemId");

                    b.HasIndex("SellerId");

                    b.ToTable("HistoryLogs");
                });

            modelBuilder.Entity("NFT.Core.Entities.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("NftItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("NftItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("NFT.Core.Entities.NftItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal?>("AvgDealPrice")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CollectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsListed")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("UserId");

                    b.ToTable("NftItems");
                });

            modelBuilder.Entity("NFT.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NFT.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserRoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NFT.Core.Entities.HistoryLog", b =>
                {
                    b.HasOne("NFT.Core.Entities.User", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NFT.Core.Entities.NftItem", "Nft")
                        .WithMany()
                        .HasForeignKey("NftItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NFT.Core.Entities.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Nft");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("NFT.Core.Entities.Inventory", b =>
                {
                    b.HasOne("NFT.Core.Entities.NftItem", "Nft")
                        .WithMany()
                        .HasForeignKey("NftItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NFT.Core.Entities.User", "User")
                        .WithMany("Inventories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nft");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NFT.Core.Entities.NftItem", b =>
                {
                    b.HasOne("NFT.Core.Entities.Collection", "Collection")
                        .WithMany("NftItems")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NFT.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NFT.Core.Entities.User", b =>
                {
                    b.HasOne("NFT.Core.Entities.Role", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("NFT.Core.Entities.Collection", b =>
                {
                    b.Navigation("NftItems");
                });

            modelBuilder.Entity("NFT.Core.Entities.User", b =>
                {
                    b.Navigation("Inventories");
                });
#pragma warning restore 612, 618
        }
    }
}
