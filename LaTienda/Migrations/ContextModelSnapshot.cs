﻿// <auto-generated />
using System;
using LaTienda.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LaTienda.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LaTienda.Models.Cliente", b =>
                {
                    b.Property<long>("CUIT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Domicilio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazonSocial")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CUIT");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("LaTienda.Models.Color", b =>
                {
                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("LaTienda.Models.LineaStock", b =>
                {
                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdColor")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdProducto")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSucursal")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdTalle")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Codigo");

                    b.HasIndex("IdColor");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdSucursal");

                    b.HasIndex("IdTalle");

                    b.ToTable("LineaStock");
                });

            modelBuilder.Entity("LaTienda.Models.LineaVenta", b =>
                {
                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<Guid>("IdProducto")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdVenta")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("PrecioUnitario")
                        .HasColumnType("real");

                    b.Property<float>("Subtotal")
                        .HasColumnType("real");

                    b.HasKey("Codigo");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdVenta");

                    b.ToTable("LineaVenta");
                });

            modelBuilder.Entity("LaTienda.Models.Marca", b =>
                {
                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("LaTienda.Models.Producto", b =>
                {
                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Costo")
                        .HasColumnType("real");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdMarca")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("MargenGanancia")
                        .HasColumnType("real");

                    b.Property<float>("NetoGravado")
                        .HasColumnType("real");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.HasKey("Codigo");

                    b.HasIndex("IdMarca");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("LaTienda.Models.Sucursal", b =>
                {
                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("Sucursal");
                });

            modelBuilder.Entity("LaTienda.Models.Talle", b =>
                {
                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("Talle");
                });

            modelBuilder.Entity("LaTienda.Models.TicketAutenticacion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GenerationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Service")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TicketAutenticacion");
                });

            modelBuilder.Entity("LaTienda.Models.Venta", b =>
                {
                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("CUITCliente")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("TipoComprobante")
                        .HasColumnType("int");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<int>("Vendedor")
                        .HasColumnType("int");

                    b.HasKey("Codigo");

                    b.HasIndex("CUITCliente");

                    b.ToTable("Venta");
                });

            modelBuilder.Entity("LaTienda.Models.LineaStock", b =>
                {
                    b.HasOne("LaTienda.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("IdColor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTienda.Models.Producto", "Producto")
                        .WithMany("LineasStock")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTienda.Models.Sucursal", "Sucursal")
                        .WithMany()
                        .HasForeignKey("IdSucursal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTienda.Models.Talle", "Talle")
                        .WithMany()
                        .HasForeignKey("IdTalle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Producto");

                    b.Navigation("Sucursal");

                    b.Navigation("Talle");
                });

            modelBuilder.Entity("LaTienda.Models.LineaVenta", b =>
                {
                    b.HasOne("LaTienda.Models.Producto", "Producto")
                        .WithMany("LineasVenta")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTienda.Models.Venta", "Venta")
                        .WithMany("LineasVenta")
                        .HasForeignKey("IdVenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("LaTienda.Models.Producto", b =>
                {
                    b.HasOne("LaTienda.Models.Marca", "Marca")
                        .WithMany("Productos")
                        .HasForeignKey("IdMarca")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("LaTienda.Models.Venta", b =>
                {
                    b.HasOne("LaTienda.Models.Cliente", "Cliente")
                        .WithMany("Ventas")
                        .HasForeignKey("CUITCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("LaTienda.Models.Cliente", b =>
                {
                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("LaTienda.Models.Marca", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("LaTienda.Models.Producto", b =>
                {
                    b.Navigation("LineasStock");

                    b.Navigation("LineasVenta");
                });

            modelBuilder.Entity("LaTienda.Models.Venta", b =>
                {
                    b.Navigation("LineasVenta");
                });
#pragma warning restore 612, 618
        }
    }
}
