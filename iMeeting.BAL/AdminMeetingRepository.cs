﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iMeeting.DAL;

namespace iMeeting.BAL
{
    public class AdminMeetingRepository : IAdminMeetingRepository
    {
        private DB_Context _context;
        public AdminMeetingRepository(DB_Context dB_Context)
        {
            this._context = dB_Context;
        }

        public IEnumerable<MeetingModel> FilterDate(string Filter)
        {
            DateTime dtFrom = Convert.ToDateTime(Filter);
            return _context.Meeting.Where(x => x.IsActive == 1 && x.DateTime == dtFrom).ToList();
        }

        public IEnumerable<MeetingModel> GetMeeting(string Filter)
        {
            if (Filter == "Today")
            {
                DateTime dateTime = DateTime.Now;
                var Today = dateTime.Date;
                return _context.Meeting.Where(x => x.DateTime == Today && x.IsActive == 1).ToList();
            }
            else if (Filter == "Tomorrow")
            {
                var Today = DateTime.Today;
                var Tomorrow = Today.AddDays(1);
                return _context.Meeting.Where(x => x.DateTime == Tomorrow && x.IsActive == 1).ToList();
            }
            else if (Filter == "ThisMonth")
            {
                DateTime dateTime = DateTime.Now;
                var Today = dateTime.Month;
                return _context.Meeting.Where(x => x.DateTime.Month == Today && x.IsActive == 1).ToList();

            }
            else if (Filter == "ThisWeek")
            {
                var Today = DateTime.Today;
                var ThisWeek = Today.AddDays(7);
                return _context.Meeting.Where(x => x.DateTime >= Today && x.DateTime < ThisWeek && x.IsActive == 1).ToList();

            }
            return _context.Meeting.Where(x => x.IsActive == 1).ToList();
        }

        public IEnumerable<MeetingModel> GetMeeting()
        {
            return _context.Meeting.Where(x => x.IsActive == 1).ToList();

        }
    }
}