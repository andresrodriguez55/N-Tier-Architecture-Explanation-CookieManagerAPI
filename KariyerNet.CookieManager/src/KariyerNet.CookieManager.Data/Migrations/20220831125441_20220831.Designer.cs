﻿// <auto-generated />
using System;
using KariyerNet.CookieManager.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KariyerNet.CookieManager.Data.Migrations
{
    [DbContext(typeof(CookieSettingsContext))]
    [Migration("20220831125441_20220831")]
    partial class _20220831
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CookiesSettings.Models.Cookie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<int>("WebSiteCookieTypeDefinitionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WebSiteCookieTypeDefinitionId");

                    b.ToTable("Cookies", (string)null);
                });

            modelBuilder.Entity("CookiesSettings.Models.WebSite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("WebSites", (string)null);
                });

            modelBuilder.Entity("CookiesSettings.Models.WebSiteCookieTypeDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CookieType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<int>("WebSiteId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WebSiteId");

                    b.ToTable("WebSiteCookieTypeDefinitions", (string)null);
                });

            modelBuilder.Entity("CookiesSettings.Models.Cookie", b =>
                {
                    b.HasOne("CookiesSettings.Models.WebSiteCookieTypeDefinition", "WebSiteCookieTypeDefinition")
                        .WithMany("Cookies")
                        .HasForeignKey("WebSiteCookieTypeDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WebSiteCookieTypeDefinition");
                });

            modelBuilder.Entity("CookiesSettings.Models.WebSiteCookieTypeDefinition", b =>
                {
                    b.HasOne("CookiesSettings.Models.WebSite", "WebSite")
                        .WithMany("WebSiteCookieTypeDefinitions")
                        .HasForeignKey("WebSiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WebSite");
                });

            modelBuilder.Entity("CookiesSettings.Models.WebSite", b =>
                {
                    b.Navigation("WebSiteCookieTypeDefinitions");
                });

            modelBuilder.Entity("CookiesSettings.Models.WebSiteCookieTypeDefinition", b =>
                {
                    b.Navigation("Cookies");
                });
#pragma warning restore 612, 618
        }
    }
}
