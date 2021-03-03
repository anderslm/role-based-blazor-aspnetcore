﻿// <auto-generated />
using System;
using BankApi;
using BankApi.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankApi.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20210303080919_UseStatements")]
    partial class UseStatements
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BankApi.AccountStatementModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("AccountStatements");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0d700d0c-ce7f-4f37-9e74-e3776a214a45"),
                            Amount = 4200,
                            Owner = "account-owner@andersmarchsteiner.onmicrosoft.com",
                            Timestamp = new DateTimeOffset(new DateTime(2021, 3, 3, 9, 9, 19, 489, DateTimeKind.Unspecified).AddTicks(172), new TimeSpan(0, 1, 0, 0, 0))
                        });
                });
#pragma warning restore 612, 618
        }
    }
}