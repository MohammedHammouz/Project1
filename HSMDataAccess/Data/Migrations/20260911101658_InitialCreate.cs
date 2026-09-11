using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HSMDataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false, defaultValueSql: "LEFT(CONVERT(nvarchar(36), NEWID()), 10)"),
                    Name = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    HeadOf = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false, defaultValueSql: "(N'(left(CONVERT([nvarchar](36),newid()),(10)))')"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactNumber = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Gender = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.ID);
                    table.CheckConstraint("CK_People_Email", "([Email] like '%_@_%._%')");
                    table.CheckConstraint("CK_People_Gender", "([Gender]='Female' OR [Gender]='Male')");
                    table.CheckConstraint("CK_People_Name", "(len(Trim([Name]))>=(2))");
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(100)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false, defaultValueSql: "(N'(left(CONVERT([nvarchar](36),newid()),(10)))')"),
                    PersonID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_People",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    MedicalHistory = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    PersonID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
                    table.CheckConstraint("CK_Patients_Status", "([Status]=(1) OR [Status]=(0))");
                    table.ForeignKey(
                        name: "FK_Patients_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalServices",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    ServiceName = table.Column<string>(type: "varchar(255)", nullable: false),
                    CategoryID = table.Column<string>(type: "nchar(10)", nullable: false),
                    DefaultPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DurationMinutes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedicalServices_ServiceCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "ServiceCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false, defaultValueSql: "(N'(left(CONVERT([nvarchar](36),newid()),(10)))')"),
                    Name = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmployeeID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.CheckConstraint("CK_Users_Role", "([Role]='Management' OR [Role]=' Accounts' OR [Role]='Nurse' OR [Role]='Doctor' OR [Role]='Admin')");
                    table.CheckConstraint("CK_Users_Status", "([Status]=(1) OR [Status]=(0))");
                    table.ForeignKey(
                        name: "FK_Users_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    PatientID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    PartialPaymentAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Invoice = table.Column<string>(type: "varchar(50)", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    InsuranceCoverage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    PatientResponsibility = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.ID);
                    table.CheckConstraint("CK_Bills_Status", "([Status]='Pending Insurance' OR [Status]='Cancelled' OR [Status]='Partially Paid' OR [Status]='Unpaid' OR [Status]='Paid')");
                    table.ForeignKey(
                        name: "FK_Bills_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    PatientID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Diagnosis = table.Column<string>(type: "text", nullable: true),
                    Treatment = table.Column<string>(type: "text", nullable: true),
                    Prescriptions = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "nchar(10)", nullable: true),
                    AuditTrail = table.Column<string>(type: "text", nullable: false),
                    AccessLevel = table.Column<string>(type: "nchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.ID);
                    table.CheckConstraint("CK_MedicalRecords_Status", "([Status]='Archived' OR [Status]='Active')");
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    UserID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Action = table.Column<string>(type: "nchar(10)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.ID);
                    table.CheckConstraint("CK_AuditLogs_Action", "([Action]='Access' OR [Action]='Delete' OR [Action]='Update' OR [Action]='Create')");
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false, defaultValueSql: "(N'(left(CONVERT([nvarchar](36),newid()),(10)))')"),
                    DepartmentID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<string>(type: "nchar(10)", nullable: true),
                    DepartmentID1 = table.Column<string>(type: "nchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.ID);
                    table.CheckConstraint("CK_Doctors_Status", "([Status]=0 OR [Status]=1)");
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepartmentID1",
                        column: x => x.DepartmentID1,
                        principalTable: "Departments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Doctors_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Notifictions",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    PatientID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    UserID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "nchar(5)", maxLength: 5, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    SentOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeliveryConfirmation = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifictions", x => x.ID);
                    table.CheckConstraint("CK_Notification_Status", "([Status]='Pending' OR [Status]='Failed' OR [Status]='Sent')");
                    table.CheckConstraint("CK_Notification_Type", "([Type]='SMS' OR [Type]='Email')");
                    table.ForeignKey(
                        name: "FK_Notifictions_Patients_UserID",
                        column: x => x.UserID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifictions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    GeneratedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    GeneratedBy = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    AppointmentCount = table.Column<int>(type: "int", nullable: true),
                    Revenue = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PaymentsReceived = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PendingPayments = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Metrics = table.Column<string>(type: "text", nullable: true),
                    ExportFormat = table.Column<string>(type: "nchar(5)", maxLength: 5, nullable: true),
                    Status = table.Column<string>(type: "nchar(7)", maxLength: 7, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ID);
                    table.CheckConstraint("CK_Reports_ExportFormat", "([ExportFormat]='Excel' OR [ExportFormat]='PDF')");
                    table.CheckConstraint("CK_Reports_Type", "([Type]='Monthly' OR [Type]='Weekly' OR [Type]='Daily')");
                    table.ForeignKey(
                        name: "FK_Reports_Users_GeneratedBy",
                        column: x => x.GeneratedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillServices",
                columns: table => new
                {
                    ServiceID = table.Column<string>(type: "nchar(10)", nullable: false),
                    BillID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillServices", x => new { x.BillID, x.ServiceID });
                    table.ForeignKey(
                        name: "FK_BillServices_Bills_BillID",
                        column: x => x.BillID,
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillServices_MedicalServices_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "MedicalServices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nchar(10)", nullable: false),
                    PatientID = table.Column<string>(type: "nchar(10)", nullable: false),
                    DoctorID = table.Column<string>(type: "nchar(10)", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time(7)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nchar(15)", nullable: false),
                    NotificationSent = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.ID);
                    table.CheckConstraint("CK_Appointments_Status", "([Status]='Cancelled' OR [Status]='Rescheduled' OR [Status]='Scheduled')");
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorID",
                table: "Appointments",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientID",
                table: "Appointments",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserID",
                table: "AuditLogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "idx_bills_date",
                table: "Bills",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "idx_bills_due_date",
                table: "Bills",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "idx_bills_patient",
                table: "Bills",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "idx_bills_status",
                table: "Bills",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "UQ_Bills_Invoice",
                table: "Bills",
                column: "Invoice",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillServices_ServiceID",
                table: "BillServices",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentID",
                table: "Doctors",
                column: "DepartmentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentID1",
                table: "Doctors",
                column: "DepartmentID1",
                unique: true,
                filter: "[DepartmentID1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Departments",
                table: "Doctors",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserID",
                table: "Doctors",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Users",
                table: "Doctors",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonID",
                table: "Employees",
                column: "PersonID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientID",
                table: "MedicalRecords",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_CategoryID",
                table: "MedicalServices",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "UQ__MedicalService__ServiceName",
                table: "MedicalServices",
                column: "ServiceName");

            migrationBuilder.CreateIndex(
                name: "IX_Notifictions_UserID",
                table: "Notifictions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonID",
                table: "Patients",
                column: "PersonID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_GeneratedBy",
                table: "Reports",
                column: "GeneratedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__Service__CategoryName",
                table: "ServiceCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BillServices");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Notifictions");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "MedicalServices");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
