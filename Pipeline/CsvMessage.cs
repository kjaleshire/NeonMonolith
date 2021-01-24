using System.IO;

namespace NeonMonolith.Pipeline
{
    internal class CsvMessage
    {
        internal long SearchId { get; set; }
        internal long MinRecordId { get; set; }
        internal long MaxRecordId { get; set; }
        internal TextWriter CsvStringWriter { get; set; } = new StringWriter();
    }
}
