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
    [Migration("20200220071414_create-conversation-table")]
    partial class createconversationtable
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

                    b.Property<int>("UserID")
                        .HasColumnType("int");

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

            modelBuilder.Entity("belajarRazor.Models.Conversation", b =>
                {
                    b.Property<Guid>("uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Fromid")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Read")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Toid")
                        .HasColumnType("int");

                    b.HasKey("uuid");

                    b.HasIndex("Fromid");

                    b.HasIndex("Toid");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("belajarRazor.Models.Purchases", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TransactionsDetailid")
                        .HasColumnType("int");

                    b.Property<int?>("Userid")
                        .HasColumnType("int");

                    b.Property<string>("_ItemsDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("courir")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TransactionsDetailid");

                    b.HasIndex("Userid");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("belajarRazor.Models.TransactionDetails", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("_actions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_va_numbers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bill_key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("biller_code")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("belajarRazor.Models.Conversation", b =>
                {
                    b.HasOne("belajarRazor.Models.User", "From")
                        .WithMany()
                        .HasForeignKey("Fromid");

                    b.HasOne("belajarRazor.Models.User", "To")
                        .WithMany()
                        .HasForeignKey("Toid");
                });

            modelBuilder.Entity("belajarRazor.Models.Purchases", b =>
                {
                    b.HasOne("belajarRazor.Models.TransactionDetails", "TransactionsDetail")
                        .WithMany()
                        .HasForeignKey("TransactionsDetailid");

                    b.HasOne("belajarRazor.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Userid");
                });
#pragma warning restore 612, 618
        }
    }
}
