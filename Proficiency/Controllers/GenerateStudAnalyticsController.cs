using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class GenerateStudAnalyticsController : Controller
{
    private readonly ApplicationDbContext _context;
    
    
    public GenerateStudAnalyticsController(ApplicationDbContext context)
    {
        _context = context;

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudAnalytic>> Generate(int id)
    {
        //getting current version and fetching the timetable using this only you will be able 

        CurrentVersion cv =   _context.CurrentVersions.First();

        int currentActiveTTid = cv.ActiveTTId;
        int stu_id = id; 
        
        // making sure that user exist already 

        var std=_context.Students.FindAsync(stu_id);
        var student = await std;
        if (student == null)
        {
            return NotFound("student not found with id"+id);
        }
        // we are sure that timetable analytics will be there only as we are confident that we are going to make call earlier only 
        
        
        var temp = await   _context.RootAnalytics
            .Include(r=>r.Profs)
            .Include(r=>r.Subs)
            .FirstOrDefaultAsync();
        RootAnalytic rtna = temp;
        
        if (rtna == null)
        {
            return NotFound("timetable not found please check it out");
        }
        
        
        // make list of profs for which we will have to search and create that object !



        List<ProfAnalytic> a=new List<ProfAnalytic>();
        List<SubAnalytic> b=new List<SubAnalytic>();


        int total_attended = 0;
        // created profs wise list !
        foreach (var pro in rtna.Profs)
        {
            string profname = pro.Professor;
            int count = pro.Lectures;
            ProfAnalytic p = new ProfAnalytic();
            p.Professor = profname;
            var attendances = _context.Attendances.Where(a => a.StudentId == id && _context.Lectures.Any
                (lec => lec.Id == a.LectureId && lec.ProfName == profname)).ToList();

            if (attendances == null)
            {
                p.Lectures = 0;
            }
            else
            {
                // we have to make sure someone is not attending lectures more than professor / sub has taken
                if (attendances.Count <= count)
                {
                    p.Lectures = attendances.Count;
                }
                else
                {
                    //lol proxy condition
                    p.Lectures = count;
                }
            }

            total_attended += p.Lectures;
            a.Add(p);
            

        }
        
        //create subwise analytics
        foreach (var sub in rtna.Subs)
        {
            string sub_name = sub.Sub;
            int count = sub.Lectures;
            SubAnalytic s = new SubAnalytic();
            s.Sub = sub_name;
            var attendances = _context.Attendances.Where(a => a.StudentId == id && _context.Lectures.Any
                (lec => lec.Id == a.LectureId && lec.SubjectName == sub_name)).ToList();

            if (attendances == null)
            {
                s.Lectures = 0;
            }
            else
            {
                if (attendances.Count <= count)
                {
                    s.Lectures = attendances.Count;
                }
                else
                {
                    //lol proxy condition :)
                    s.Lectures = count;
                }
                
            }
            b.Add(s);

        }
        
        
        
        // we have to do all these calculation each time ... why ? as we can delete the previous entry of the attendance
        // finally now we will check if it's already there just update it otherwise we will create

        var stuana = await _context.StudAnalytics
            .Include(s => s.Profwise)
            .Include(s => s.SubWise)
            .FirstOrDefaultAsync(s => s.StuId == stu_id);


        if (stuana == null)
        {
            stuana = new StudAnalytic
            {
                StuId = stu_id,
                Profwise = a,
                SubWise = b,
                TotalLectures = total_attended,
            };

            await _context.StudAnalytics.AddAsync(stuana);
            await _context.SaveChangesAsync();
            return Ok("created student analytics super efficiently");
        }
        
            //updating the student analytics if it already exist
            stuana.Profwise = a;
            stuana.SubWise = b;
            stuana.TotalLectures = total_attended;
            TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
           stuana.RecentUpate = TimeZoneInfo.ConvertTime(DateTime.Now,indiaTimeZone);
            
            
             _context.StudAnalytics.Update(stuana);
             await _context.SaveChangesAsync();
             
             a.Clear();
             b.Clear();
             

             return Ok("updation of the student analytics done efficiently !");

    }
}