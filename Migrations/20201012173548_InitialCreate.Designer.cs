﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bagel_sales_control.DataContext;

namespace bagel_sales_control.Migrations
{
    [DbContext(typeof(BagelSalesControlContext))]
    [Migration("20201012173548_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("bagel_sales_control.Features.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("Existence")
                        .HasColumnType("int");

                    b.Property<int>("InitialExistence")
                        .HasColumnType("int");

                    b.Property<string>("NameProduct")
                        .HasColumnType("text");

                    b.Property<int>("PurchasePrice")
                        .HasColumnType("int");

                    b.Property<int>("SalePrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime");

                    b.Property<int>("Wholesaleprice")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
