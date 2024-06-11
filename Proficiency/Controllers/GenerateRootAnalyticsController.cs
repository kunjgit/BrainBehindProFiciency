using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proficiency.Data;
using Proficiency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proficiency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateRootAnalyticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenerateRootAnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<RootAnalytic>> Index()
        {
            var currentVersion = await _context.CurrentVersions.FirstOrDefaultAsync();

            if (currentVersion == null)
            {
                return NotFound("Current version not found.");
            }

            var timeTable = await _context.TimeTables
                .Include(tt => tt.Days)
                .ThenInclude(d => d.Lectures)
                .FirstOrDefaultAsync(tt => tt.Id == currentVersion.ActiveTTId);

            if (timeTable == null)
            {
                return NotFound("Timetable not found." + currentVersion.ActiveTTId);
            }

            DateTime lastUpdateDay = timeTable.RecentUpdatedDate;
            TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime currentDay = TimeZoneInfo.ConvertTime(DateTime.Now,indiaTimeZone);
            Dictionary<DayName, int> daysPassed = CountDaysBetween(lastUpdateDay, currentDay);
            
                //generating timetable analytics
                
                Dictionary<DayName, Dictionary<string, int>> subs = new Dictionary<DayName, Dictionary<string, int>>();
                Dictionary<DayName, Dictionary<string, int>> profs = new Dictionary<DayName, Dictionary<string, int>>();
                Dictionary<DayName, int> total = new Dictionary<DayName, int>();

                foreach (DayName day in Enum.GetValues(typeof(DayName)))
                {
                    total[day] = 0;
                    profs[day] = new Dictionary<string, int>();
                    subs[day] = new Dictionary<string, int>();
                }

                if (timeTable?.Days == null)
                {
                    return NotFound("Timetable days are null!" + timeTable.Id + timeTable.Days.First().Date);
                }

                foreach (var a in timeTable.Days)
                {
                    foreach (var b in a.Lectures)
                    {
                        if (!subs[a.DayName].ContainsKey(b.SubjectName))
                        {
                            subs[a.DayName][b.SubjectName] = 0;
                        }
                        if (!profs[a.DayName].ContainsKey(b.ProfName))
                        {
                            profs[a.DayName][b.ProfName] = 0;
                        }

                        total[a.DayName]++;
                        subs[a.DayName][b.SubjectName]++;
                        profs[a.DayName][b.ProfName]++;
                    }
                }

                TimeTableAnalytic newAnalytic = new TimeTableAnalytic
                {
                    subs = subs,
                    prof = profs,
                    version = currentVersion.ActiveTTId,
                    total = total
                };

                TimeTableAnalytic x;
                x = newAnalytic;
     

            var temp =  _context.RootAnalytics
                .Include(r=>r.Profs)
                .Include(r=>r.Subs)
                .FirstOrDefault();
            RootAnalytic previousRoot = temp;

            if (previousRoot == null)
            {
                Dictionary<string, ProfAnalytic> profDetails = new Dictionary<string, ProfAnalytic>();
                Dictionary<string, SubAnalytic> subDetails = new Dictionary<string, SubAnalytic>();
                int totalLectures = 0;

                foreach (KeyValuePair<DayName,int> a in daysPassed)
                {
                    DayName d = a.Key;
                    int days = a.Value;
                    totalLectures += x.total[d] * days;

                    foreach (var profOnDay in x.prof[d])
                    {
                        if (!profDetails.ContainsKey(profOnDay.Key))
                        {
                            profDetails[profOnDay.Key] = new ProfAnalytic
                            {
                                Professor = profOnDay.Key,
                                Lectures = 0
                            };
                        }

                        profDetails[profOnDay.Key].Lectures += profOnDay.Value * days;
                    }

                    foreach (var subOnDay in x.subs[d])
                    {
                        if (!subDetails.ContainsKey(subOnDay.Key))
                        {
                            subDetails[subOnDay.Key] = new SubAnalytic
                            {
                                Sub = subOnDay.Key,
                                Lectures = 0
                            };
                        }
                        subDetails[subOnDay.Key].Lectures += subOnDay.Value * days;
                    }
                }

                RootAnalytic ra = new RootAnalytic
                {
                    Profs = profDetails.Values.ToList(),
                    Subs = subDetails.Values.ToList(),
                    TotalLectures = totalLectures,
                    Version = currentVersion.ActiveTTId,
                    LatestUpdate = currentDay
                };

                await _context.RootAnalytics.AddAsync(ra);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                 lastUpdateDay = temp.LatestUpdate;
                indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                 currentDay = TimeZoneInfo.ConvertTime(DateTime.Now, indiaTimeZone);
                daysPassed = CountDaysBetween(lastUpdateDay, currentDay);
                
                Dictionary<string, ProfAnalytic> profDetails = new Dictionary<string, ProfAnalytic>();
                Dictionary<string, SubAnalytic> subDetails = new Dictionary<string, SubAnalytic>();

                foreach (var pr in previousRoot.Profs)
                {
                    string profName = pr.Professor;
                    profDetails[profName] = pr;
                }

                foreach (var sb in previousRoot.Subs)
                {
                    string subName = sb.Sub;
                    subDetails[subName] = sb;
                }

                int totalLectures = previousRoot.TotalLectures;
                foreach (var a in daysPassed)
                {
                    DayName d = a.Key;
                    int days = a.Value;
                    totalLectures += x.total[d] * days;

                    foreach (var profOnDay in x.prof[d])
                    {
                        if (!profDetails.ContainsKey(profOnDay.Key))
                        {
                            profDetails[profOnDay.Key] = new ProfAnalytic
                            {
                                Professor = profOnDay.Key,
                                Lectures = 0
                            };
                        }

                        profDetails[profOnDay.Key].Lectures += profOnDay.Value * days;
                    }

                    foreach (var subOnDay in x.subs[d])
                    {
                        if (!subDetails.ContainsKey(subOnDay.Key))
                        {
                            subDetails[subOnDay.Key] = new SubAnalytic
                            {
                                Sub = subOnDay.Key,
                                Lectures = 0
                            };
                        }
                        subDetails[subOnDay.Key].Lectures += subOnDay.Value * days;
                    }
                }

                temp.TotalLectures = totalLectures;
                temp.Profs = profDetails.Values.ToList();
                temp.Subs = subDetails.Values.ToList();
                temp.Version = currentVersion.ActiveTTId;
                temp.LatestUpdate = currentDay;
                _context.RootAnalytics.Update(temp);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        static Dictionary<DayName, int> CountDaysBetween(DateTime startDate, DateTime endDate)
        {
            Dictionary<DayName, int> dayCountDictionary = new Dictionary<DayName, int>();

            foreach (DayName dayName in Enum.GetValues(typeof(DayName)))
            {
                dayCountDictionary[dayName] = 0;
            }

            int totalDays = (int)(endDate - startDate).TotalDays;

            DayName startDayOfWeek = (DayName)startDate.DayOfWeek;

            for (int i = 0; i <= totalDays; i++)
            {
                DayName currentDay = (DayName)(((int)startDayOfWeek + i) % 7);
                dayCountDictionary[currentDay]++;
            }

            return dayCountDictionary;
        }
    }
}
