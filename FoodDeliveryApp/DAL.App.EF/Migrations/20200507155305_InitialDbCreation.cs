using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 64, nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: false),
                    LastName = table.Column<string>(maxLength: 64, nullable: false),
                    Phone = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Phone = table.Column<string>(maxLength: 64, nullable: false),
                    Address = table.Column<string>(maxLength: 512, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    County = table.Column<string>(maxLength: 64, nullable: false),
                    City = table.Column<string>(maxLength: 64, nullable: false),
                    Street = table.Column<string>(maxLength: 64, nullable: false),
                    BuildingNumber = table.Column<string>(maxLength: 64, nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: false),
                    CustomerId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    RestaurantId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemTypes_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    FoodCost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DeliveryCost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Address = table.Column<string>(maxLength: 512, nullable: false),
                    Apartment = table.Column<string>(maxLength: 512, nullable: true),
                    Comment = table.Column<string>(maxLength: 512, nullable: true),
                    CustomerId = table.Column<Guid>(maxLength: 36, nullable: false),
                    RestaurantId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<Guid>(maxLength: 36, nullable: false),
                    RestaurantId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantCategories_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    WeekDay = table.Column<int>(nullable: false),
                    OpeningTime = table.Column<DateTime>(nullable: false),
                    ClosingTime = table.Column<DateTime>(nullable: false),
                    RestaurantId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHourses_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    PictureLink = table.Column<string>(maxLength: 512, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    ItemTypeId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    IsSingle = table.Column<bool>(nullable: false),
                    ItemId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOptions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    ItemId = table.Column<Guid>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(7,3)", nullable: false),
                    Unit = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionInfos_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    ItemId = table.Column<Guid>(maxLength: 36, nullable: true),
                    OrderId = table.Column<Guid>(maxLength: 36, nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Comment = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderRows_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderRows_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    AdditionalPrice = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    ItemOptionId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemChoices_ItemOptions_ItemOptionId",
                        column: x => x.ItemOptionId,
                        principalTable: "ItemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    OrderRowId = table.Column<Guid>(maxLength: 36, nullable: true),
                    ItemChoiceId = table.Column<Guid>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemChoices_ItemChoices_ItemChoiceId",
                        column: x => x.ItemChoiceId,
                        principalTable: "ItemChoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemChoices_OrderRows_OrderRowId",
                        column: x => x.OrderRowId,
                        principalTable: "OrderRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemChoices_ItemOptionId",
                table: "ItemChoices",
                column: "ItemOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOptions_ItemId",
                table: "ItemOptions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_RestaurantId",
                table: "ItemTypes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionInfos_ItemId",
                table: "NutritionInfos",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemChoices_ItemChoiceId",
                table: "OrderItemChoices",
                column: "ItemChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemChoices_OrderRowId",
                table: "OrderItemChoices",
                column: "OrderRowId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_ItemId",
                table: "OrderRows",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_OrderId",
                table: "OrderRows",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantId",
                table: "Orders",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantCategories_CategoryId",
                table: "RestaurantCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantCategories_RestaurantId",
                table: "RestaurantCategories",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHourses_RestaurantId",
                table: "WorkingHourses",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "NutritionInfos");

            migrationBuilder.DropTable(
                name: "OrderItemChoices");

            migrationBuilder.DropTable(
                name: "RestaurantCategories");

            migrationBuilder.DropTable(
                name: "WorkingHourses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ItemChoices");

            migrationBuilder.DropTable(
                name: "OrderRows");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ItemOptions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
