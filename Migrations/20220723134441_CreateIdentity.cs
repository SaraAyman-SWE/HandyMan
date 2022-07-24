using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandyMan.Migrations
{
    public partial class CreateIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Craft",
                columns: table => new
                {
                    Craft_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Craft_Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Craft__D37F5DD90F011EA7", x => x.Craft_ID);
                });

            migrationBuilder.CreateTable(
                name: "HandymanRegion",
                columns: table => new
                {
                    Handyman_SSN = table.Column<int>(type: "int", nullable: false),
                    Region_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandymanRegion", x => new { x.Handyman_SSN, x.Region_ID });
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Region_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region_Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Region__A9EAD51FEE4C6713", x => x.Region_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Handyman",
                columns: table => new
                {
                    Handyman_SSN = table.Column<int>(type: "int", nullable: false),
                    Handyman_Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Handyman_Mobile = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    CraftID = table.Column<int>(type: "int", nullable: false),
                    Handyman_Fixed_Rate = table.Column<int>(type: "int", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    Open_For_Work = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    Handyman_Photo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Handyman_ID_Image = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Handyman_Criminal_Record = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Handyman__9C5E1C4351B08EA4", x => x.Handyman_SSN);
                    table.ForeignKey(
                        name: "FK_Handyman_Craft",
                        column: x => x.CraftID,
                        principalTable: "Craft",
                        principalColumn: "Craft_ID");
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Client_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Region_ID = table.Column<int>(type: "int", nullable: false),
                    Client_Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Client_Address = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Client_Mobile = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Client_ID);
                    table.ForeignKey(
                        name: "FK_Client_Region",
                        column: x => x.Region_ID,
                        principalTable: "Region",
                        principalColumn: "Region_ID");
                });

            migrationBuilder.CreateTable(
                name: "Handyman_Region",
                columns: table => new
                {
                    Handyman_SSN = table.Column<int>(type: "int", nullable: false),
                    Region_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handyman_Region", x => new { x.Handyman_SSN, x.Region_ID });
                    table.ForeignKey(
                        name: "FK_Handyman_Region_Handyman",
                        column: x => x.Handyman_SSN,
                        principalTable: "Handyman",
                        principalColumn: "Handyman_SSN");
                    table.ForeignKey(
                        name: "FK_Handyman_Region_Region",
                        column: x => x.Region_ID,
                        principalTable: "Region",
                        principalColumn: "Region_ID");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Handy_SSN = table.Column<int>(type: "int", nullable: false),
                    Schedule_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time_From = table.Column<TimeSpan>(type: "time", nullable: false),
                    Time_To = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Schedule__79B9CA37213B802B", x => new { x.Handy_SSN, x.Time_From, x.Schedule_Date });
                    table.ForeignKey(
                        name: "FK__Schedule__Handy___4BAC3F29",
                        column: x => x.Handy_SSN,
                        principalTable: "Handyman",
                        principalColumn: "Handyman_SSN");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Request_ID = table.Column<int>(type: "int", nullable: false),
                    Handyman_SSN = table.Column<int>(type: "int", nullable: false),
                    Client_ID = table.Column<int>(type: "int", nullable: false),
                    Request_Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Request_Date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    Request_Order_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Client_Rate = table.Column<int>(type: "int", nullable: true),
                    Client_Review = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Handy_Rate = table.Column<int>(type: "int", nullable: true),
                    Handy_Review = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Request_ID);
                    table.ForeignKey(
                        name: "FK_Request_Client",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "Client_ID");
                    table.ForeignKey(
                        name: "FK_Request_Handyman",
                        column: x => x.Handyman_SSN,
                        principalTable: "Handyman",
                        principalColumn: "Handyman_SSN");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Payment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Request_ID = table.Column<int>(type: "int", nullable: false),
                    Payment_Status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    Method = table.Column<bool>(type: "bit", nullable: false),
                    Payment_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Payment_Amount = table.Column<int>(type: "int", nullable: false),
                    Transaction_ID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => new { x.Payment_ID, x.Request_ID });
                    table.ForeignKey(
                        name: "FK_Payment_Request",
                        column: x => x.Request_ID,
                        principalTable: "Request",
                        principalColumn: "Request_ID");
                });

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
                name: "IX_Client_Client_Email",
                table: "Client",
                column: "Client_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Region_ID",
                table: "Client",
                column: "Region_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__Client__409D25A0AB59A5F9",
                table: "Client",
                column: "Client_Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Handyman_CraftID",
                table: "Handyman",
                column: "CraftID");

            migrationBuilder.CreateIndex(
                name: "UQ__Handyman__F297F34DD98DA7CB",
                table: "Handyman",
                column: "Handyman_Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Handyman_Region_Region_ID",
                table: "Handyman_Region",
                column: "Region_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Request_ID",
                table: "Payment",
                column: "Request_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Client_ID",
                table: "Request",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Handyman_SSN",
                table: "Request",
                column: "Handyman_SSN");

            migrationBuilder.CreateIndex(
                name: "UQ__Request__E9C5B292D224AFDB",
                table: "Request",
                column: "Request_ID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Handyman_Region");

            migrationBuilder.DropTable(
                name: "HandymanRegion");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Handyman");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Craft");
        }
    }
}
