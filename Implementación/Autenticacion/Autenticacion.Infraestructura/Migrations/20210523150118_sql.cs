using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Autenticacion.Infraestructura.Migrations
{
    public partial class sql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_LogAutenticacionAPI",
                columns: table => new
                {
                    IdLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aplicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsExcepcion = table.Column<bool>(type: "bit", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parametros = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LogAutenticacionAPI", x => x.IdLog);
                });

            migrationBuilder.CreateTable(
                name: "AspNetAlgoritmosDeSeguridad",
                columns: table => new
                {
                    Algoritmo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetAlgoritmosDeSeguridad", x => x.Algoritmo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetTiposAutenticacion",
                columns: table => new
                {
                    IdTipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Autenticacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsDirectorioActivo = table.Column<bool>(type: "bit", nullable: false),
                    IdAD = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTiposAutenticacion", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetAplicacion",
                columns: table => new
                {
                    IdAplicacion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    PermiteJWT = table.Column<bool>(type: "bit", nullable: false),
                    AlgoritmoDeSeguridad = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LlaveSecreta = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    MinutosDeVida = table.Column<double>(type: "float", nullable: true),
                    FechaExpiracionLlave = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoLlave = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetAplicacion", x => x.IdAplicacion);
                    table.ForeignKey(
                        name: "FK_AspNetAplicacion_AspNetAlgoritmosDeSeguridad_AlgoritmoDeSeguridad",
                        column: x => x.AlgoritmoDeSeguridad,
                        principalTable: "AspNetAlgoritmosDeSeguridad",
                        principalColumn: "Algoritmo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTipoAuth = table.Column<int>(type: "int", nullable: false),
                    Organizacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsExterno = table.Column<bool>(type: "bit", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetTiposAutenticacion_IdTipoAuth",
                        column: x => x.IdTipoAuth,
                        principalTable: "AspNetTiposAutenticacion",
                        principalColumn: "IdTipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAplicacion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_AspNetAplicacion_IdAplicacion",
                        column: x => x.IdAplicacion,
                        principalTable: "AspNetAplicacion",
                        principalColumn: "IdAplicacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdAplicacion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LongitudToken = table.Column<int>(type: "int", nullable: false),
                    MinutosDeVida = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirmaJWT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetAplicacion_IdAplicacion",
                        column: x => x.IdAplicacion,
                        principalTable: "AspNetAplicacion",
                        principalColumn: "IdAplicacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
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
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.InsertData(
                table: "AspNetAlgoritmosDeSeguridad",
                columns: new[] { "Algoritmo", "Valor" },
                values: new object[,]
                {
                    { "EcdsaSha256", "ES256" },
                    { "RsaPKCS1", "RSA1_5" },
                    { "Sha512", "SHA512" },
                    { "Sha384", "SHA384" },
                    { "Sha256", "SHA256" },
                    { "RsaOAEP", "RSA-OAEP" },
                    { "Aes256KW", "A256KW" },
                    { "Aes128KW", "A128KW" },
                    { "Aes192CbcHmacSha384", "A192CBC-HS384" },
                    { "HmacSha256", "HS256" },
                    { "Aes128CbcHmacSha256", "A128CBC-HS256" },
                    { "RsaSsaPssSha512", "PS512" },
                    { "RsaSsaPssSha384", "PS384" },
                    { "RsaSsaPssSha256", "PS256" },
                    { "RsaSha512", "RS512" },
                    { "RsaSha384", "RS384" },
                    { "RsaSha256", "RS256" },
                    { "None", "none" },
                    { "HmacSha512", "HS512" },
                    { "HmacSha384", "HS384" },
                    { "EcdsaSha512", "ES512" },
                    { "EcdsaSha384", "ES384" },
                    { "Aes256CbcHmacSha512", "A256CBC-HS512" }
                });

            migrationBuilder.InsertData(
                table: "AspNetTiposAutenticacion",
                columns: new[] { "IdTipo", "Autenticacion", "EsDirectorioActivo", "IdAD" },
                values: new object[] { 1, "Usuario y Contraseña", false, null });

            migrationBuilder.InsertData(
                table: "AspNetAplicacion",
                columns: new[] { "IdAplicacion", "AlgoritmoDeSeguridad", "EmailContacto", "Estado", "EstadoLlave", "FechaExpiracionLlave", "LlaveSecreta", "MinutosDeVida", "Nombre", "PermiteJWT" },
                values: new object[] { "Manager", "HmacSha512", "michavarg9@gmail.com", true, true, new DateTime(2023, 5, 23, 10, 1, 17, 707, DateTimeKind.Local).AddTicks(9094), "Secret_Key", 60.0, "Account Service", true });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellidos", "Cargo", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "EsExterno", "IdTipoAuth", "Identificacion", "LockoutEnabled", "LockoutEnd", "Nombres", "NormalizedEmail", "NormalizedUserName", "Organizacion", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "", "Administrador del sistema", "adfe54ba-b549-4839-8ce5-81ec1f4a1aec", "Administrador del sistema de autenticación", "admin@admin.org.co", true, false, 1, null, false, null, "Admin", "ADMIN@ADMIN.ORG.CO", "ADMIN", "PUJ", "AQAAAAEAACcQAAAAEDvsJrU5P2uO7jfhKVQTK2rMCwYlOAoWC3AzIGB+iktmo8A2515Utzul5+KXfWEjqQ==", null, false, "XVMFBE37LCN4TNGMZSHLPHBV7FIVHBQG", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Descripcion", "Display", "IdAplicacion", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "2f98f861-d2e4-43a2-ac09-f1e605335952", "Usuario con permisos Full", "PowerUser", "Manager", "PowerUser", "POWERUSER" },
                    { 2, "a3307ea4-077d-48e9-a5d9-db5b16d2a719", "Usuario con permisos de Admin", "Administrador", "Manager", "Administrador", "ADMINISTRADOR" },
                    { 3, "67ebce17-09b4-4d0f-ad24-8b7085ed4ea9", "Cliente del sistema", "Cliente", "Manager", "Cliente", "CLIENTE" },
                    { 4, "dd5a7d54-a1d2-406e-b8e3-e945970ad591", "Proveedor del sistema", "Proveedor", "Manager", "Proveedor", "PROVEEDOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "Usuario y Contraseña", 1 },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress", "", 1 },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri", "", 1 },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", "Manager", 1 },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", "Manager", 2 },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", "Manager", 3 },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", "Manager", 4 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetAplicacion_AlgoritmoDeSeguridad",
                table: "AspNetAplicacion",
                column: "AlgoritmoDeSeguridad");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_IdAplicacion",
                table: "AspNetRoles",
                column: "IdAplicacion");

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
                name: "IX_AspNetUsers_IdTipoAuth",
                table: "AspNetUsers",
                column: "IdTipoAuth");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_IdAplicacion",
                table: "AspNetUserTokens",
                column: "IdAplicacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_LogAutenticacionAPI");

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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetAplicacion");

            migrationBuilder.DropTable(
                name: "AspNetTiposAutenticacion");

            migrationBuilder.DropTable(
                name: "AspNetAlgoritmosDeSeguridad");
        }
    }
}
