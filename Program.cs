using ConsoleApp11;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        using var db = new AppDbContext();

        SeedStudentsAndEnrollments(db);

        Console.WriteLine("\n--- CREATE ---");
        int newInstructorId = AddInstructor(db);
        int newCourseId = AddCourse(db, newInstructorId, 5);
        AddCourse(db, 999, 5);
        AddCourse(db, newInstructorId, 999);
        AddStudents(db);
        EnrollStudent(db, 1, 5);
        EnrollStudent(db, 999, 5);
        EnrollStudent(db, 1, 999);
        EnrollStudent(db, 1, 3);
        EnrollStudent(db, 1, 1);
        EnrollStudent(db, 1, newCourseId);

        Console.WriteLine("\n--- READ ---");
        FindCourse(db, 1, "Design Systems");
        FindCourse(db, 999, "No Such Course");
        FilterCourses(db, 300m, 800m);
        SubCategories(db, 1);
        CourseRoster(db, 1);
        CourseRoster(db, 999);

        Console.WriteLine("\n--- UPDATE ---");
        ChangePrice(db, 1, 350m);
        ChangePrice(db, 999, 100m);
        UpdateProgress(db, 1, 2, 55);
        UpdateProgress(db, 1, 2, 100);
        UpdateProgress(db, 1, 2, 150);
        UpdateProgress(db, 999, 2, 50);
        PublishCourse(db, newCourseId);
        PublishCourse(db, 999);

        Console.WriteLine("\n--- DELETE ---");
        Unenroll(db, 5, 1);
        Unenroll(db, 999, 1);
        DeleteCategoryUnsafe(db, 1);
        var tempCategory = new Categories { Name = "Temporary Category", Description = "Used to test the safe delete" };
        db.Categories.Add(tempCategory);
        db.SaveChanges();
        DeleteCategorySafe(db, 1);
        DeleteCategorySafe(db, tempCategory.Id);
        DeleteCategorySafe(db, 999);
        EnrollStudent(db, 1, newCourseId);
        EnrollStudent(db, 2, newCourseId);
        DeleteCourse(db, newCourseId);
        DeleteCourse(db, 999);

        Console.WriteLine("\n--- BULK OPERATIONS ---");
        BulkPriceRaise(db, CourseLevel.Beginner);
        BulkDeleteDropped(db);
    }

    static void SeedStudentsAndEnrollments(AppDbContext db)
    {
        if (!db.Students.Any())
        {
            var students = new List<Students>
            {
                new Students { FullName = "Mona Adel",     EmailAddress = "mona.adel@example.com",     DOB = new DateTime(2001, 3, 14) },
                new Students { FullName = "Youssef Nabil", EmailAddress = "youssef.nabil@example.com", DOB = new DateTime(2000, 7, 2) },
                new Students { FullName = "Hana Samir",    EmailAddress = "hana.samir@example.com",    DOB = new DateTime(2002, 11, 25) },
                new Students { FullName = "Karim Fathy",   EmailAddress = "karim.fathy@example.com",   DOB = new DateTime(1999, 5, 30) },
                new Students { FullName = "Nour Ibrahim",  EmailAddress = "nour.ibrahim@example.com",  DOB = new DateTime(2003, 1, 9) }
            };

            db.Students.AddRange(students);
            db.SaveChanges();
            Console.WriteLine("Seeded 5 students.");

            if (!db.Enrollments.Any())
            {
                db.Enrollments.AddRange(
                    new Enrollments { StudentsId = students[0].Id, CoursesId = 1, EnrollmentDate = new DateTime(2024, 5, 1), Progress = 100, EnrollmentStatus = EnrollmentStatus.Completed, FinalGrade = 92.50m },
                    new Enrollments { StudentsId = students[0].Id, CoursesId = 2, EnrollmentDate = new DateTime(2024, 5, 10), Progress = 40, EnrollmentStatus = EnrollmentStatus.Active },
                    new Enrollments { StudentsId = students[1].Id, CoursesId = 1, EnrollmentDate = new DateTime(2024, 5, 3), Progress = 100, EnrollmentStatus = EnrollmentStatus.Completed, FinalGrade = 85.00m },
                    new Enrollments { StudentsId = students[1].Id, CoursesId = 4, EnrollmentDate = new DateTime(2024, 6, 1), Progress = 20, EnrollmentStatus = EnrollmentStatus.Dropped },
                    new Enrollments { StudentsId = students[2].Id, CoursesId = 2, EnrollmentDate = new DateTime(2024, 6, 15), Progress = 75, EnrollmentStatus = EnrollmentStatus.Active },
                    new Enrollments { StudentsId = students[3].Id, CoursesId = 5, EnrollmentDate = new DateTime(2024, 6, 20), Progress = 10, EnrollmentStatus = EnrollmentStatus.Dropped },
                    new Enrollments { StudentsId = students[3].Id, CoursesId = 4, EnrollmentDate = new DateTime(2024, 7, 1), Progress = 100, EnrollmentStatus = EnrollmentStatus.Completed, FinalGrade = 78.25m },
                    new Enrollments { StudentsId = students[4].Id, CoursesId = 1, EnrollmentDate = new DateTime(2024, 7, 5), Progress = 60, EnrollmentStatus = EnrollmentStatus.Active }
                );
                db.SaveChanges();
                Console.WriteLine("Seeded 8 enrollments.");
            }
        }
        else
        {
            Console.WriteLine("Seed data already exists, skipping.");
        }
    }

    static int AddInstructor(AppDbContext db)
    {
        var instructor = new Instructors
        {
            FullName = "Laila Mansour",
            EmailAddress = "laila.mansour@example.com",
            BIO = "Data science instructor.",
            JoinedDate = DateTime.Now
        };

        db.Instructors.Add(instructor);
        db.SaveChanges();

        Console.WriteLine($"Instructor added. Generated Id = {instructor.Id}");
        return instructor.Id;
    }

    static int AddCourse(AppDbContext db, int instructorId, int categoryId)
    {
        bool instructorExists = db.Instructors.Any(i => i.Id == instructorId);
        bool categoryExists = db.Categories.Any(c => c.Id == categoryId);

        if (!instructorExists)
        {
            Console.WriteLine($"Cannot add course: instructor with Id {instructorId} does not exist.");
            return 0;
        }

        if (!categoryExists)
        {
            Console.WriteLine($"Cannot add course: category with Id {categoryId} does not exist.");
            return 0;
        }

        var course = new Courses
        {
            Title = "Python for Data Analysis",
            Description = "Analyze data with Python.",
            Price = 500m,
            Level = CourseLevel.Intermediate,
            IsPublished = false,
            InstructorId = instructorId,
            CategoryId = categoryId
        };

        db.Courses.Add(course);
        db.SaveChanges();

        Console.WriteLine($"Course added. Generated Id = {course.Id}");
        return course.Id;
    }

    static void AddStudents(AppDbContext db)
    {
        var newStudents = new List<Students>
        {
            new Students { FullName = "Salma Reda",   EmailAddress = "salma.reda@example.com",   DOB = new DateTime(2002, 2, 18) },
            new Students { FullName = "Tarek Aziz",   EmailAddress = "tarek.aziz@example.com",   DOB = new DateTime(2001, 9, 5) },
            new Students { FullName = "Dina Mostafa", EmailAddress = "dina.mostafa@example.com", DOB = new DateTime(2000, 12, 22) }
        };

        db.Students.AddRange(newStudents);
        db.SaveChanges();

        Console.WriteLine("Students added:");
        foreach (var s in newStudents)
        {
            Console.WriteLine($"  Id = {s.Id}, Name = {s.FullName}");
        }
    }

    static void EnrollStudent(AppDbContext db, int studentId, int courseId)
    {
        bool studentExists = db.Students.Any(s => s.Id == studentId);
        if (!studentExists)
        {
            Console.WriteLine($"Enrollment refused: student {studentId} does not exist.");
            return;
        }

        var course = db.Courses.Find(courseId);
        if (course == null)
        {
            Console.WriteLine($"Enrollment refused: course {courseId} does not exist.");
            return;
        }

        if (!course.IsPublished)
        {
            Console.WriteLine($"Enrollment refused: course '{course.Title}' is not published.");
            return;
        }

        bool alreadyEnrolled = db.Enrollments.Any(e => e.StudentsId == studentId && e.CoursesId == courseId);
        if (alreadyEnrolled)
        {
            Console.WriteLine($"Enrollment refused: student {studentId} is already enrolled in '{course.Title}'.");
            return;
        }

        var enrollment = new Enrollments
        {
            StudentsId = studentId,
            CoursesId = courseId,
            EnrollmentDate = DateTime.Now,
            Progress = 0,
            EnrollmentStatus = EnrollmentStatus.Active
        };

        db.Enrollments.Add(enrollment);
        db.SaveChanges();

        Console.WriteLine($"Student {studentId} enrolled in '{course.Title}'.");
    }

    static void FindCourse(AppDbContext db, int id, string title)
    {
        var byId = db.Courses.Find(id);
        if (byId == null)
            Console.WriteLine($"Find by Id {id}: not found.");
        else
            Console.WriteLine($"Find by Id {id}: {byId.Title} ({byId.Level}, {byId.Price:F2})");

        var byTitle = db.Courses.FirstOrDefault(c => c.Title == title);
        if (byTitle == null)
            Console.WriteLine($"Find by title '{title}': not found.");
        else
            Console.WriteLine($"Find by title '{title}': Id = {byTitle.Id}, {byTitle.Level}, {byTitle.Price:F2}");
    }

    static void FilterCourses(AppDbContext db, decimal minPrice, decimal maxPrice)
    {
        if (minPrice > maxPrice)
        {
            Console.WriteLine("Invalid range: the minimum price is greater than the maximum price.");
            return;
        }

        var courses = db.Courses
            .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
            .OrderByDescending(c => c.Price)
            .Select(c => new { c.Title, c.Level, c.Price })
            .ToList();

        Console.WriteLine($"Courses priced between {minPrice:F2} and {maxPrice:F2} (highest first):");
        if (courses.Count == 0)
        {
            Console.WriteLine("  No courses found.");
            return;
        }

        foreach (var c in courses)
        {
            Console.WriteLine($"  {c.Title} | {c.Level} | {c.Price:F2}");
        }
    }

    static void SubCategories(AppDbContext db, int parentId)
    {
        var parent = db.Categories.Find(parentId);
        if (parent == null)
        {
            Console.WriteLine($"Category {parentId} not found.");
        }
        else
        {
            var subs = db.Categories.Where(c => c.ParentCategoryId == parentId).ToList();
            Console.WriteLine($"Direct sub-categories of '{parent.Name}':");
            if (subs.Count == 0)
                Console.WriteLine("  None.");
            foreach (var s in subs)
            {
                Console.WriteLine($"  {s.Id}: {s.Name}");
            }
        }

        var topLevel = db.Categories.Where(c => c.ParentCategoryId == null).ToList();
        Console.WriteLine("Top-level categories:");
        foreach (var t in topLevel)
        {
            Console.WriteLine($"  {t.Id}: {t.Name}");
        }
    }

    static void CourseRoster(AppDbContext db, int courseId)
    {
        var course = db.Courses.Find(courseId);
        if (course == null)
        {
            Console.WriteLine($"Roster: course {courseId} not found.");
            return;
        }

        var studentIds = db.Enrollments
            .Where(e => e.CoursesId == courseId)
            .Select(e => e.StudentsId)
            .ToList();

        Console.WriteLine($"Course '{course.Title}' has {studentIds.Count} enrolled student(s):");

        var students = db.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToList();

        foreach (var s in students)
        {
            Console.WriteLine($"  {s.FullName}");
        }
    }

    static void ChangePrice(AppDbContext db, int courseId, decimal newPrice)
    {
        if (newPrice < 0)
        {
            Console.WriteLine("Invalid price: it cannot be negative.");
            return;
        }

        var course = db.Courses.Find(courseId);
        if (course == null)
        {
            Console.WriteLine($"Change price: course {courseId} not found.");
            return;
        }

        decimal oldPrice = course.Price;
        course.Price = newPrice;
        db.SaveChanges();

        Console.WriteLine($"Price of '{course.Title}' changed from {oldPrice:F2} to {newPrice:F2}.");
    }

    static void UpdateProgress(AppDbContext db, int studentId, int courseId, decimal progress)
    {
        if (progress < 0 || progress > 100)
        {
            Console.WriteLine($"Invalid progress {progress}: it must be between 0 and 100.");
            return;
        }

        var enrollment = db.Enrollments.Find(studentId, courseId);
        if (enrollment == null)
        {
            Console.WriteLine($"Update progress: no enrollment for student {studentId} in course {courseId}.");
            return;
        }

        enrollment.Progress = progress;
        if (progress == 100)
        {
            enrollment.EnrollmentStatus = EnrollmentStatus.Completed;
        }
        db.SaveChanges();

        Console.WriteLine($"Progress for student {studentId} in course {courseId} set to {progress}. Status: {enrollment.EnrollmentStatus}.");
    }

    static void PublishCourse(AppDbContext db, int courseId)
    {
        var course = db.Courses.Find(courseId);
        if (course == null)
        {
            Console.WriteLine($"Publish: course {courseId} not found.");
            return;
        }

        if (course.IsPublished)
        {
            Console.WriteLine($"Course '{course.Title}' is already published.");
            return;
        }

        course.IsPublished = true;
        course.PublishedDate = DateTime.Now;
        db.SaveChanges();

        Console.WriteLine($"Course '{course.Title}' published on {course.PublishedDate:yyyy-MM-dd}.");
    }

    static void Unenroll(AppDbContext db, int studentId, int courseId)
    {
        var enrollment = db.Enrollments.Find(studentId, courseId);
        if (enrollment == null)
        {
            Console.WriteLine($"Unenroll: no enrollment for student {studentId} in course {courseId}.");
            return;
        }

        db.Enrollments.Remove(enrollment);
        db.SaveChanges();

        Console.WriteLine($"Student {studentId} was unenrolled from course {courseId}.");
    }

    static void DeleteCategoryUnsafe(AppDbContext db, int categoryId)
    {
        db.ChangeTracker.Clear();

        var category = db.Categories.Find(categoryId);
        if (category == null)
        {
            Console.WriteLine($"Delete category: category {categoryId} not found.");
            return;
        }

        try
        {
            db.Categories.Remove(category);
            db.SaveChanges();
            Console.WriteLine($"Category '{category.Name}' deleted.");
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Could not delete '{category.Name}': {ex.InnerException?.Message ?? ex.Message}");
            db.ChangeTracker.Clear();
        }
    }

    static void DeleteCategorySafe(AppDbContext db, int categoryId)
    {
        var category = db.Categories.Find(categoryId);
        if (category == null)
        {
            Console.WriteLine($"Safe delete: category {categoryId} not found.");
            return;
        }

        if (db.Categories.Any(c => c.ParentCategoryId == categoryId))
        {
            Console.WriteLine($"Safe delete refused: '{category.Name}' still has sub-categories.");
            return;
        }

        if (db.Courses.Any(c => c.CategoryId == categoryId))
        {
            Console.WriteLine($"Safe delete refused: '{category.Name}' still has courses.");
            return;
        }

        db.Categories.Remove(category);
        db.SaveChanges();

        Console.WriteLine($"Category '{category.Name}' deleted safely.");
    }

    static void DeleteCourse(AppDbContext db, int courseId)
    {
        var course = db.Courses.Find(courseId);
        if (course == null)
        {
            Console.WriteLine($"Delete course: course {courseId} not found.");
            return;
        }

        int before = db.Enrollments.Count(e => e.CoursesId == courseId);
        Console.WriteLine($"Enrollments of '{course.Title}' before delete: {before}");

        db.Courses.Remove(course);
        db.SaveChanges();

        // The enrollments were removed too because the foreign key from Enrollments to Courses
        // is configured with DeleteBehavior.Cascade, so the database deletes the child rows
        // automatically when their parent course is deleted.

        int after = db.Enrollments.Count(e => e.CoursesId == courseId);
        Console.WriteLine($"Enrollments of the deleted course after delete: {after}");
    }

    static void BulkPriceRaise(AppDbContext db, CourseLevel level)
    {
        int affected = db.Courses
            .Where(c => c.IsPublished && c.Level == level)
            .ExecuteUpdate(s => s.SetProperty(c => c.Price, c => c.Price * 1.10m));

        Console.WriteLine($"Bulk price raise: {affected} published {level} course(s) raised by 10%.");
    }

    static void BulkDeleteDropped(AppDbContext db)
    {
        int affected = db.Enrollments
            .Where(e => e.EnrollmentStatus == EnrollmentStatus.Dropped)
            .ExecuteDelete();

        Console.WriteLine($"Bulk delete: {affected} dropped enrollment(s) deleted.");
    }
}