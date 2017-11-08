namespace SSTVN.PMS.Common
{
    #region Enum
    public enum RequestType
    {
        Annual = 0,
        Sick,
        Marriage,
        Funeral,
        Unpaid,
        Maternity
    }

    public enum vcOrderStatus
    {
        Pending = 0, Approved, Reject
    }

    public enum FullAmPm
    {
        FullDay = 0,
        AM,
        PM
    }

    public enum Position
    {
        Member = 0,
        Director,
        HRManager,
        HRAdmin,
        TeamLeader,
        Senior,
    }
    #endregion

    public static class Constant
    {
        #region RexPatterns
        public const string REGEX_URLSEO = @"[^A-Za-z0-9_\.~]+";
        #endregion

        #region Employee
        public const string COLUMN_FullName = "FullName";
        public const string COLUMN_EmployeeID = "EmployeeID";
        public const string SUFFIX_EMAIL_STRONGTIE = "@strongtie.com";
        #endregion

        #region Vacation
        public const string CODE_DEFAULT_ANNUAL_LEAVE = "VACATION_HOLIDAY_POLICY_default_annual_leave";
        public const string CODE_TOTAL_DAY_INCREASE = "VACATION_HOLIDAY_POLICY_Total_Day_Increase";
        public const string CODE_TOTAL_YEARS_FOR_INCREASE = "VACATION_HOLIDAY_POLICY_Total_Years_for_Increase";
        public const string COLUMN_Name = "Name";
        public const string COLUMN_Type = "Type";
        public const string COLUMN_Value = "Value";
        public const string CODE_PUBLIC_HOLIDAY = "VACATION_HOLIDAY_POLICY_public_holiday";
        public const string VACATION_REQUEST_TYPE = "VACATION_REQUEST_TYPE";
        public const string VACATION_ORDERSTATUS = "VACATION_ORDERSTATUS";
        public const string VACATION_BALANCEDAY_BY_YEAR = "VACATION_BALANCEDAY_BY_YEAR";
        public const string VACATION_AWARDDAY_BY_YEAR = "VACATION_AWARDDAY_BY_YEAR";
        public const string VACATION_ADVANCEDLEAVE_BY_YEAR = "VACATION_ADVANCEDLEAVE_BY_YEAR";
        public const string VACATION_PUBLIC_HOLIDAY_VN = "VACATION_PUBLIC_HOLIDAY_VN";
        public const string VACATION_PUBLIC_HOLIDAY_VN_RECURRENT = "VACATION_PUBLIC_HOLIDAY_VN_RECURRENT";
        public const string VACATION_LAST_OPEN = "VACATION_LAST_OPEN";
        public const string ACCESS_PERMISSION_VACATION_ADMIN = "ACCESS_PERMISSION_VACATION_ADMIN";
        public const string ACCESS_PERMISSION_VACATION_LEADER = "ACCESS_PERMISSION_VACATION_LEADER";

        #region Email Communication
        public const string VACATION_EMAILCOMMUNICATION = "VACATION_EMAILCOMMUNICATION";
        public const string HR_Manager = "HR_Manager";
        public const string Director = "Director";
        public const string DirectorOfES = "Director_of_ES";
        public const string HR_Dept = "HR_Dept";
        #endregion

        #region Simple RequestType
        public const string VACATION_Paid = "Paid";
        public const string VACATION_Paidstar = "Paid*";
        public const string VACATION_Unpaid = "Unpaid";
        #endregion

        #endregion

        #region ReferType
        public const string TYPE_Text = "Text";
        public const string TYPE_DateTime = "DateTime";
        public const string TYPE_Number = "Number";
        public const string TYPE_Boolean = "Boolean";
        #endregion

        #region StatusLabel
        public const string STT_Ready = "Ready";
        public const string STT_Submitsucceeded = "Submit succeeded";
        public const string STT_Clearsucceeded = "Clear succeeded";
        public const string STT_Deletesucceeded = "Delete succeeded";
        public const string STT_DeleteUnsucceeded = "Delete a record or sending an email unsuccessfully";
        public const string STT_Processing = "Processing...";
        public const string STT_NORECORD = "No request found. Please select at least 1 request !!!";
        public const string STT_CALENDAR_Couldselect = "Couldn't select a past date";
        public const string STT_CALENDAR_ADD_Succeed = "{0} day(s) have been selected successfully";
        public const string STT_CALENDAR_DeSucceed = "Deselected succeeded";
        public const string STT_Colorsucceeded = "Update color succeeded";
        #endregion

        #region Path
        public const string PATH_PHOTOS = @"C:\PMS\Files\Photos\";
        #endregion

        #region
        public const string DateTimeFormat = "MM/dd/yyyy";
        public const string DateTimeFormat2 = "MM/dd/yyyy HH:mm:ss";
        public const string DateTimeFormat_MMMddyyyy = "MMM dd, yyyy";
        #endregion

        #region Leave Request Form
        public const string STATEMENTDATE = "StatementDate";
        public const string OPENINGDATE = "OpeningDate";
        public const string CLOSINGDATE = "ClosingDate";
        #endregion

        #region Feedback
        public const string EVENT_urlMAKEUSBETTER = "EVENT_urlMakeUsBetter";
        #endregion

        #region Parameter Code
        public const string USER_PMS_VERSION = "USER_PMS_VERSION";
        #endregion

        #region Some special parameters need hardcode!!
        // Project for Gen0
        public const string GEN0_Description = "This record is created by PMS - Vacation!";
        public const string GEN0_Description_Holiday = "This record is created by PMS - Holiday!";
#if DEBUG
        public const int GEN0_ProId = 23691;
        public const int GEN0_SubTaskId = 225;
#else
        public const int GEN0_ProId = 27108;
        public const int GEN0_SubTaskId = 235;
#endif
        #endregion

        #region Error Message
        public const string MessageErr_RequestCtl_InsertTimeLogs = "Error at Insert TimeLog; Their time log may be not inserted yet!";
        public const string MessageErr_RequestCtl_InsertTimeLog = "Error at Insert TimeLog; Their time log may be not inserted yet! But the request(s) have been approved!";
        public const string MessageErr_RequestCtl_DeleteTimeLog = "Error at Delete TimeLog; Their time log may be not deleted yet! But the request(s) have been removed!";
        #endregion

        #region Email Template String
        public const string Email_Account_ForgotPassword = "Account_ForgotPassword.html";
        #endregion

        #region Url
        public const string PARENT_PATH_API_IMAGE = @"http://105vm-w764-sql-ddo/SSTVN/PMSAPI/api/Image/Getemployeeimage/";
        #endregion
    }
}
