using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.CreateTable(
                name: "chore_lists",
                schema: "Application",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chore_lists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "Application",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    identity_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chores",
                schema: "Application",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    chore_list_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    frequency = table.Column<int>(type: "integer", nullable: false),
                    category = table.Column<int>(type: "integer", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chores", x => x.id);
                    table.ForeignKey(
                        name: "fk_chores_chore_lists_chore_list_id",
                        column: x => x.chore_list_id,
                        principalSchema: "Application",
                        principalTable: "chore_lists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chores_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "Application",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chore_instances",
                schema: "Application",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chore_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_due = table.Column<DateOnly>(type: "date", nullable: false),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    image_links = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chore_instances", x => x.id);
                    table.ForeignKey(
                        name: "fk_chore_instances_chores_chore_id",
                        column: x => x.chore_id,
                        principalSchema: "Application",
                        principalTable: "chores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chore_users",
                schema: "Application",
                columns: table => new
                {
                    chore_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chore_users", x => new { x.chore_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_chore_users_chores_chore_id",
                        column: x => x.chore_id,
                        principalSchema: "Application",
                        principalTable: "chores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chore_users_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "Application",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "Application",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chore_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    last_known_price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    last_known_price_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    last_known_price_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_chores_chore_id",
                        column: x => x.chore_id,
                        principalSchema: "Application",
                        principalTable: "chores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                schema: "Application",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    chore_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    published_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    edited_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    chore_instance_id1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_comments_chore_instances_chore_instance_id",
                        column: x => x.chore_instance_id,
                        principalSchema: "Application",
                        principalTable: "chore_instances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comments_chore_instances_chore_instance_id1",
                        column: x => x.chore_instance_id1,
                        principalSchema: "Application",
                        principalTable: "chore_instances",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_comments_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "Application",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_chore_instances_chore_id",
                schema: "Application",
                table: "chore_instances",
                column: "chore_id");

            migrationBuilder.CreateIndex(
                name: "ix_chore_users_user_id",
                schema: "Application",
                table: "chore_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_chores_chore_list_id",
                schema: "Application",
                table: "chores",
                column: "chore_list_id");

            migrationBuilder.CreateIndex(
                name: "ix_chores_user_id",
                schema: "Application",
                table: "chores",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_chore_instance_id",
                schema: "Application",
                table: "comments",
                column: "chore_instance_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_chore_instance_id1",
                schema: "Application",
                table: "comments",
                column: "chore_instance_id1");

            migrationBuilder.CreateIndex(
                name: "ix_comments_user_id",
                schema: "Application",
                table: "comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_chore_id",
                schema: "Application",
                table: "products",
                column: "chore_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "Application",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chore_users",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "comments",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "products",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "chore_instances",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "chores",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "chore_lists",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "users",
                schema: "Application");
        }
    }
}
