using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondHandMVC.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_Users_UserID",
                table: "Advertisement");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_Users_UserID1",
                table: "Advertisement");

            migrationBuilder.DropIndex(
                name: "IX_Advertisement_UserID",
                table: "Advertisement");

            migrationBuilder.DropIndex(
                name: "IX_Advertisement_UserID1",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "Advertisement");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisement",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Advertisement",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Advertisement",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "BuyerUserID",
                table: "Advertisement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SellerUserID",
                table: "Advertisement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_BuyerUserID",
                table: "Advertisement",
                column: "BuyerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_SellerUserID",
                table: "Advertisement",
                column: "SellerUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_Users_BuyerUserID",
                table: "Advertisement",
                column: "BuyerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_Users_SellerUserID",
                table: "Advertisement",
                column: "SellerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_Users_BuyerUserID",
                table: "Advertisement");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_Users_SellerUserID",
                table: "Advertisement");

            migrationBuilder.DropIndex(
                name: "IX_Advertisement_BuyerUserID",
                table: "Advertisement");

            migrationBuilder.DropIndex(
                name: "IX_Advertisement_SellerUserID",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "BuyerUserID",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "SellerUserID",
                table: "Advertisement");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisement",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Advertisement",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Advertisement",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Advertisement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID1",
                table: "Advertisement",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_UserID",
                table: "Advertisement",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_UserID1",
                table: "Advertisement",
                column: "UserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_Users_UserID",
                table: "Advertisement",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_Users_UserID1",
                table: "Advertisement",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
