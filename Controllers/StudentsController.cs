using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAll()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    // GET: api/students/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult<Student>> Create(StudentDto StudentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var student = new Student { Name = StudentDto.Name, Score = StudentDto.Score };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    // PUT: api/students/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudentDto StudentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        student.Name = StudentDto.Name;
        student.Score = StudentDto.Score;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/students/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
