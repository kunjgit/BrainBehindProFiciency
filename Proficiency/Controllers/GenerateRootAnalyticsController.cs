using Microsoft.AspNetCore.Mvc;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers;


    [Route("api/[controller]")]
    [ApiController]
    public class GenerateRootAnalyticsController : ControllerBase
    {
        private readonly TimeTablesController _tt;
        private readonly ApplicationDbContext _context;
        private readonly CurrentVersionController _cur_version;
        private readonly TimeTableAnalyticsController _ttanalytics;
        private readonly RootAnalyticController _ranalyticscontroller;
        public GenerateRootAnalyticsController(TimeTablesController tt, CurrentVersionController cur_version, ApplicationDbContext context , TimeTableAnalyticsController ttanalytics , RootAnalyticController ranalyticscontroller)
        {
            _tt = tt;
            _context = context;
            _cur_version = cur_version;
            _ttanalytics = ttanalytics;
            _ranalyticscontroller = ranalyticscontroller;
        }
        
        
        
        [HttpGet]
        public async Task<ActionResult<RootAnalytic>> Index()
        {

            var cvTask = _cur_version.GetCurrentVersion();
            var cvResult = await cvTask;
            var currentVersion = cvResult.Value;

            if (currentVersion == null)
            {
                return NotFound("Current version not found.");
            }

            // sort of fetching currentTT details and upating it !
            var ttResult =  _tt.Get(currentVersion.ActiveTTId);
            var timeTable = ttResult.Value;

            if (timeTable == null)
            {
                return NotFound("Timetable not found.");
            }

            
            
            
            
            //doing calculation for fetching the days passed there !
            DateTime lastupdate_day = timeTable.RecentUpdatedDate;
            DateTime current_day = DateTime.Now;
            Dictionary<DayName, int> days_passed = CountDaysBetween(lastupdate_day, current_day);
            
            //checking if there is already timetable analytics there ...

            var ttana=_ttanalytics.GetTimeTableAnalytic();
          
            var x = ttana.Value;

           
            
            if (x == null)// we are not having timetable analytic so we have to create one....
            {

                Dictionary<DayName, Dictionary<string, int>> subs = new Dictionary<DayName, Dictionary<string, int>>();
                Dictionary<DayName, Dictionary<string, int>> profs = new Dictionary<DayName, Dictionary<string, int>>();
                Dictionary<DayName, int> total=new Dictionary<DayName, int>();
            
                foreach (var a in timeTable.Days)
                {

                    foreach (var b in a.Lectures)
                    {
                        total[a.DayName]++;
                        subs[a.DayName][b.SubjectName]++;
                        profs[a.DayName][b.ProfName]++;
                    }
                }
                
                TimeTableAnalytic newanalytic=new TimeTableAnalytic();
                newanalytic.subs = subs;
                newanalytic.prof = profs;
                newanalytic.version = currentVersion.ActiveTTId;
                newanalytic.total = total;

                await _ttanalytics.CreateTimeTableAnalytic(newanalytic);
                //creating timetable analytic
                subs.Clear();
                profs.Clear();
                total.Clear();

                x = newanalytic;
            }

            // generating the root analytics

            var temp = _ranalyticscontroller.GetRootAnalyticByV(currentVersion.ActiveTTId);
            var tempa = await temp;
            RootAnalytic previous_root = tempa.Value;

            int total_days = 0;
            if (previous_root==null)
            {
                //creating new root analytics
                Dictionary<string,ProfAnalytic>prof_details = new Dictionary<string, ProfAnalytic>();
                Dictionary<string, SubAnalytic> sub_details = new Dictionary<string, SubAnalytic>();
                int total_lectures=0;
                foreach (var a in days_passed)
                {
                    // Fetching for a single day
                    DayName d = a.Key;
                    int days = a.Value;
                    total_days += x.total[d] * days;
                    // Process prof details
                    foreach (var prof_on_day in x.prof[d])
                    {
                        if (!prof_details.ContainsKey(prof_on_day.Key))
                        {
                            prof_details[prof_on_day.Key] = new ProfAnalytic
                            {
                                Professor = prof_on_day.Key,
                                Lectures = 0
                            };
                        }


                        prof_details[prof_on_day.Key].Lectures+=prof_on_day.Value*days;
                    }

                    // Process sub details
                    foreach (var sub_on_day in x.subs[d])
                    {
                        if (!sub_details.ContainsKey(sub_on_day.Key))
                        {
                            sub_details[sub_on_day.Key] = new SubAnalytic
                            {
                                Sub = sub_on_day.Key,
                                Lectures = 0
                            };
                        }
                        sub_details[sub_on_day.Key].Lectures+=sub_on_day.Value*days;
                    }


                   
                }
                
                
                RootAnalytic ra = new RootAnalytic();
                foreach (var p in prof_details)
                {
                    ra.Profs.Add(p.Value);
                }

                foreach (var s in sub_details)
                {
                    ra.Subs.Add(s.Value);
                }

                ra.TotalLectures = total_lectures;

                    
                await _ranalyticscontroller.PostRootAnalytic(ra);

                return Ok();


            }
            else
            {
                //we have to reuse previous analytics so that we can optimize the pre calculated values
                Dictionary<string,ProfAnalytic>prof_details = new Dictionary<string, ProfAnalytic>();
                Dictionary<string, SubAnalytic> sub_details = new Dictionary<string, SubAnalytic>();

    
                foreach (var pr in previous_root.Profs)
                {
                    string pro_name = pr.Professor;
                    prof_details[pro_name] = pr;
                }
                foreach (var sb in previous_root.Subs)
                {
                    string pro_name = sb.Sub;
                    sub_details[pro_name] = sb;
                }
                
                int total_lectures=previous_root.TotalLectures;
                foreach (var a in days_passed)
                {
                    // Fetching for a single day
                    DayName d = a.Key;
                    int days = a.Value;
                    total_days += x.total[d] * days;
                    // Process prof details
                    foreach (var prof_on_day in x.prof[d])
                    {
                        if (!prof_details.ContainsKey(prof_on_day.Key))
                        {
                            prof_details[prof_on_day.Key] = new ProfAnalytic
                            {
                                Professor = prof_on_day.Key,
                                Lectures = 0
                            };
                        }


                        prof_details[prof_on_day.Key].Lectures+=prof_on_day.Value*days;
                    }

                    // Process sub details
                    foreach (var sub_on_day in x.subs[d])
                    {
                        if (!sub_details.ContainsKey(sub_on_day.Key))
                        {
                            sub_details[sub_on_day.Key] = new SubAnalytic
                            {
                                Sub = sub_on_day.Key,
                                Lectures = 0
                            };
                        }
                        sub_details[sub_on_day.Key].Lectures+=sub_on_day.Value*days;
                    }


                   
                }
                // make sure you are having upated details
                RootAnalytic ra = new RootAnalytic();
                foreach (var p in prof_details)
                {
                    ra.Profs.Add(p.Value);
                }

                foreach (var s in sub_details)
                {
                    ra.Subs.Add(s.Value);
                }

                ra.TotalLectures = total_lectures;


                int ra_id = previous_root.id;
               await  _ranalyticscontroller.PutRootAnalytic(ra_id,ra);

               return Ok();
               



            }
            
            
            
            
            
           

            await _context.SaveChangesAsync();

            return Ok();
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