//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using Ninwa_Employee.Data;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.Data.SqlClient;
//using Ninwa_Employee.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;


//namespace Ninwa_Employee.Pages.Admin
//{

//    public class Calculator_PointsModel : PageModel
//    {
//        private readonly IConfiguration configuration;
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<IdentityUser> _userManager;

//        public Calculator_PointsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
//        {
//            this.configuration = configuration;
//            _context = context;
//            _userManager = userManager;
//        }
//        [BindProperty]
//        public Tbluserdatum Tbluserdatum { get; set; } = default!;


//        public List<dynamic> UserPoints { get; set; }
//        public int TotalRecords { get; set; } = 0;
//        public int PageSize { get; set; } = 200;  // Adjust the page size as needed
//        public int CurrentPage { get; set; } = 1;
//        [BindProperty(SupportsGet = true)]
//        public string? SearchTerm { get; set; }  // Capture search term from query string

//        [BindProperty(SupportsGet = true)]
//        public int? GenderId { get; set; }

//        [BindProperty(SupportsGet = true)]
//        public int? MaritalStatusId { get; set; }

//        [BindProperty(SupportsGet = true)]
//        public int? BirthplaceId { get; set; }

//        [BindProperty(SupportsGet = true)]
//        public int? CurrentPlaceId { get; set; }

//        [BindProperty(SupportsGet = true)]
//        public int? CertificateId { get; set; }

//        [BindProperty(SupportsGet = true)]
//        public int? IsHighCertificate { get; set; }

//        [BindProperty(SupportsGet = true)]
//        public int? AuditedOrNot { get; set; }

//        public async Task<IActionResult> OnGetAsync(int? pageNumber, string? searchTerm)
//        {
//            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
//            {
//                return NotFound();  // Restrict access to non-admin users
//            }
//            ViewData["BirthplaceId"] = new SelectList(_context.Tblbirthplaces, "Id", "Birthplace");
//            ViewData["CertificateId"] = new SelectList(_context.Tblcertificates, "Id", "CertificateType");
//            ViewData["CurrentPlaceId"] = new SelectList(_context.TblCurrentPlaces, "Id", "CurrentPlace");
//            ViewData["GenderId"] = new SelectList(_context.Tblgenders, "Id", "Gender");
//            ViewData["MaritalStatusId"] = new SelectList(_context.Tblmaritalstatuses, "Id", "MaritalStatus");
//            CurrentPage = pageNumber ?? 1;  // Set current page based on query parameter or default to 1
//            SearchTerm = searchTerm;  // Set search term from query parameter

//            string connectionString = configuration["ConnectionStrings:DefaultConnection"];
//            UserPoints = new List<dynamic>();

//            int startRow = ((CurrentPage - 1) * PageSize) + 1;
//            int endRow = startRow + PageSize - 1;

//            // Add search condition to the query if it is provided
//            string searchCondition;
//            string GenderIdquery = "";
//            string MaritalStatusIdquery = "";
//            string BirthplaceIdquery = "";
//            string CurrentPlaceIdquery = "";
//            string CertificateIdquery = "";
//            string IsHighCertificatequery = "";
//            string AuditedOrNotquery = "";

//            if (string.IsNullOrEmpty(SearchTerm))
//            {
//                // No search term, so no additional WHERE clause
//                searchCondition = "";
//            }
//            else if (SearchTerm.StartsWith("bas", StringComparison.OrdinalIgnoreCase))
//            {
//                // Custom condition for search terms starting with "bas"

//                searchCondition = "WHERE Tbluserdata.UserName LIKE '%' + @SearchTerm + '%'"; // Searches only for names starting with "aaa"
//            }
//            else
//            {
//                // General search condition for any other term
//                searchCondition = "WHERE Tbluserdata.FullName LIKE '%' + @SearchTerm + '%'";
//            }

//            if (GenderId.HasValue) GenderIdquery = " AND Tbluserdata.GenderId = @GenderId";

//            if (MaritalStatusId.HasValue) MaritalStatusIdquery += " AND Tbluserdata.MaritalStatusId = @MaritalStatusId";

//            if (BirthplaceId.HasValue) BirthplaceIdquery += " AND Tbluserdata.BirthplaceId = @BirthplaceId";

//            if (CurrentPlaceId.HasValue) CurrentPlaceIdquery += " AND Tbluserdata.CurrentPlaceId = @CurrentPlaceId";

//            if (CertificateId.HasValue) CertificateIdquery += " AND Tbluserdata.CertificateId = @CertificateId";

//            if (IsHighCertificate.HasValue) IsHighCertificatequery += " AND Tbluserdata.IsHighCertificate = @IsHighCertificate";

//            if (AuditedOrNot.HasValue) AuditedOrNotquery += " AND Tbluserdata.AuditedOrNot = @AuditedOrNot";

//            // Main query with pagination
//            string query = $@"
//            WITH PaginatedResults AS (
//                SELECT 
//                    Tbluserdata.UserName,
//                    Tbluserdata.FullName,
//                    Tbluserdata.FirstName,
//                    Tbluserdata.SecondName,
//                    Tbluserdata.ThirdName,
//                    Tbluserdata.LastName,
//                    Tblgenders.Gender,
//                    Tblmaritalstatuses.MaritalStatus,
//                    Tblbirthplaces.Birthplace,
//                    TblCurrentPlaces.CurrentPlace,
//                    Tblcertificates.CertificateType,
//                    Tblpoints.ServicesYearsPoints,
//                    Tblpoints.CertificatePoints,
//                    Tblpoints.MaritalStatusPoints,
//                    Tblpoints.ChildrenPoint,
//                    ROW_NUMBER() OVER (ORDER BY Tbluserdata.FullName) AS RowNum
//                FROM [Edu_BasraLands].[dbo].[Tbluserdata]
//                INNER JOIN [Edu_BasraLands].[dbo].[Tblpoints] ON Tbluserdata.UserId = Tblpoints.UserId
//                INNER JOIN [Edu_BasraLands].[dbo].[Tblgenders] ON Tbluserdata.GenderId = Tblgenders.Id
//                INNER JOIN [Edu_BasraLands].[dbo].[TblCurrentPlaces] ON Tbluserdata.CurrentPlaceId = TblCurrentPlaces.Id
//                INNER JOIN [Edu_BasraLands].[dbo].[Tblbirthplaces] ON Tbluserdata.BirthplaceId = Tblbirthplaces.Id
//                INNER JOIN [Edu_BasraLands].[dbo].[Tblcertificates] ON Tbluserdata.CertificateId = Tblcertificates.Id
//                INNER JOIN [Edu_BasraLands].[dbo].[Tblmaritalstatuses] ON Tbluserdata.MaritalStatusId = Tblmaritalstatuses.Id
//                {searchCondition}{GenderIdquery}{MaritalStatusIdquery}
//                {BirthplaceIdquery}{CurrentPlaceIdquery}{CertificateIdquery}  
//                {IsHighCertificatequery}{AuditedOrNotquery}
//                )
//            SELECT * FROM PaginatedResults
//            WHERE RowNum BETWEEN @StartRow AND @EndRow
//            ORDER BY RowNum;

//            -- Count total records for pagination
//            SELECT COUNT(*) FROM [Edu_BasraLands].[dbo].[Tbluserdata]{searchCondition};";
//            // Add filters to the WHERE clause if selected

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                SqlCommand cmd = new SqlCommand(query, conn);
//                cmd.Parameters.AddWithValue("@StartRow", startRow);
//                cmd.Parameters.AddWithValue("@EndRow", endRow);

//                if (!string.IsNullOrEmpty(SearchTerm))
//                {
//                    cmd.Parameters.AddWithValue("@SearchTerm", SearchTerm);
//                }

//                if (GenderId.HasValue) cmd.Parameters.AddWithValue("@GenderId", GenderId.Value);

//                if (MaritalStatusId.HasValue) cmd.Parameters.AddWithValue("@MaritalStatusId", MaritalStatusId.Value);

//                if (BirthplaceId.HasValue) cmd.Parameters.AddWithValue("@BirthplaceId", BirthplaceId.Value);

//                if (CurrentPlaceId.HasValue) cmd.Parameters.AddWithValue("@CurrentPlaceId", CurrentPlaceId.Value);

//                if (CertificateId.HasValue) cmd.Parameters.AddWithValue("@CertificateId", CertificateId.Value);

//                if (IsHighCertificate.HasValue) cmd.Parameters.AddWithValue("@IsHighCertificate", IsHighCertificate.Value);

//                if (AuditedOrNot.HasValue) cmd.Parameters.AddWithValue("@AuditedOrNot", AuditedOrNot.Value);

//                conn.Open();

//                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
//                {
//                    while (reader.Read())
//                    {
//                        var user = new
//                        {
//                            UserName = reader["UserName"].ToString(),
//                            FullName = reader["FullName"].ToString(),
//                            FirstName = reader["FirstName"].ToString(),
//                            SecondName = reader["SecondName"].ToString(),
//                            ThirdName = reader["ThirdName"].ToString(),
//                            LastName = reader["LastName"].ToString(),
//                            Gender = reader["Gender"].ToString(),
//                            Birthplace = reader["Birthplace"].ToString(),
//                            CurrentPlace = reader["CurrentPlace"].ToString(),
//                            CertificateType = reader["CertificateType"].ToString(),
//                            MaritalStatus = reader["MaritalStatus"].ToString(),
//                            ServicesYearsPoints = reader["ServicesYearsPoints"] != DBNull.Value ? Convert.ToInt32(reader["ServicesYearsPoints"]) : 0,
//                            CertificatePoints = reader["CertificatePoints"] != DBNull.Value ? Convert.ToInt32(reader["CertificatePoints"]) : 0,
//                            MaritalStatusPoints = reader["MaritalStatusPoints"] != DBNull.Value ? Convert.ToInt32(reader["MaritalStatusPoints"]) : 0,
//                            ChildrenPoint = reader["ChildrenPoint"] != DBNull.Value ? Convert.ToInt32(reader["ChildrenPoint"]) : 0
//                        };

//                        UserPoints.Add(user);
//                    }

//                    if (reader.NextResult() && reader.Read())
//                    {
//                        TotalRecords = reader.GetInt32(0);
//                    }
//                }
//            }

//            return Page();
//        }
//    }


//}