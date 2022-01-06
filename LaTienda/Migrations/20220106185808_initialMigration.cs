using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LaTienda.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CUIT = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domicilio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.CUIT);
                });

            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Talles",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talle", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    Vendedor = table.Column<int>(type: "int", nullable: false),
                    CUITCliente = table.Column<long>(type: "bigint", nullable: false),
                    TipoComprobante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Venta_Cliente_CUITCliente",
                        column: x => x.CUITCliente,
                        principalTable: "Clientes",
                        principalColumn: "CUIT",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdMarca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Costo = table.Column<float>(type: "real", nullable: false),
                    NetoGravado = table.Column<float>(type: "real", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    MargenGanancia = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Producto_Marca_IdMarca",
                        column: x => x.IdMarca,
                        principalTable: "Marcas",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineasStock",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdColor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTalle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSucursal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaStock", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_LineaStock_Color_IdColor",
                        column: x => x.IdColor,
                        principalTable: "Colores",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaStock_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaStock_Sucursal_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaStock_Talle_IdTalle",
                        column: x => x.IdTalle,
                        principalTable: "Talles",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineasVenta",
                columns: table => new
                {
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrecioUnitario = table.Column<float>(type: "real", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<float>(type: "real", nullable: false),
                    IdVenta = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaVenta", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_LineaVenta_Producto_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineaVenta_Venta_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Ventas",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineaStock_IdColor",
                table: "LineasStock",
                column: "IdColor");

            migrationBuilder.CreateIndex(
                name: "IX_LineaStock_IdProducto",
                table: "LineasStock",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_LineaStock_IdSucursal",
                table: "LineasStock",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_LineaStock_IdTalle",
                table: "LineasStock",
                column: "IdTalle");

            migrationBuilder.CreateIndex(
                name: "IX_LineaVenta_IdProducto",
                table: "LineasVenta",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_LineaVenta_IdVenta",
                table: "LineasVenta",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdMarca",
                table: "Productos",
                column: "IdMarca");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_CUITCliente",
                table: "Ventas",
                column: "CUITCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineasStock");

            migrationBuilder.DropTable(
                name: "LineasVenta");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Talles");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
