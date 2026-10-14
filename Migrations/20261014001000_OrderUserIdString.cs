using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ShopFlowAPI.Data;

#nullable disable

namespace ShopFlowAPI.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20261014001000_OrderUserIdString")]
public partial class OrderUserIdString : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """ALTER TABLE "order" ALTER COLUMN user_id TYPE text USING user_id::text;""");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            DO $$
            BEGIN
                IF EXISTS (
                    SELECT 1 FROM "order"
                    WHERE user_id !~ '^[0-9]+$'
                ) THEN
                    RAISE EXCEPTION 'Cannot convert order user IDs back to integers.';
                END IF;
            END $$;
            ALTER TABLE "order" ALTER COLUMN user_id TYPE integer USING user_id::integer;
            """);
    }
}
