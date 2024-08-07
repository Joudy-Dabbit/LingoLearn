using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoLearn.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_Question_and_answer_in_context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeAnswer_ChallengeQuestion_QuestionId",
                table: "ChallengeAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeQuestion_Challenges_ChallengeId",
                table: "ChallengeQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Levels_LevelId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengeQuestion",
                table: "ChallengeQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengeAnswer",
                table: "ChallengeAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "ChallengeQuestion",
                newName: "ChallengeQuestions");

            migrationBuilder.RenameTable(
                name: "ChallengeAnswer",
                newName: "ChallengeAnswers");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_Question_LevelId",
                table: "Questions",
                newName: "IX_Questions_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ChallengeQuestion_ChallengeId",
                table: "ChallengeQuestions",
                newName: "IX_ChallengeQuestions_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_ChallengeAnswer_QuestionId",
                table: "ChallengeAnswers",
                newName: "IX_ChallengeAnswers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengeQuestions",
                table: "ChallengeQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengeAnswers",
                table: "ChallengeAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeAnswers_ChallengeQuestions_QuestionId",
                table: "ChallengeAnswers",
                column: "QuestionId",
                principalTable: "ChallengeQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeQuestions_Challenges_ChallengeId",
                table: "ChallengeQuestions",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Levels_LevelId",
                table: "Questions",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeAnswers_ChallengeQuestions_QuestionId",
                table: "ChallengeAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeQuestions_Challenges_ChallengeId",
                table: "ChallengeQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Levels_LevelId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengeQuestions",
                table: "ChallengeQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChallengeAnswers",
                table: "ChallengeAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "ChallengeQuestions",
                newName: "ChallengeQuestion");

            migrationBuilder.RenameTable(
                name: "ChallengeAnswers",
                newName: "ChallengeAnswer");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_LevelId",
                table: "Question",
                newName: "IX_Question_LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_ChallengeQuestions_ChallengeId",
                table: "ChallengeQuestion",
                newName: "IX_ChallengeQuestion_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_ChallengeAnswers_QuestionId",
                table: "ChallengeAnswer",
                newName: "IX_ChallengeAnswer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengeQuestion",
                table: "ChallengeQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChallengeAnswer",
                table: "ChallengeAnswer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Question_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeAnswer_ChallengeQuestion_QuestionId",
                table: "ChallengeAnswer",
                column: "QuestionId",
                principalTable: "ChallengeQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeQuestion_Challenges_ChallengeId",
                table: "ChallengeQuestion",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Levels_LevelId",
                table: "Question",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
