using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace sport.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoUrlToSportingGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses_of_pick-up_points",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"фddresses_of_pick-up_points_id_seq\"'::regclass)"),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("фddresses_of_pick-up_points_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "manufacturers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    manufacturer = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("manufacturers_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("statuses_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    supplier = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("suppliers_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "units_of_measurement",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    unit_of_measurement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("units_of_measurement_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_role = table.Column<int>(type: "integer", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    login = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_ussers_roles",
                        column: x => x.id_role,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sporting_goods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    article = table.Column<string>(type: "text", nullable: false),
                    id_category = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    id_manufacturer = table.Column<int>(type: "integer", nullable: false),
                    id_supplier = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: true),
                    id_unit_of_measurement = table.Column<int>(type: "integer", nullable: true),
                    discount = table.Column<int>(type: "integer", nullable: true),
                    quantity_in_stock = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sporting_goods_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_sporting_goods_categories",
                        column: x => x.id_category,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_sporting_goods_manufacturers",
                        column: x => x.id_manufacturer,
                        principalTable: "manufacturers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_sporting_goods_suppliers",
                        column: x => x.id_supplier,
                        principalTable: "suppliers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_sporting_goods_units of measurement",
                        column: x => x.id_unit_of_measurement,
                        principalTable: "units_of_measurement",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_date = table.Column<DateOnly>(type: "date", nullable: true),
                    delivery_date = table.Column<DateOnly>(type: "date", nullable: true),
                    id_delivery_point_address = table.Column<int>(type: "integer", nullable: true),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    id_status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orders_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_addresses_of_pick-up_points",
                        column: x => x.id_delivery_point_address,
                        principalTable: "addresses_of_pick-up_points",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_statuses",
                        column: x => x.id_status,
                        principalTable: "statuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_users",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "orders_sporting_goods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_order = table.Column<int>(type: "integer", nullable: false),
                    id_sporting_goods = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orders_sporting_goods_pkey", x => new { x.id, x.id_order, x.id_sporting_goods });
                    table.ForeignKey(
                        name: "fk_orders_sporting_goods_orders",
                        column: x => x.id_order,
                        principalTable: "orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_sporting_goods_sporting_goods",
                        column: x => x.id_sporting_goods,
                        principalTable: "sporting_goods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_id_delivery_point_address",
                table: "orders",
                column: "id_delivery_point_address");

            migrationBuilder.CreateIndex(
                name: "IX_orders_id_status",
                table: "orders",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_orders_id_user",
                table: "orders",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_orders_sporting_goods_id_order",
                table: "orders_sporting_goods",
                column: "id_order");

            migrationBuilder.CreateIndex(
                name: "IX_orders_sporting_goods_id_sporting_goods",
                table: "orders_sporting_goods",
                column: "id_sporting_goods");

            migrationBuilder.CreateIndex(
                name: "IX_sporting_goods_id_category",
                table: "sporting_goods",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "IX_sporting_goods_id_manufacturer",
                table: "sporting_goods",
                column: "id_manufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_sporting_goods_id_supplier",
                table: "sporting_goods",
                column: "id_supplier");

            migrationBuilder.CreateIndex(
                name: "IX_sporting_goods_id_unit_of_measurement",
                table: "sporting_goods",
                column: "id_unit_of_measurement");

            migrationBuilder.CreateIndex(
                name: "IX_users_id_role",
                table: "users",
                column: "id_role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders_sporting_goods");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "sporting_goods");

            migrationBuilder.DropTable(
                name: "addresses_of_pick-up_points");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "manufacturers");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "units_of_measurement");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
