namespace NeonMonolith
{
    internal class AppConfig
    {
        public bool DO_CURRENT_LISTINGS_BACKFILL { get; set; }
        public bool DO_OLDER_LISTINGS_BACKFILL { get; set; }
        public bool DO_CURRENT_LISTINGS_PATCH { get; set; }
        public bool DO_OLDER_LISTINGS_PATCH { get; set; }
        public bool DO_ENTIRE_BACKFILL { get; set; }
        public string RETS_ENDPOINT { get; set; } = string.Empty;
        public string RETS_USERNAME { get; set; } = string.Empty;
        public string RETS_PASSWORD { get; set; } = string.Empty;
        public string DROPBOX_TOKEN { get; set; } = string.Empty;
        public string DROPBOX_ROOT { get; set; } = string.Empty;
        public string LOCAL_DESTINATION { get; set; } = string.Empty;
        public string CURRENT_LISTING_PATH { get; set; } = string.Empty;
        public string OLDER_LISTING_PATH { get; set; } = string.Empty;
        public string CSV_FILENAME_FORMAT { get; set; } = string.Empty;
        public int RUN_PERIOD_HOURS { get; set; }
        public int PATCH_RANGE_HOURS { get; set; }
        public string DATABASE_STRING { get; set; } = string.Empty;
        public const int MaxRecords = 5000; // Max number of records ABOR Rets will send
    }
}
