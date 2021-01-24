using System.Collections.Generic;

namespace NeonMonolith.Pipeline
{
    internal class ListingMessage<RetsType>
    {
        internal long SearchId { get; set; }
        internal IEnumerable<RetsType> Records { get; set; }

        internal ListingMessage(IEnumerable<RetsType> records) =>
            Records = records;
    }
}
