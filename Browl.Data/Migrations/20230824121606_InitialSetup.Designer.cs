﻿// <auto-generated />
using Browl.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Browl.Data.Migrations
{
    [DbContext(typeof(BrowlDbContext))]
    [Migration("20230824121606_InitialSetup")]
    partial class InitialSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Browl.Data.Entities.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Habits");

                    b.HasData(
                        new
                        {
                            Id = 100,
                            Description = "Become a francophone",
                            Name = "Learn French"
                        },
                        new
                        {
                            Id = 101,
                            Description = "Get really fit",
                            Name = "Run a marathon"
                        },
                        new
                        {
                            Id = 102,
                            Description = "Finish your book project",
                            Name = "Write every day"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
