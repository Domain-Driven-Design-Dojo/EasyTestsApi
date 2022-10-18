using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddBaseEntityRelationsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AccGroups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatorId", "ModifierId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                table: "AccUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELOWEI9Oc6M53pTbwJUTVPd6f0+gZCRwDi7zS3kvrnzsqAam11gJpcoiTdOhOW+ORQ==");

            migrationBuilder.UpdateData(
                table: "CrePeopleTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatorId", "ModifierId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                table: "CrePeopleTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatorId", "ModifierId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_SysErrorTypes_CreatorId",
                table: "SysErrorTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SysErrorTypes_ModifierId",
                table: "SysErrorTypes",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_CrePeopleTypes_CreatorId",
                table: "CrePeopleTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CrePeopleTypes_ModifierId",
                table: "CrePeopleTypes",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_CrePeople_CreatorId",
                table: "CrePeople",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CrePeople_ModifierId",
                table: "CrePeople",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_AccGroupUsers_CreatorId",
                table: "AccGroupUsers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccGroupUsers_ModifierId",
                table: "AccGroupUsers",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_AccGroups_CreatorId",
                table: "AccGroups",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccGroups_ModifierId",
                table: "AccGroups",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_AccGroupRoles_CreatorId",
                table: "AccGroupRoles",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccGroupRoles_ModifierId",
                table: "AccGroupRoles",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccGroupRoles_AccUsers_CreatorId",
                table: "AccGroupRoles",
                column: "CreatorId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccGroupRoles_AccUsers_ModifierId",
                table: "AccGroupRoles",
                column: "ModifierId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccGroups_AccUsers_CreatorId",
                table: "AccGroups",
                column: "CreatorId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccGroups_AccUsers_ModifierId",
                table: "AccGroups",
                column: "ModifierId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccGroupUsers_AccUsers_CreatorId",
                table: "AccGroupUsers",
                column: "CreatorId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccGroupUsers_AccUsers_ModifierId",
                table: "AccGroupUsers",
                column: "ModifierId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrePeople_AccUsers_CreatorId",
                table: "CrePeople",
                column: "CreatorId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrePeople_AccUsers_ModifierId",
                table: "CrePeople",
                column: "ModifierId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrePeopleTypes_AccUsers_CreatorId",
                table: "CrePeopleTypes",
                column: "CreatorId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrePeopleTypes_AccUsers_ModifierId",
                table: "CrePeopleTypes",
                column: "ModifierId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysErrorTypes_AccUsers_CreatorId",
                table: "SysErrorTypes",
                column: "CreatorId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysErrorTypes_AccUsers_ModifierId",
                table: "SysErrorTypes",
                column: "ModifierId",
                principalTable: "AccUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccGroupRoles_AccUsers_CreatorId",
                table: "AccGroupRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccGroupRoles_AccUsers_ModifierId",
                table: "AccGroupRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccGroups_AccUsers_CreatorId",
                table: "AccGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AccGroups_AccUsers_ModifierId",
                table: "AccGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_AccGroupUsers_AccUsers_CreatorId",
                table: "AccGroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AccGroupUsers_AccUsers_ModifierId",
                table: "AccGroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CrePeople_AccUsers_CreatorId",
                table: "CrePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_CrePeople_AccUsers_ModifierId",
                table: "CrePeople");

            migrationBuilder.DropForeignKey(
                name: "FK_CrePeopleTypes_AccUsers_CreatorId",
                table: "CrePeopleTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CrePeopleTypes_AccUsers_ModifierId",
                table: "CrePeopleTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_SysErrorTypes_AccUsers_CreatorId",
                table: "SysErrorTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_SysErrorTypes_AccUsers_ModifierId",
                table: "SysErrorTypes");

            migrationBuilder.DropIndex(
                name: "IX_SysErrorTypes_CreatorId",
                table: "SysErrorTypes");

            migrationBuilder.DropIndex(
                name: "IX_SysErrorTypes_ModifierId",
                table: "SysErrorTypes");

            migrationBuilder.DropIndex(
                name: "IX_CrePeopleTypes_CreatorId",
                table: "CrePeopleTypes");

            migrationBuilder.DropIndex(
                name: "IX_CrePeopleTypes_ModifierId",
                table: "CrePeopleTypes");

            migrationBuilder.DropIndex(
                name: "IX_CrePeople_CreatorId",
                table: "CrePeople");

            migrationBuilder.DropIndex(
                name: "IX_CrePeople_ModifierId",
                table: "CrePeople");

            migrationBuilder.DropIndex(
                name: "IX_AccGroupUsers_CreatorId",
                table: "AccGroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_AccGroupUsers_ModifierId",
                table: "AccGroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_AccGroups_CreatorId",
                table: "AccGroups");

            migrationBuilder.DropIndex(
                name: "IX_AccGroups_ModifierId",
                table: "AccGroups");

            migrationBuilder.DropIndex(
                name: "IX_AccGroupRoles_CreatorId",
                table: "AccGroupRoles");

            migrationBuilder.DropIndex(
                name: "IX_AccGroupRoles_ModifierId",
                table: "AccGroupRoles");

            migrationBuilder.UpdateData(
                table: "AccGroups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatorId", "ModifierId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                table: "AccUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEDfEmt5lTB3ZSu0s2oBovId9paD120eNpego6TNzOjrOrIfS4QAs6TCnJb04wsjPsw==");

            migrationBuilder.UpdateData(
                table: "CrePeopleTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatorId", "ModifierId" },
                values: new object[] { 2L, 2L });

            migrationBuilder.UpdateData(
                table: "CrePeopleTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatorId", "ModifierId" },
                values: new object[] { 2L, 2L });
        }
    }
}
