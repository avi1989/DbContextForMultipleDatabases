using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MultiDbContext.Data.Conventions
{
    //Reference: https://andrewlock.net/customising-asp-net-core-identity-ef-core-naming-conventions-for-postgresql/
    public class SnakeCaseConverter : IDatabaseConventionConverter
    {
        public void SetConvention(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(ConvertToSnakeCase(entity.GetTableName()));
                // Replace table names

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(ConvertToSnakeCase(property.GetColumnName()));
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(ConvertToSnakeCase(key.GetName()));
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(ConvertToSnakeCase(key.GetConstraintName()));
                }

                foreach (var key in entity.GetIndexes())
                {
                    key.SetName(ConvertToSnakeCase(key.GetName()));
                }
            }
        }

        private string ConvertToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
