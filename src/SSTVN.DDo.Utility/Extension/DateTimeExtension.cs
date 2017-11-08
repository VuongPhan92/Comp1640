using System;
using System.Data;

namespace SSTVN.DDo.Utility.Extension
{
    public static class DateTimeExtension
    {
        public static DateTime DateWithFirstDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime DateWithLastDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }

        #region DataRow
        const int m_iNO_MOVE_OCCURED = -1; // number to indicate error

        public static int MoveRowUp(this DataRow row)
        {
            DataTable dtParent = row.Table;
            int iOldRowIndex = dtParent.Rows.IndexOf(row);

            DataRow newRow = dtParent.NewRow();
            newRow.ItemArray = row.ItemArray;

            if (iOldRowIndex < dtParent.Rows.Count)
            {
                dtParent.Rows.Remove(row);
                dtParent.Rows.InsertAt(newRow, iOldRowIndex + 1);
                return dtParent.Rows.IndexOf(newRow);
            }

            return m_iNO_MOVE_OCCURED;
        }

        public static int MoveRowDown(this DataRow row)
        {
            DataTable dtParent = row.Table;
            int iOldRowIndex = dtParent.Rows.IndexOf(row);

            DataRow newRow = dtParent.NewRow();
            newRow.ItemArray = row.ItemArray;

            if (iOldRowIndex > 0)
            {
                dtParent.Rows.Remove(row);
                dtParent.Rows.InsertAt(newRow, iOldRowIndex - 1);
                return dtParent.Rows.IndexOf(newRow);
            }

            return m_iNO_MOVE_OCCURED;
        }

        public static int MoveLast(this DataRow row)
        {
            DataTable dtParent = row.Table;
            int iOldRowIndex = dtParent.Rows.IndexOf(row);

            DataRow newRow = dtParent.NewRow();
            newRow.ItemArray = row.ItemArray;

            if (iOldRowIndex > 0)
            {
                dtParent.Rows.Remove(row);
                dtParent.Rows.InsertAt(newRow, dtParent.Rows.Count);
                return dtParent.Rows.IndexOf(newRow);
            }

            return m_iNO_MOVE_OCCURED;
        }
        #endregion

        public static string TimePassed(this DateTime dateInput)
        {
            string date = null;
            double dateDiff = 0.0;
            var timeDiff = DateTime.Now - dateInput;
            var yearPassed = timeDiff.TotalDays / 365;
            var monthPassed = timeDiff.TotalDays / 30;
            var dayPassed = timeDiff.TotalDays;
            var hourPassed = timeDiff.TotalHours;
            var minutePassed = timeDiff.TotalMinutes;
            var secondPassed = timeDiff.TotalSeconds;
            if (Math.Floor(yearPassed) > 0)
            {
                dateDiff = Math.Floor(yearPassed);
                date = dateDiff == 1 ? dateDiff + " year ago" : dateDiff + " years ago";
            }
            else
            {
                if (Math.Floor(monthPassed) > 0)
                {
                    dateDiff = Math.Floor(monthPassed);
                    date = dateDiff == 1 ? dateDiff + " month ago" : dateDiff + " months ago";
                }
                else
                {
                    if (Math.Floor(dayPassed) > 0)
                    {
                        dateDiff = Math.Floor(dayPassed);
                        date = dateDiff == 1 ? dateDiff + " day ago" : dateDiff + " days ago";
                    }
                    else
                    {
                        if (Math.Floor(hourPassed) > 0)
                        {
                            dateDiff = Math.Floor(hourPassed);
                            date = dateDiff == 1 ? dateDiff + " hour ago" : dateDiff + " hours ago";
                        }
                        else
                        {
                            if (Math.Floor(minutePassed) > 0)
                            {
                                dateDiff = Math.Floor(minutePassed);
                                date = dateDiff == 1 ? dateDiff + " minute ago" : dateDiff + " minutes ago";
                            }
                            else
                            {
                                dateDiff = Math.Floor(secondPassed);
                                date = dateDiff == 1 ? dateDiff + " second ago" : dateDiff + " seconds ago";
                            }
                        }
                    }
                }
            }

            return date;
        }
    }
}
