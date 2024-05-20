using back_end_projects.DAL;
using back_end_projects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace back_end_projects.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DoctorController( AppDbContext context, IWebHostEnvironment environment) 
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Doctors.ToList());
        }
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            string fileName = Guid.NewGuid()+doctor.ImgFile.FileName;


            string path = _environment.WebRootPath + @"\Upload\Doctor\";
            using(FileStream stream = new FileStream(path + fileName,FileMode.Create))
            {
                doctor.ImgFile.CopyTo(stream);
            }
            doctor.ImgUrl = fileName;
            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
        public IActionResult Update(int id) 
        {
           Doctor doctor = _context.Doctors.FirstOrDefault(x=>x.Id==id);
            if(doctor == null)
            {
                return RedirectToAction("Index");
            }

            return View(doctor);
        }
        [HttpPost]
        public IActionResult Update(Doctor newdoctor)
        {
            Doctor olddoctor = _context.Doctors.FirstOrDefault(x => x.Id == newdoctor.Id);
            if (olddoctor == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(olddoctor);

            }
          if(newdoctor.ImgFile != null) {

                string fileName = Guid.NewGuid() + newdoctor.ImgFile.FileName;


                string path = _environment.WebRootPath + @"\Upload\Doctor\";
                FileInfo fileInfo = new FileInfo(path+olddoctor);
                using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
                {
                    newdoctor.ImgFile.CopyTo(stream);
                }
                olddoctor.ImgUrl = fileName;
            }
          olddoctor.FullName=newdoctor.FullName;
            olddoctor.Position=newdoctor.Position;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(x => x.Id == id);
            if (doctor == null) return NotFound();

           
            string imagePath = Path.Combine(_environment.WebRootPath, "Upload", "Doctor", doctor.ImgUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
