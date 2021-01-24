namespace RetsExchange
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct)
    ]
    public class RetsResource : System.Attribute
    {
        public string Class = string.Empty;
        public string SearchType = string.Empty;
    }
}
