namespace SqlLibrary.Classes;
public class SqlStatements
{
    public static string DependenciesStatement =>
        """
        SELECT src.name AS "ParentTable",
               srcCol.name AS "ParentColumn",
               dst.name AS "ReferenceTable",
               dstCol.name AS "ReferenceColumn"
        FROM sys.foreign_key_columns fk
            INNER JOIN sys.columns srcCol
                ON fk.parent_column_id = srcCol.column_id
                   AND fk.parent_object_id = srcCol.object_id
            INNER JOIN sys.tables src
                ON src.object_id = fk.parent_object_id
            INNER JOIN sys.tables dst
                ON dst.object_id = fk.referenced_object_id
            INNER JOIN sys.columns dstCol
                ON fk.referenced_column_id = dstCol.column_id
                   AND fk.referenced_object_id = dstCol.object_id
        ORDER BY src.name
        """;
}