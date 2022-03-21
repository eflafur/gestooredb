﻿// <auto-generated />
using Ecosystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ecosystem.Migrations
{
    [DbContext(typeof(IOTManager))]
    partial class IOTManagerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Ecosystem.Entity.application", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<int>("port")
                        .HasColumnType("integer");

                    b.Property<string>("url")
                        .HasColumnType("text");

                    b.Property<int>("userprofile_id")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("userprofile_id");

                    b.ToTable("application");
                });

            modelBuilder.Entity("Ecosystem.Entity.application_datacenter", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("application_id")
                        .HasColumnType("integer");

                    b.Property<int>("datacenter_id")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("application_id");

                    b.HasIndex("datacenter_id");

                    b.ToTable("application_datacenter");
                });

            modelBuilder.Entity("Ecosystem.Entity.container", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("service")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("container");
                });

            modelBuilder.Entity("Ecosystem.Entity.datacenter", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("dimension")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("site")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("datacenter");
                });

            modelBuilder.Entity("Ecosystem.Entity.organigram", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("leader")
                        .HasColumnType("text");

                    b.Property<string>("worker")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("organigram");
                });

            modelBuilder.Entity("Ecosystem.Entity.rediskey", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("key")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("rediskey");
                });

            modelBuilder.Entity("Ecosystem.Entity.service", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("container_id")
                        .HasColumnType("integer");

                    b.Property<string>("idenv")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<int>("servicecategory_id")
                        .HasColumnType("integer");

                    b.Property<int>("tenant_id")
                        .HasColumnType("integer");

                    b.Property<string>("type")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("container_id");

                    b.HasIndex("servicecategory_id");

                    b.HasIndex("tenant_id");

                    b.ToTable("service");
                });

            modelBuilder.Entity("Ecosystem.Entity.servicecategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("servicecategory");
                });

            modelBuilder.Entity("Ecosystem.Entity.tenant", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("area")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("site")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("tenant");
                });

            modelBuilder.Entity("Ecosystem.Entity.userprofile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("firstname")
                        .HasColumnType("text");

                    b.Property<string>("lastname")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("userprofile");
                });

            modelBuilder.Entity("Ecosystem.Entity.application", b =>
                {
                    b.HasOne("Ecosystem.Entity.userprofile", "userprofile")
                        .WithMany()
                        .HasForeignKey("userprofile_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ecosystem.Entity.application_datacenter", b =>
                {
                    b.HasOne("Ecosystem.Entity.application", "application")
                        .WithMany()
                        .HasForeignKey("application_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecosystem.Entity.datacenter", "datacenter")
                        .WithMany()
                        .HasForeignKey("datacenter_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ecosystem.Entity.service", b =>
                {
                    b.HasOne("Ecosystem.Entity.container", "container")
                        .WithMany()
                        .HasForeignKey("container_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecosystem.Entity.servicecategory", "servicecategory")
                        .WithMany()
                        .HasForeignKey("servicecategory_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecosystem.Entity.tenant", "tenant")
                        .WithMany()
                        .HasForeignKey("tenant_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}