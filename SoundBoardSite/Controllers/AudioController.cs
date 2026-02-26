using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
public class AudioController : Controller
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public AudioController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _db.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(new AudioCreateVm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AudioCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(vm);
        }

        // Basic validation: ensure it's an audio file
        if (vm.Mp3.Length <= 0)
        {
            ModelState.AddModelError(nameof(vm.Mp3), "File is empty.");
            ViewBag.Categories = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(vm);
        }

        var uploadsDir = Path.Combine(_env.ContentRootPath, "uploads"); // outside wwwroot
        Directory.CreateDirectory(uploadsDir);

        var ext = Path.GetExtension(vm.Mp3.FileName);
        var fileName = $"{Guid.NewGuid():N}{ext}";
        var fullPath = Path.Combine(uploadsDir, fileName);

        await using (var stream = System.IO.File.Create(fullPath))
        {
            await vm.Mp3.CopyToAsync(stream);
        }

        // Create row(s)
        var audio = new AudioFile
        {
            Label = vm.Label.Trim(),
            Description = string.IsNullOrWhiteSpace(vm.Description) ? null : vm.Description.Trim(),
            StoragePath = $"uploads/{fileName}", // your own convention
            ContentType = vm.Mp3.ContentType,
            SizeBytes = vm.Mp3.Length,
            UploadedAt = DateTimeOffset.UtcNow
        };

        // If you used many-to-many: create join row
        audio.AudioFileCategories.Add(new AudioFileCategory
        {
            CategoryId = vm.CategoryId
        });

        _db.AudioFiles.Add(audio);
        await _db.SaveChangesAsync();

        return RedirectToAction("Details", new { id = audio.Id });
    }

    // Simple Details page (optional but helpful)
    public async Task<IActionResult> Details(int id)
    {
        var audio = await _db.AudioFiles
            .Include(a => a.AudioFileCategories)
            .ThenInclude(ac => ac.Category)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (audio == null) return NotFound();
        return View(audio);
    }
}