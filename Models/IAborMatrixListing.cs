using System;

namespace NeonMonolith.Models
{
    internal interface IAborMatrixListing
    {
        // This is always the primary key
        long Matrix_Unique_ID { get; set; }

        // There must always be a modified date to fetch new results against
        DateTime MatrixModifiedDT { get; set; }
    }
}
