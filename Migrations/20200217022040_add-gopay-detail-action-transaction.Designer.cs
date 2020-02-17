﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using belajarRazor.Data;

namespace belajarRazor.Migrations
{
    [DbContext(typeof(AppDbContex))]
    [Migration("20200217022040_add-gopay-detail-action-transaction")]
    partial class addgopaydetailactiontransaction
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("description")
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

            modelBuilder.Entity("belajarRazor.Models.Carts", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("_Items")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("totalPrice")
                        .HasColumnType("float");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("belajarRazor.Models.TransactionDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fraud_status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gross_amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchant_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("order_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payment_type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status_message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transaction_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transaction_status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("transaction_time")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("TransactionDetail");
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

            modelBuilder.Entity("belajarRazor.Models.Virtual", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TransactionDetailsid")
                        .HasColumnType("int");

                    b.Property<string>("bank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("va_number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("TransactionDetailsid");

                    b.ToTable("VaNumber");
                });

            modelBuilder.Entity("belajarRazor.Models.Virtual", b =>
                {
                    b.HasOne("belajarRazor.Models.TransactionDetails", null)
                        .WithMany("va_numbers")
                        .HasForeignKey("TransactionDetailsid");
                });
#pragma warning restore 612, 618
        }
    }
}
