# Neon Monolith

A MLS to CSV exporter including a hand-written C#-native RETS implementation.

## How to build

`docker build -t neonmonolith .`

## How to run

First, set these configuration environment variables:

`NEON_RETS_ENDPOINT` - The endpoint for the MLS database.

`NEON_RETS_USERNAME` - The username for the MLS login.

`NEON_RETS_PASSWORD` - The password for the MLS login.

`NEON_DROPBOX_TOKEN` - The Dropbox token required.

`NEON_DATABASE_STRING` - The [connection string](https://www.connectionstrings.com/postgresql/) for the database to use.

The default run mode is to scrape the entire listing table (full backfill). You can use these other environment variables to control behavior:

`NEON_LOCAL_DESTINATION` - Local output for testing (default `output/`)

`NEON_CURRENT_LISTING_PATH` - directory to place new listing CSVs into (default `NeonMonolith/`)
`NEON_OLDER_LISTING_PATH` - directory to place already-process listing CSVs into (default `CrimsonFalcon/`)

`NEON_CSV_FILENAME_FORMAT` - filename format for listing CSVs (default `RETS_{0}_{1}-{2}.csv`, where `{0}` is replace with a unique search ID, `{1}` by the minimum listing ID, and `{1}` by the maximum listing ID).

`NEON_RUN_PERIOD_HOURS` - How many hours elapse between runs.

`NEON_PATCH_RANGE_HOURS` - How many hours in the past to query for records (default `48`). For example, you can set the program to sync once per hour, and pick up listings that have changed up to 2 hours in the past by setting this variable to `2`.

*To control what listings are queried and emitted, one or more of the following must be set to `true`:*

`NEON_DO_CURRENT_LISTINGS_BACKFILL` - Query and emit listings marked as Active or Pending.

`NEON_DO_OLDER_LISTINGS_BACKFILL` - Query and emit listings marked as Expired, Withdrawn or Sold.

`NEON_DO_CURRENT_LISTINGS_PATCH` - Query and emit listings marked as Active or Pending for the `NEON_PATCH_RANGE_HOURS` period.

`NEON_DO_OLDER_LISTINGS_PATCH` - Query and emit listings marked as Expired, Withdrawn or Sold for the `NEON_PATCH_RANGE_HOURS` period.

`NEON_DO_ENTIRE_BACKFILL` - Query and emit all listings.
