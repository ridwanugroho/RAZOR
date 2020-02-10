﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using belajarRazor.Data;

namespace belajarRazor.Migrations
{
    [DbContext(typeof(AppDbContex))]
    partial class AppDbContexModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("belajarRazor.Models.Barang", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("editedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("img_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Barang");
                });

            modelBuilder.Entity("belajarRazor.Models.Cart", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Userid")
                        .HasColumnType("int");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("editedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("belajarRazor.Models.Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Barangid")
                        .HasColumnType("int");

                    b.Property<int?>("cartid")
                        .HasColumnType("int");

                    b.Property<int>("qty")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Barangid");

                    b.HasIndex("cartid");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("belajarRazor.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("authLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("belajarRazor.Models.Cart", b =>
                {
                    b.HasOne("belajarRazor.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("Userid");
                });

            modelBuilder.Entity("belajarRazor.Models.Item", b =>
                {
                    b.HasOne("belajarRazor.Models.Barang", "Barang")
                        .WithMany()
                        .HasForeignKey("Barangid");

                    b.HasOne("belajarRazor.Models.Cart", "cart")
                        .WithMany("Items")
                        .HasForeignKey("cartid");
                });
#pragma warning restore 612, 618
        }
    }
}
