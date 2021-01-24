using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RetsExchange;

namespace NeonMonolith.Models
{
    [RetsResource(Class = "RESI", SearchType = "Lease")]
    [Table("residential_leases")]
    internal class ResidentialLease : IAborMatrixListing
    {
        [Column("ac")]
        public string? AC { get; set; }
        [Column("access_instruction")]
        public string? AccessInstruction { get; set; }
        [Column("active_open_house_count")]
        public int? ActiveOpenHouseCount { get; set; }
        [Column("additional_pet_fee")]
        public int? AdditionalPetFee { get; set; }
        [Column("additional_pet_fee_descript")]
        public string? AdditionalPetFeeDescript { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("address_internet")]
        public string? AddressInternet { get; set; }
        [Column("admin_fee_payble_to")]
        public string? AdminFeePaybleTo { get; set; }
        [Column("administration_fee")]
        public int? AdministrationFee { get; set; }
        [Column("allow_3rd_party_comments")]
        public bool? Allow3rdPartyComments { get; set; }
        [Column("allow_automated_valuations")]
        public bool? AllowAutomatedValuations { get; set; }
        [Column("also_listed_as")]
        public string? AlsoListedAs { get; set; }
        [Column("app_delivery_to")]
        public string? AppDeliveryTo { get; set; }
        [Column("app_deposit_payable_to")]
        public string? AppDepositPayableTo { get; set; }
        [Column("app_fee_payable_to")]
        public string? AppFeePayableTo { get; set; }
        [Column("appliances_maintained")]
        public string? AppliancesMaintained { get; set; }
        [Column("application_fee")]
        public int? ApplicationFee { get; set; }
        [Column("application_policy")]
        public string? ApplicationPolicy { get; set; }
        [Column("application_required")]
        public string? ApplicationRequired { get; set; }
        [Column("application_requirements")]
        public string? ApplicationRequirements { get; set; }
        [Column("area")]
        public string? Area { get; set; }
        [Column("area_amenities")]
        public string? AreaAmenities { get; set; }
        [Column("association_fee")]
        public int? AssociationFee { get; set; }
        [Column("association_fee_frequency")]
        public string? AssociationFeeFrequency { get; set; }
        [Column("back_on_market_date")]
        public DateTime? BackOnMarketDate { get; set; }
        [Column("baths_full")]
        public int? BathsFull { get; set; }
        [Column("baths_half")]
        public int? BathsHalf { get; set; }
        [Column("baths_total")]
        public int? BathsTotal { get; set; }
        [Column("beds_total")]
        public int? BedsTotal { get; set; }
        [Column("body_of_water")]
        public string? BodyofWater { get; set; }
        [Column("cdom")]
        public int? CDOM { get; set; }
        [Column("cdom_change_notes")]
        public string? CDOM_ChangeNotes { get; set; }
        [Column("cdom_initial")]
        public int? CDOM_Initial { get; set; }
        [Column("cdom_seed_address_match")]
        public bool? CDOM_Seed_Address_Match { get; set; }
        [Column("cdom_seed_apn_match")]
        public bool? CDOM_Seed_APN_Match { get; set; }
        [Column("cdom_seed_datetime")]
        public DateTime? CDOM_Seed_Datetime { get; set; }
        [Column("cdom_seed_listing_mui")]
        public long? CDOM_Seed_Listing_MUI { get; set; }
        [Column("cdom_seed_ml_number")]
        public string? CDOM_Seed_MLNumber { get; set; }
        [Column("ceiling_fan_location")]
        public string? CeilingFanLocation { get; set; }
        [Column("ceiling_fan_yn")]
        public bool? CeilingFanYN { get; set; }
        [Column("certified_funds")]
        public bool? CertifiedFunds { get; set; }
        [Column("city")]
        public string? City { get; set; }
        [Column("close_date")]
        public DateTime? CloseDate { get; set; }
        [Column("close_price")]
        public Decimal? ClosePrice { get; set; }
        [Column("co_list_agent_mui")]
        public long? CoListAgent_MUI { get; set; }
        [Column("co_list_agent_direct_work_phone")]
        public string? CoListAgentDirectWorkPhone { get; set; }
        [Column("co_list_agent_full_name")]
        public string? CoListAgentFullName { get; set; }
        [Column("co_list_agent_mls_id")]
        public string? CoListAgentMLSID { get; set; }
        [Column("co_list_office_mui")]
        public long? CoListOffice_MUI { get; set; }
        [Column("co_list_office_mls_id")]
        public string? CoListOfficeMLSID { get; set; }
        [Column("co_list_office_name")]
        public string? CoListOfficeName { get; set; }
        [Column("co_list_office_phone")]
        public string? CoListOfficePhone { get; set; }
        [Column("community_web_site")]
        public string? CommunityWebSite { get; set; }
        [Column("complex_name")]
        public string? ComplexName { get; set; }
        [Column("conditional_date")]
        public DateTime? ConditionalDate { get; set; }
        [Column("construction")]
        public string? Construction { get; set; }
        [Column("co_selling_agent_mui")]
        public long? CoSellingAgent_MUI { get; set; }
        [Column("co_selling_agent_direct_work_phone")]
        public string? CoSellingAgentDirectWorkPhone { get; set; }
        [Column("co_selling_agent_full_name")]
        public string? CoSellingAgentFullName { get; set; }
        [Column("co_selling_agent_mls_id")]
        public string? CoSellingAgentMLSID { get; set; }
        [Column("co_selling_office_mui")]
        public long? CoSellingOffice_MUI { get; set; }
        [Column("co_selling_office_mls_id")]
        public string? CoSellingOfficeMLSID { get; set; }
        [Column("co_selling_office_name")]
        public string? CoSellingOfficeName { get; set; }
        [Column("co_selling_office_phone")]
        public string? CoSellingOfficePhone { get; set; }
        [Column("country")]
        public string? Country { get; set; }
        [Column("county_or_parish")]
        public string? CountyOrParish { get; set; }
        [Column("covered_spaces")]
        public int? CoveredSpaces { get; set; }
        [Column("create_automatic_virtual_tour_yn")]
        public bool? CreateAutomaticVirtualTourYN { get; set; }
        [Column("current_price")]
        public Decimal? CurrentPrice { get; set; }
        [Column("date_possession")]
        public DateTime? DatePossession { get; set; }
        [Column("dining_description")]
        public string? DiningDescription { get; set; }
        [Column("directions")]
        public string? Directions { get; set; }
        [Column("disability_features")]
        public bool? DisabilityFeatures { get; set; }
        [Column("disability_features_desc")]
        public string? DisabilityFeaturesDesc { get; set; }
        [Column("distance_to_light_rail")]
        public string? DistancetoLightRail { get; set; }
        [Column("distance_to_metro")]
        public string? DistancetoMetro { get; set; }
        [Column("distance_to_ut_shuttle")]
        public string? DistanceToUTShuttle { get; set; }
        [Column("distance_to_water_access")]
        public string? DistanceToWaterAccess { get; set; }
        [Column("dom")]
        public int? DOM { get; set; }
        [Column("down_payment_resource_yn")]
        public bool? DownPaymentResourceYN { get; set; }
        [Column("dryer")]
        public string? Dryer { get; set; }
        [Column("dryer_maintained")]
        public bool? DryerMaintained { get; set; }
        [Column("ees")]
        public bool? EES { get; set; }
        [Column("ees_features")]
        public string? EESFeatures { get; set; }
        [Column("elem_a_other")]
        public string? ElemAOther { get; set; }
        [Column("elementary_a")]
        public string? ElementaryA { get; set; }
        [Column("expiration_date")]
        public DateTime? ExpirationDate { get; set; }
        [Column("exterior_features")]
        public string? ExteriorFeatures { get; set; }
        [Column("faces")]
        public string? Faces { get; set; }
        [Column("fee_description")]
        public string? FeeDescription { get; set; }
        [Column("fema_100_yr_flood_plain")]
        public string? FEMA100YrFloodPlain { get; set; }
        [Column("fence")]
        public string? Fence { get; set; }
        [Column("fence_yn")]
        public bool? FenceYN { get; set; }
        [Column("fireplace_features")]
        public string? FireplaceFeatures { get; set; }
        [Column("first_month_rent_payable_to")]
        public string? FirstMonthRentPayableTo { get; set; }
        [Column("flooring")]
        public string? Flooring { get; set; }
        [Column("floor_plan_name_number")]
        public string? FloorPlanNameNumber { get; set; }
        [Column("foundation_details")]
        public string? FoundationDetails { get; set; }
        [Column("furnished")]
        public string? Furnished { get; set; }
        [Column("gated_community")]
        public bool? GatedCommunity { get; set; }
        [Column("gr9_high_other")]
        public string? Gr9HighOther { get; set; }
        [Column("gr9_high_school")]
        public string? Gr9HighSchool { get; set; }
        [Column("green_rating_program")]
        public string? GreenRatingProgram { get; set; }
        [Column("heating")]
        public string? Heating { get; set; }
        [Column("hers_index")]
        public int? HERSIndex { get; set; }
        [Column("hers_year")]
        public int? HERSYear { get; set; }
        [Column("hoa_name")]
        public string? HOAName { get; set; }
        [Column("hoa_requirement")]
        public string? HOARequirement { get; set; }
        [Column("hoa_yn")]
        public bool? HOAYN { get; set; }
        [Column("horses")]
        public bool? Horses { get; set; }
        [Column("idx_opt_in_yn")]
        public bool? IDXOptInYN { get; set; }
        [Column("interior_features")]
        public string? InteriorFeatures { get; set; }
        [Column("invoice_submission")]
        public string? InvoiceSubmission { get; set; }
        [Column("is_deleted")]
        public bool? IsDeleted { get; set; }
        [Column("kitchen")]
        public string? Kitchen { get; set; }
        [Column("kitchen_appliances")]
        public string? KitchenAppliances { get; set; }
        [Column("land_sqft")]
        public Decimal? LandSQFT { get; set; }
        [Column("last_change_timestamp")]
        public DateTime? LastChangeTimestamp { get; set; }
        [Column("last_change_type")]
        public string? LastChangeType { get; set; }
        [Column("last_list_price")]
        public Decimal? LastListPrice { get; set; }
        [Column("last_status")]
        public string? LastStatus { get; set; }
        [Column("latitude")]
        public Decimal? Latitude { get; set; }
        [Column("laundry_facilities")]
        public string? LaundryFacilities { get; set; }
        [Column("lease_guarantor")]
        public bool? LeaseGuarantor { get; set; }
        [Column("legal_description")]
        public string? LegalDescription { get; set; }
        [Column("list_agent_mui")]
        public long? ListAgent_MUI { get; set; }
        [Column("list_agent_direct_work_phone")]
        public string? ListAgentDirectWorkPhone { get; set; }
        [Column("list_agent_full_name")]
        public string? ListAgentFullName { get; set; }
        [Column("list_agent_mls_id")]
        public string? ListAgentMLSID { get; set; }
        [Column("listing_agent_fax")]
        public string? ListingAgentFax { get; set; }
        [Column("listing_contract_date")]
        public DateTime? ListingContractDate { get; set; }
        [Column("listing_will_appear_on")]
        public string? ListingWillAppearOn { get; set; }
        [Column("list_office_mui")]
        public long? ListOffice_MUI { get; set; }
        [Column("list_office_mls_id")]
        public string? ListOfficeMLSID { get; set; }
        [Column("list_office_name")]
        public string? ListOfficeName { get; set; }
        [Column("list_office_phone")]
        public string? ListOfficePhone { get; set; }
        [Column("list_price")]
        public Decimal? ListPrice { get; set; }
        [Column("lockbox_location")]
        public string? LockboxLocation { get; set; }
        [Column("lockbox_type")]
        public string? LockboxType { get; set; }
        [Column("longitude")]
        public Decimal? Longitude { get; set; }
        [Column("lot_features")]
        public string? LotFeatures { get; set; }
        [Column("lot_size_area")]
        public Decimal? LotSizeArea { get; set; }
        [Column("lot_size_dimensions")]
        public string? LotSizeDimensions { get; set; }
        [Column("lse_length_negotiable")]
        public string? LseLengthNegotiable { get; set; }
        [Column("managed_by")]
        public string? ManagedBy { get; set; }
        [Key]
        [Column("matrix_unique_id")]
        public long Matrix_Unique_ID { get; set; }
        [Column("matrix_modified_dt")]
        public DateTime MatrixModifiedDT { get; set; }
        [Column("mapsco_grid")]
        public string? MAPSCOGrid { get; set; }
        [Column("mapsco_page")]
        public string? MAPSCOPage { get; set; }
        [Column("master_description")]
        public string? MasterDescription { get; set; }
        [Column("master_main")]
        public bool? MasterMain { get; set; }
        [Column("max_lease_months")]
        public int? MaxLeaseMonths { get; set; }
        [Column("max_numof_pets")]
        public int? MaxNumofPets { get; set; }
        [Column("member_office_phone_ext")]
        public string? MemberOfficePhoneExt { get; set; }
        [Column("meter_description")]
        public string? MeterDescription { get; set; }
        [Column("mgmt_company")]
        public string? MgmtCompany { get; set; }
        [Column("middle_intermediate_school")]
        public string? MiddleIntermediateSchool { get; set; }
        [Column("middle_other")]
        public string? MiddleOther { get; set; }
        [Column("min_lease_months")]
        public int? MinLeaseMonths { get; set; }
        [Column("ml_num_to_be_copied")]
        public string? MLNumtobeCopied { get; set; }
        [Column("mls")]
        public string? MLS { get; set; }
        [Column("mls_number")]
        public string? MLSNumber { get; set; }
        [Column("monthly_pet_rent")]
        public int? MonthlyPetRent { get; set; }
        [Column("monthly_pet_rent_per_pet_yn")]
        public bool? MonthlyPetRentPerPetYN { get; set; }
        [Column("move_in_requirements")]
        public string? MoveInRequirements { get; set; }
        [Column("move_in_special_description")]
        public string? MoveInSpecialDescription { get; set; }
        [Column("move_in_special_yn")]
        public bool? MoveInSpecialYN { get; set; }
        [Column("number_of_units_in_complex")]
        public int? NumberOfUnitsInComplex { get; set; }
        [Column("num_blocks_to_metro_lightrail")]
        public int? NumBlockstoMetroLightrail { get; set; }
        [Column("num_blocks_to_ut_shuttle")]
        public int? NumBlockstoUTShuttle { get; set; }
        [Column("num_dining")]
        public int? NumDining { get; set; }
        [Column("num_fireplaces")]
        public int? NumFireplaces { get; set; }
        [Column("num_horses_allowed")]
        public int? NumHorsesAllowed { get; set; }
        [Column("num_living")]
        public int? NumLiving { get; set; }
        [Column("num_main_level_beds")]
        public int? NumMainLevelBeds { get; set; }
        [Column("num_other_level_beds")]
        public int? NumOtherLevelBeds { get; set; }
        [Column("num_parking_spaces")]
        public int? NumParkingSpaces { get; set; }
        [Column("occupant_name")]
        public string? OccupantName { get; set; }
        [Column("occupant_phone")]
        public string? OccupantPhone { get; set; }
        [Column("occupant_phone_other")]
        public string? OccupantPhoneOther { get; set; }
        [Column("occupant_type")]
        public string? OccupantType { get; set; }
        [Column("off_market_date")]
        public DateTime? OffMarketDate { get; set; }
        [Column("one_time_expenses")]
        public string? OneTimeExpenses { get; set; }
        [Column("online_app_instructions")]
        public string? OnlineAppInstructions { get; set; }
        [Column("online_app_instructionspublic")]
        public string? OnlineAppInstructionspublic { get; set; }
        [Column("on_site_compliance_yn")]
        public bool? OnSiteComplianceYN { get; set; }
        [Column("open_house_count")]
        public int? OpenHouseCount { get; set; }
        [Column("open_house_date_public")]
        public DateTime? OpenHouseDatePublic { get; set; }
        [Column("open_house_time_public")]
        public string? OpenHouseTimePublic { get; set; }
        [Column("open_house_upcoming")]
        public string? OpenHouseUpcoming { get; set; }
        [Column("original_entry_timestamp")]
        public DateTime? OriginalEntryTimestamp { get; set; }
        [Column("original_list_price")]
        public Decimal? OriginalListPrice { get; set; }
        [Column("outdoor_living_features")]
        public string? OutdoorLivingFeatures { get; set; }
        [Column("out_of_area_city")]
        public string? OutofAreaCity { get; set; }
        [Column("out_of_area_county")]
        public string? OutofAreaCounty { get; set; }
        [Column("out_of_area_school_district")]
        public string? OutofAreaSchoolDistrict { get; set; }
        [Column("owner_name")]
        public string? OwnerName { get; set; }
        [Column("owner_pays")]
        public string? OwnerPays { get; set; }
        [Column("owner_phone")]
        public string? OwnerPhone { get; set; }
        [Column("parcel_number")]
        public string? ParcelNumber { get; set; }
        [Column("parking_features")]
        public string? ParkingFeatures { get; set; }
        [Column("parking_fee_frequency")]
        public string? ParkingFeeFrequency { get; set; }
        [Column("parking_fee_from")]
        public int? ParkingFeeFrom { get; set; }
        [Column("parking_fee_to")]
        public int? ParkingFeeTo { get; set; }
        [Column("pending_date")]
        public DateTime? PendingDate { get; set; }
        [Column("permit_internet_yn")]
        public bool? PermitInternetYN { get; set; }
        [Column("per_person")]
        public bool? PerPerson { get; set; }
        [Column("per_pet")]
        public bool? PerPet { get; set; }
        [Column("pet_deposit")]
        public int? PetDeposit { get; set; }
        [Column("pet_description")]
        public string? PetDescription { get; set; }
        [Column("pets")]
        public bool? Pets { get; set; }
        [Column("photo_count")]
        public int? PhotoCount { get; set; }
        [Column("photo_exist")]
        public string? PhotoExist { get; set; }
        [Column("photo_modification_timestamp")]
        public DateTime? PhotoModificationTimestamp { get; set; }
        [Column("pool_descr_on_prop")]
        public string? PoolDescronProp { get; set; }
        [Column("pool_on_property")]
        public bool? PoolonProperty { get; set; }
        [Column("postal_code")]
        public string? PostalCode { get; set; }
        [Column("postal_code_plus4")]
        public string? PostalCodePlus4 { get; set; }
        [Column("price_change_timestamp")]
        public DateTime? PriceChangeTimestamp { get; set; }
        [Column("private_remarks")]
        public string? PrivateRemarks { get; set; }
        [Column("property_address_on_internet")]
        public bool? PropertyAddressonInternet { get; set; }
        [Column("property_sub_type")]
        public string? PropertySubType { get; set; }
        [Column("property_type")]
        public string? PropertyType { get; set; }
        [Column("provider_key")]
        public int? ProviderKey { get; set; }
        [Column("public_remarks")]
        public string? PublicRemarks { get; set; }
        [Column("range")]
        public string? Range { get; set; }
        [Column("rating_achieved")]
        public string? RatingAchieved { get; set; }
        [Column("rating_year")]
        public int? RatingYear { get; set; }
        [Column("ratio_close_price_by_list_price")]
        public Decimal? RATIO_ClosePrice_By_ListPrice { get; set; }
        [Column("ratio_close_price_by_original_list_price")]
        public Decimal? RATIO_ClosePrice_By_OriginalListPrice { get; set; }
        [Column("ratio_close_price_by_sqft")]
        public Decimal? RATIO_ClosePrice_By_SQFT { get; set; }
        [Column("ratio_current_price_by_sqft")]
        public Decimal? RATIO_CurrentPrice_By_SQFT { get; set; }
        [Column("ratio_list_price_by_sqft")]
        public Decimal? RATIO_ListPrice_By_SQFT { get; set; }
        [Column("recurring_expenses")]
        public string? RecurringExpenses { get; set; }
        [Column("refrigerator")]
        public bool? Refrigerator { get; set; }
        [Column("refrigerator_maintained")]
        public bool? RefrigeratorMaintained { get; set; }
        [Column("region")]
        public string? Region { get; set; }
        [Column("renters_insurance_required_yn")]
        public bool? RentersInsuranceRequiredYN { get; set; }
        [Column("reqd_doc_agent_infor")]
        public string? ReqdDocAgentInfor { get; set; }
        [Column("roof")]
        public string? Roof { get; set; }
        [Column("rooms")]
        public string? Rooms { get; set; }
        [Column("school_district")]
        public string? SchoolDistrict { get; set; }
        [Column("search_expire_date")]
        public DateTime? SearchExpireDate { get; set; }
        [Column("section8")]
        public bool? Section8 { get; set; }
        [Column("security_deposit")]
        public int? SecurityDeposit { get; set; }
        [Column("selling_agent_mui")]
        public long? SellingAgent_MUI { get; set; }
        [Column("selling_agent_direct_work_phone")]
        public string? SellingAgentDirectWorkPhone { get; set; }
        [Column("selling_agent_full_name")]
        public string? SellingAgentFullName { get; set; }
        [Column("selling_agent_mls_id")]
        public string? SellingAgentMLSID { get; set; }
        [Column("selling_office_mui")]
        public long? SellingOffice_MUI { get; set; }
        [Column("selling_office_mls_id")]
        public string? SellingOfficeMLSID { get; set; }
        [Column("selling_office_name")]
        public string? SellingOfficeName { get; set; }
        [Column("selling_office_phone")]
        public string? SellingOfficePhone { get; set; }
        [Column("senior_high_school")]
        public string? SeniorHighSchool { get; set; }
        [Column("sewer")]
        public string? Sewer { get; set; }
        [Column("short_term_premium_amount")]
        public Decimal? ShortTermPremiumAmount { get; set; }
        [Column("short_term_premium_yn")]
        public bool? ShortTermPremiumYN { get; set; }
        [Column("smoking_inside")]
        public bool? SmokingInside { get; set; }
        [Column("sold_leased_comments")]
        public string? SoldLeasedComments { get; set; }
        [Column("sold_terms")]
        public string? SoldTerms { get; set; }
        [Column("spa_hot_tub_description")]
        public string? SpaHotTubDescription { get; set; }
        [Column("spa_hot_tub_yn")]
        public bool? SpaHotTubYN { get; set; }
        [Column("sprinkler_system")]
        public bool? SprinklerSystem { get; set; }
        [Column("sqft_total")]
        public int? SqftTotal { get; set; }
        [Column("square_foot_source")]
        public string? SquareFootSource { get; set; }
        [Column("sr_high_other")]
        public string? SrHighOther { get; set; }
        [Column("state_or_province")]
        public string? StateOrProvince { get; set; }
        [Column("status")]
        public string? Status { get; set; }
        [Column("status_change_timestamp")]
        public DateTime? StatusChangeTimestamp { get; set; }
        [Column("status_contractual_search_date")]
        public DateTime? StatusContractualSearchDate { get; set; }
        [Column("steps")]
        public string? Steps { get; set; }
        [Column("stories_lookup")]
        public string? StoriesLookup { get; set; }
        [Column("street_dir_prefix")]
        public string? StreetDirPrefix { get; set; }
        [Column("street_dir_suffix")]
        public string? StreetDirSuffix { get; set; }
        [Column("street_name")]
        public string? StreetName { get; set; }
        [Column("street_number")]
        public string? StreetNumber { get; set; }
        [Column("street_suffix")]
        public string? StreetSuffix { get; set; }
        [Column("subdivision_name")]
        public string? SubdivisionName { get; set; }
        [Column("tax_block")]
        public string? TaxBlock { get; set; }
        [Column("tax_filled_sqft_total")]
        public int? TaxFilledSqftTotal { get; set; }
        [Column("tax_lot")]
        public string? TaxLot { get; set; }
        [Column("temp_off_market_date")]
        public DateTime? TempOffMarketDate { get; set; }
        [Column("tenant_pays")]
        public string? TenantPays { get; set; }
        [Column("tentative_close_date")]
        public DateTime? TentativeCloseDate { get; set; }
        [Column("transaction_type")]
        public string? TransactionType { get; set; }
        [Column("trees")]
        public string? Trees { get; set; }
        [Column("unit_count")]
        public int? UnitCount { get; set; }
        [Column("unit_number")]
        public string? UnitNumber { get; set; }
        [Column("unit_style")]
        public string? UnitStyle { get; set; }
        [Column("upgraded_ee_features")]
        public string? UpgradedEEFeatures { get; set; }
        [Column("upgraded_energy_efficient")]
        public bool? UpgradedEnergyEfficient { get; set; }
        [Column("utilities")]
        public string? Utilities { get; set; }
        [Column("video_tour_link_branded")]
        public string? VideoTourLinkBranded { get; set; }
        [Column("video_tour_link_unbranded")]
        public string? VideoTourLinkUnbranded { get; set; }
        [Column("view")]
        public string? View { get; set; }
        [Column("virtual_tour_link_branded")]
        public string? VirtualTourLinkBranded { get; set; }
        [Column("virtual_tour_url_unbranded")]
        public string? VirtualTourURLUnbranded { get; set; }
        [Column("washer")]
        public string? Washer { get; set; }
        [Column("washer_maintained")]
        public bool? WasherMaintained { get; set; }
        [Column("water_access")]
        public bool? WaterAccess { get; set; }
        [Column("water_access_description")]
        public string? WaterAccessDescription { get; set; }
        [Column("waterfront")]
        public bool? Waterfront { get; set; }
        [Column("waterfront_description")]
        public string? WaterfrontDescription { get; set; }
        [Column("water_source")]
        public string? WaterSource { get; set; }
        [Column("withdrawn_date")]
        public DateTime? WithdrawnDate { get; set; }
        [Column("year_built")]
        public int? YearBuilt { get; set; }
        [Column("year_built_exception")]
        public string? YearBuiltException { get; set; }
    }
}
