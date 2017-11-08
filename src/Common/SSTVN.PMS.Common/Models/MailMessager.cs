using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTVN.PMS.Common.Models
{
    public class MailMessager
    {
        #region Constructor
        public MailMessager()
        {

        }

        public MailMessager(List<string> To, List<string> Cc, string Subject, string body, string DisplayName)
        {
            if (To != null)
            {
                m_To = To;
            }

            if (Cc != null)
            {
                m_Cc = Cc;
            }

            m_subject = Subject;
            m_body = body;
            m_displayName = DisplayName;
        }
        #endregion

        #region Class Members
        private List<string> m_To = new List<string>();
        private List<string> m_Cc = new List<string>();
        private string m_subject;
        private string m_body;
        private string m_displayName;
        private string m_position;
        #endregion

        #region Properties
        public List<string> To
        {
            get
            {
                return m_To;
            }
            set
            {
                m_To = value;
            }
        }

        public List<string> Cc
        {
            get
            {
                return m_Cc;
            }
            set
            {
                m_Cc = value;
            }
        }

        public string Subject
        {
            get
            {
                return m_subject;
            }
            set
            {
                m_subject = value;
            }
        }

        public string Body
        {
            get
            {
                return m_body;
            }
            set
            {
                m_body = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return m_displayName;
            }
            set
            {
                m_displayName = value;
            }
        }

        public string position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }
        #endregion
    }
}
